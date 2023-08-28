using IN2.UserPortal.Persistance.Models;

namespace IN2.UserPortal.Persistance.Interfaces
{
    public interface ITicketRepository : IAsyncRepository<TicketModel>
    {
        Task<IEnumerable<TicketModel>> GetAll(int userId, int userRoleId, DateTime? enrollmentTimeDateFrom, DateTime? enrollmentTimeDateTo);

        public Task<TicketModel> GetById(int id);

        public Task<TicketModel> GetCurrentRegisteredTicket(int userId);

        public Task TicketClose(int id);

        public Task TicketChangePriority(TicketModel model);

        Task<IEnumerable<TicketAttachmentModel>> GetTicketAttachment(int id);

        Task<IEnumerable<HospitalContractedProduct>> GetHospitalProducts(string userHospital);

        Task<IEnumerable<HospitalContractedProduct>> GetProductDomains(string productName);

        Task<IEnumerable<HospitalModel>> GetHospitals();

        Task<IEnumerable<UserModel>> GetHospitalUsers(string hospitalName);
    }
}
