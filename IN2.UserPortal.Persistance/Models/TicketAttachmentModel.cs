namespace IN2.UserPortal.Persistance.Models
{
    public class TicketAttachmentModel
    {
        public string title { get; set; }
        public string documentExtension { get; set; }
        public byte[] documentData { get; set; }
        public int size { get; set;}
    }
}
