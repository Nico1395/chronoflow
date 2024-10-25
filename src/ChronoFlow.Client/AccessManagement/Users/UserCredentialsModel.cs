namespace ChronoFlow.Client.AccessManagement.Users;

public sealed record UserCredentialsModel
{
    public required string LoginName { get; set; }
    public required string PasswordHash { get; set; }
}
