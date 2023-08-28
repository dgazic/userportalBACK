namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class TicketRegistrationDto
    {

        public string Type { get; set; }

        public string Abstract { get; set; }

        public string Description { get; set; }

        public string Product { get; set; }
        public string Domain { get; set; }

        public string Priority { get; set; }

        public IList<TicketAttachmentDto> Attachments {get; set;}

    }
}
