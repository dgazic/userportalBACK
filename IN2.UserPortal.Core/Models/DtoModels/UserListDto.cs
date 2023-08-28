namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class UserListDto
    {
        public int Id { get; init; }

        public string? LastName { get; init; }

        public string? FirstName { get; init; }

        public int? UserRoleId { get; init; }

        public int? Activated { get; init; }

        public string? PhoneNumber { get; init; }

        public string Email { get; init; }

        public string Username { get; init; }

        public int AdministratorId { get; init; }
        public string Hospital { get; init; }

    }
}
