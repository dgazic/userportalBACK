namespace IN2.UserPortal.Core.Models.DtoModels
{
    public  class TicketAttachmentDto
    {
        public String title { get; set; }
        public String documentExtension { get; set; }
        public byte[] documentData { get; set; }

        public int size { get; set; }
    }
}
