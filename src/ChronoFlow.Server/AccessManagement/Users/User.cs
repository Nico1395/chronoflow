using ChronoFlow.Server.Common.Entities;

namespace ChronoFlow.Server.AccessManagement.Users;

public sealed class User : MainDataAggregate
{
    public required UserCredentials Credentials { get; set; }
    public List<UserRole> UserRoles { get; init; } = [];
}
