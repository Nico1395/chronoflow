namespace ChronoFlow.Client.Common.Components.Controls.Forms;

internal interface IField
{
    internal bool Validating { get; set; }
    internal bool Valid { get; set; }
    internal void Register(IControl control);
}
