namespace ChronoFlow.Shared.AccessManagement.Users;

public sealed record UserCredentialsDto
{
    public required string LoginName { get; set; }
    public required string PasswordHash { get; set; }
}
