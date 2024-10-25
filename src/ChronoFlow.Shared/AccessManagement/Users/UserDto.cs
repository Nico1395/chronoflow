namespace ChronoFlow.Shared.AccessManagement.Users;

public sealed class UserDto
{
    public required UserCredentialsDto Credentials { get; set; }
    public List<UserRoleDto> UserRoles { get; init; } = [];
}
