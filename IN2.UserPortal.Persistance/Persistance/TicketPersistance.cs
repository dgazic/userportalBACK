using Dapper;
using IN2.UserPortal.Persistance.Context;
using IN2.UserPortal.Persistance.Interfaces;
using IN2.UserPortal.Persistance.Models;
using System.Data;

namespace IN2.UserPortal.Persistance.Persistance
{
    public class TicketPersistance : ITicketRepository
    {
        private readonly DapperContext _context;

        public TicketPersistance(DapperContext context)
        {
            _context = context;
        }
            
            
        public async Task<IEnumerable<TicketModel>> GetAll(int userId, int userRoleId, DateTime? enrollmentTimeDateFrom, DateTime? enrollmentTimeDateTo)
        {

            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    UserRoleId = userRoleId,
                    UserId = userId,
                    EnrollmentTimeDateFrom = enrollmentTimeDateFrom,
                    EnrollmentTimeDateTo = enrollmentTimeDateTo
                };

                var tickets = await connection.QueryAsync<TicketModel>("dbo.TicketsMantis_Select", parameters,
                commandType: CommandType.StoredProcedure);
                return tickets;
            }
        }

        public async Task<TicketModel> GetById(int id)
        {
            using(var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Id = id
                };
                var ticket = await connection.QueryAsync<TicketModel>("dbo.TicketGetById_Select", parameters, commandType: CommandType.StoredProcedure);
                return ticket.FirstOrDefault();
            }
        }

        public async Task TicketClose(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Id = id
                };

                await connection.QueryAsync<TicketModel>("dbo.CloseTicket_Update", parameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task TicketChangePriority(TicketModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Id = model.Id,
                    Priority = model.Priority
                };

                await connection.QueryAsync<TicketModel>("dbo.ChangeTicketPriority_Update", parameters,
                commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TicketAttachmentModel>> GetTicketAttachment(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Id = id
                };

                var tickets = await connection.QueryAsync<TicketAttachmentModel>("dbo.GetTicketAttachment_Select", parameters,
                commandType: CommandType.StoredProcedure);
                return tickets;
            }
        }

        public async Task<IEnumerable<HospitalContractedProduct>> GetHospitalProducts(string userHospital)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    UserHospital = userHospital
                };
                var hospitalProducts = await connection.QueryAsync<HospitalContractedProduct>("dbo.GetHospitalProducts_Select", parameters,
                    commandType: CommandType.StoredProcedure);
                return hospitalProducts;
            }
        }

        public async Task<IEnumerable<HospitalContractedProduct>> GetProductDomains(string productName)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    ProductName = productName
                };
                var hospitalProducts = await connection.QueryAsync<HospitalContractedProduct>("dbo.GetProductDomains_Select", parameters,
                    commandType: CommandType.StoredProcedure);
                return hospitalProducts;
            }
        }

        public async Task<IEnumerable<HospitalModel>> GetHospitals()
        {
            using (var connection = _context.CreateConnection())
            {
                var hospitals = await connection.QueryAsync<HospitalModel>("dbo.GetHospitals_Select",
                    commandType: CommandType.StoredProcedure);
                return hospitals;
            }
        }
        public async Task<IEnumerable<UserModel>> GetHospitalUsers(string hospitalName)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    HospitalName = hospitalName
                };
                var users = await connection.QueryAsync<UserModel>("dbo.GetHospitalUsers_Select",parameters,
                    commandType: CommandType.StoredProcedure);
                return users;
            }
        }


        public async Task<TicketModel> GetCurrentRegisteredTicket(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    UserId = userId
                };
                var ticket = await connection.QueryAsync<TicketModel>("dbo.GetCurrentRegisteredTicket_Select", parameters, commandType: CommandType.StoredProcedure);
                return ticket.FirstOrDefault();
            }
        }



        public Task Delete(TicketModel model)
        {
            throw new NotImplementedException();
        }

        public Task<TicketModel> Update(TicketModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketModel> Insert(TicketModel model)
        {
            DataTable tblTicketAttachment = new DataTable("tblTicketAttachment");
            tblTicketAttachment.Columns.Add("Title", typeof(string));
            tblTicketAttachment.Columns.Add("DocumentExtension", typeof(string));
            tblTicketAttachment.Columns.Add("DocumentData", typeof(byte[]));
            tblTicketAttachment.Columns.Add("Size", typeof(int));

            foreach (TicketAttachmentModel ticketAttachmentModel in model.Attachments)
            {
                tblTicketAttachment.Rows.Add
                    (
                        ticketAttachmentModel.title,
                        ticketAttachmentModel.documentExtension,
                        ticketAttachmentModel.documentData,
                        ticketAttachmentModel.size
                    );
            }

            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Abstract = model.Abstract,
                    Type = model.Type,
                    Description = model.Description,
                    UserId = model.UserId,
                    Product = model.Product,
                    Domain = model.Domain,
                    Priority = model.Priority,
                    TicketAttachment = tblTicketAttachment.AsTableValuedParameter("TicketAttachment_UDT"),
                };

                var ticketModel = await connection.QueryAsync<TicketModel>("dbo.TicketMantis_Insert", parameters,
                commandType: CommandType.StoredProcedure);
                return ticketModel.FirstOrDefault();
            }
        }

        Task<IEnumerable<TicketModel>> IAsyncRepository<TicketModel>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
