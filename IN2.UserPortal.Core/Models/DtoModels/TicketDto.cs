using IN2.UserPortal.Persistance.Models;

namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class TicketDto
    {
        public int Id { get; init; }

        public string Type { get; init; }

        public string Abstract { get; init; }

        public string Description { get; init; }

        public string? EnrollmentTime { get; init; }

        public string? Status { get; init; }

        public string Product { get; init; }

        public string Domain { get; init; }

        public string Priority { get; init; }

        public int UserId { get; init; }

        public string firstNameLastNameApplicant { get; init; }

        public string HospitalName { get; init; }

        public IList<TicketAttachmentModel> Attachments { get; set; }

        public bool TicketExist { get; set; } = true;

        public string? TicketHandler { get; set; }
    }
}
