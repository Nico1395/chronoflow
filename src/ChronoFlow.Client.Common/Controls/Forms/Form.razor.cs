using ChronoFlow.Client.Common.Validation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChronoFlow.Client.Common.Controls.Forms;

/// <summary>
/// Component represents a form.
/// </summary>
public partial class Form : ComponentBase, IDisposable
{
    private EditContext? _editContext;
    private ValidationState? _validationState;
    private EventHandler<ValidationRequestedEventArgs>? _eventHandler;
    private bool _firstValidation = true;
    private bool _isValidating = false;

    internal List<Field> Fields { get; set; } = [];
    internal EditContext EditContext => _editContext ??= new EditContext(Item);

    /// <summary>
    /// Validation service handling the validation process.
    /// </summary>
    [Inject]
    private IValidationService ValidationService { get; set; } = null!;

    /// <summary>
    /// View model edited in the form and valdiated on submit.
    /// </summary>
    [Parameter, EditorRequired]
    public required object Item { get; set; }

    /// <summary>
    /// Placement of validation error messages that might arise during validation.
    /// </summary>
    [Parameter]
    public FormMessagePlacement MessagePlacement { get; set; } = FormMessagePlacement.AsSummary;

    /// <summary>
    /// Invoked when the form has been submitted and errors arose during validation.
    /// </summary>
    [Parameter]
    public EventCallback<ValidationState> OnInvalidSubmit { get; set; }

    /// <summary>
    /// Invoked when the form has been submitted and successfully validated.
    /// </summary>
    [Parameter]
    public EventCallback<object> OnValidSubmit { get; set; }

    /// <summary>
    /// Content of the form. Provides the context of the form as a context parameter.
    /// </summary>
    [Parameter]
    public RenderFragment<FormContext>? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Method disposes off of the form and unsubscribes the event from the <see cref="EditContext"/>.
    /// </summary>
    public void Dispose()
    {
        EditContext.OnValidationRequested -= _eventHandler;
        _eventHandler = null;
    }

    /// <summary>
    /// Method initializes the form. Attaches to the <see cref="EditContext"/>.
    /// </summary>
    protected override void OnInitialized()
    {
        _eventHandler = async (sender, args) => await ValidateAsync();
        EditContext.OnValidationRequested += _eventHandler;
    }

    /// <summary>
    /// Method registers the specified <paramref name="formField"/> to the form.
    /// </summary>
    /// <param name="formField">Form category to be registered.</param>
    internal void Register(Field formField)
    {
        if (!Fields.Contains(formField))
            Fields.Add(formField);
    }

    /// <summary>
    /// Method unregisters the specified <paramref name="formCategory"/> from the form.
    /// </summary>
    /// <param name="formCategory">Form category to be unregistered.</param>
    internal void Unregister(Field formCategory)
    {
        Fields.Remove(formCategory);
    }

    /// <summary>
    /// Method determines whether the specified field's control is has a valid value.
    /// </summary>
    /// <remarks>
    /// This method will return <see langword="true"/> if the form has not validated yet or of the control is not placed in a <see cref="FormField"/>.
    /// The <see cref="FormField"/> contains the properties name and is therefore mandatory for validation. The error message is assigned and rendered in
    /// that component.<br/>
    /// This method exists since when a form template has multiple tabs some will not be rendered and therefore wont be part of the error message assignment
    /// and validation process. However when the respective tab opens and the controls are rendered for the first time they can use this method to request
    /// their validity and error message.
    /// </remarks>
    /// <param name="field">Form fields, whose child control is to be validated.</param>
    /// <returns>
    /// A tuple with values of type <see cref="bool"/> for whether the controls value is valid and <see cref="string?"/> containing the fields error message.
    /// </returns>
    internal (bool IsValid, ValidationError? Error) IsValidField(Field field)
    {
        if (_firstValidation || _validationState == null)
            return (true, null);

        var validationError = _validationState.Errors.FirstOrDefault(e => e.Identifier.Equals(field.Identifier));
        return (validationError == null, validationError);
    }

    /// <summary>
    /// Method validates the <see cref="Item"/> asynchronously.
    /// </summary>
    /// <remarks>
    /// Invokes <see cref="OnValidSubmit"/> or <see cref="OnInvalidSubmit"/> and caches the <see cref="_validationState"/>.
    /// Also remembers whether this validation run is the first validation run. This is done for validating fields that are
    /// initially rendered after a validation run has been completed, for example when fields are placed into a different
    /// not yet rendered tab and their validation data cannot be assigned to them since they are not currently in memory.
    /// </remarks>
    private async Task ValidateAsync()
    {
        await SetIsValidatingAsync(true);

        _validationState = ValidationService.Validate(Item, EditContext);
        PassValidationStateToChildren(_validationState);

        await (_validationState.IsValid
            ? OnValidSubmit.InvokeAsync(Item)
            : OnInvalidSubmit.InvokeAsync(_validationState));

        _firstValidation = false;

        await SetIsValidatingAsync(false);
    }

    /// <summary>
    /// Method applies the specified <paramref name="validationState"/> to the child form controls.
    /// </summary>
    /// <remarks>
    /// Messages and validity is associated with form fields using the <see cref="FormField.FieldIdentity"/> and <see cref="ValidationError.Identifier"/>.
    /// These field identifiers are the best available identifiers for fields. The error identifiers are genereated in the <see cref="IValidationService"/>.
    /// The field ones are created from the specified <see cref="FormField.Field"/> parameter.
    /// </remarks>
    /// <param name="validationState">Validation state to be applied.</param>
    private void PassValidationStateToChildren(ValidationState validationState)
    {
        foreach (var formField in Fields)
            formField.ClearValidationErrors();

        var groupedErrors = validationState.Errors.GroupBy(e => e.Identifier).Select(g => KeyValuePair.Create(g.Key, g.ToList())).ToList();
        foreach (var error in groupedErrors)
        {
            var field = Fields.SingleOrDefault(f => f.Identifier.Equals(error.Key));
            if (field == null)
                continue;

            field.SetValidationErrors(error.Value);
        }
    }

    /// <summary>
    /// Method generate an object encapsulating the context of the form.
    /// </summary>
    /// <returns>A <see cref="FormContext"/> representing the context of the form.</returns>
    private FormContext GenerateFormContext()
    {
        return FormContext.Create(
            _isValidating,
            _validationState?.IsValid ?? true,
            _validationState?.Errors.Select(e => e.Message).ToList() ?? [],
            MessagePlacement);
    }

    /// <summary>
    /// Method passes whether the form is validating or not down to its child components asynchronously.
    /// </summary>
    /// <remarks>
    /// This method is intentionally asynchronous since the rerender is supposed to be awaited and the children are
    /// guaranteed to know whether to be enabled or not.
    /// </remarks>
    /// <param name="isValidating">Determines whether the form is currently validating or not.</param>
    private Task SetIsValidatingAsync(bool isValidating)
    {
        _isValidating = isValidating;

        foreach (var formCategory in Fields)
        {
            formCategory.IsValidating = _isValidating;

            foreach (var formField in Fields)
            {
                formField.IsValidating = _isValidating;

                if (formField.Control != null)
                    formField.Control.IsValidating = _isValidating;
            }
        }

        return Task.Run(StateHasChanged);
    }

    private Dictionary<string, object> GetFormAttributes()
    {
        if (Class == null)
            return [];

        return new Dictionary<string, object> { { "class", Class } };
    }
}
