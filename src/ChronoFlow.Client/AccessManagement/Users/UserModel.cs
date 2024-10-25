namespace ChronoFlow.Client.AccessManagement.Users;

public sealed class UserModel
{
    public required UserCredentialsModel Credentials { get; set; }
    public List<UserRoleModel> UserRoles { get; init; } = [];
}
