using System.ComponentModel.DataAnnotations;

namespace ChronoFlow.Client.Common.Authentication.Entities;

public sealed record AuthenticationRequestViewModel
{
    [Required]
    public string PersonnelNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool ClockIn { get; set; }
}
