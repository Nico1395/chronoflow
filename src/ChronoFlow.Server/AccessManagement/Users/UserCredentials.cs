namespace ChronoFlow.Server.AccessManagement.Users;

public sealed record UserCredentials
{
    public required string LoginName { get; set; }
    public required string PasswordHash { get; set; } // TODO -> Default password. On login the records are checked. If a login is the first login the user will be prompted to change the password so the password given by the admin has to be changed
}
