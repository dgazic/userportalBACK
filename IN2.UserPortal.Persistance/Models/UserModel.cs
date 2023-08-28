﻿namespace IN2.UserPortal.Persistance.Models
{
    public record UserModel
    {
        public int Id { get; init; }

        public string Email { get; init; }

        public string Username { get; init; }

        public byte[]? Password { get; init; }

        public byte[]? SaltPassword { get; init; }

        public string LastName { get; init; }

        public string FirstName { get; init; }

        public string LastNameFirstName { get; init; }

        public int? UserRoleId { get; init; }

        public string? UserRoleName { get; init; }

        public int? Activated { get; init; }

        public byte[] ActivationToken { get; init; }

        public int AdministratorId { get; init; }

        public string PhoneNumber { get; init; }

        public string Hospital { get; init; }

        public string? UserportalHospitalName { get; init; }

        public int HospitalId { get; init; }

    }
}
