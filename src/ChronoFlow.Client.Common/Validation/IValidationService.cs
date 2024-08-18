using Microsoft.AspNetCore.Components.Forms;

namespace ChronoFlow.Client.Common.Validation;

/// <summary>
/// Service validates a specified models property values and validation attributes.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Service validates a specified models property values and validation attributes
    /// </summary>
    /// <param name="item">View model to be validated.</param>
    /// <param name="editContext">Edit context of the form.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>State of the validation process.</returns>
    public ValidationState Validate(object viewModel, EditContext editContext, CancellationToken cancellationToken = default);
}
