namespace ChronoFlow.Client.Common.Controls.Forms;

public sealed record FormContext
{
    private FormContext()
    {
    }

    public bool IsValidating { get; init; }
    public bool IsValid { get; init; }
    public List<string> Messages { get; init; } = [];
    public FormMessagePlacement MessagePlacement { get; init; }

    internal static FormContext Create(bool isValidating, bool isValid, List<string> messages, FormMessagePlacement messagePlacement)
    {
        return new FormContext()
        {
            IsValidating = isValidating,
            IsValid = isValid,
            Messages = messages,
            MessagePlacement = messagePlacement,
        };
    }
}
