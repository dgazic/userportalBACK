namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class UserRegisterDto
    {

        public string Username { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }


        public int UserRoleId { get; set; } 

        public string PhoneNumber { get; set; }

        public string? HospitalName { get; set; }

    }
}
