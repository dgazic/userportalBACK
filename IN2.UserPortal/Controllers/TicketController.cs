using IN2.UserPortal.Core.Interfaces;
using IN2.UserPortal.Core.Models.DtoModels;
using IN2.UserPortal.Persistance.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IN2.UserPortal.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketPersistance;
        private readonly ITicketRegistrationService _ticketRegistrationService;
        private readonly ITicketCloseService _ticketCloseService;
        private readonly ITicketChangePriorityService _ticketChangePriorityService;
        private readonly ITicketQueryService _ticketQueryService;

        public TicketController(ITicketRegistrationService ticketRegistrationService, ITicketRepository ticketRepository, ITicketCloseService ticketCloseService, ITicketChangePriorityService ticketChangePriorityService, ITicketQueryService ticketQueryService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketRegistrationService = ticketRegistrationService;
            _ticketPersistance = ticketRepository;
            _ticketCloseService = ticketCloseService;
            _ticketChangePriorityService = ticketChangePriorityService;
            _ticketQueryService = ticketQueryService;
        }

        [HttpGet("TicketById")]
        public async Task<IActionResult> GetTicket(int id)
        {
            try
            {
                var ticket = await _ticketQueryService.GetById(id);
                return Ok(ticket.TicketDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetTicketAttachment")]
        public async Task<IActionResult> GetTicketAttachment(int id)
        {
            try
            {
                var ticketAttachment = await _ticketPersistance.GetTicketAttachment(id);
                if (ticketAttachment == null)
                    return NotFound();
                return Ok(ticketAttachment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetTickets")]
        public async Task<IActionResult> GetTickets(DateTime enrollmentTimeDateFrom, DateTime enrollmentTimeDateTo)
        {
            try
            {
                var userId = HttpContext?.User.Claims.Where(x => x.Type == "UserId").Single();
                var userRoleId = HttpContext?.User.Claims.Where(x => x.Type == "UserRoleId").Single();
                var tickets = await _ticketPersistance.GetAll(int.Parse(userId.Value), int.Parse(userRoleId.Value), enrollmentTimeDateFrom, enrollmentTimeDateTo);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("TicketRegistration")]
        public async Task<IActionResult> TicketRegistration(TicketRegistrationDto request)
        {
            try
            {
                var userId = HttpContext?.User.Claims.Where(x => x.Type == "UserId").Single();
                var registerService = await _ticketRegistrationService.TicketRegistration(request, int.Parse(userId.Value));

                var ticket = await _ticketPersistance.GetCurrentRegisteredTicket(int.Parse(userId.Value));
                registerService.ticketId = ticket.Id;
                return Ok(registerService);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CloseTicket")]
        public async Task<IActionResult> CloseTicket(int id)
        {
            try
            {
                var ticketCloseResponse = await _ticketCloseService.TicketClose(id);
                return Ok(ticketCloseResponse);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("ChangeTicketPriority")]
        public async Task<IActionResult> ChangeTicketPriority(TicketChangePriorityDto request)
        {
            try
            {
                var ticketCloseResponse = await _ticketChangePriorityService.TicketChangePriority(request);
                return Ok(ticketCloseResponse);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("GetHospitalProducts")]
        public async Task<IActionResult> GetHospitalProducts(string userHospital)
        {
            try
            {
                var hospitalProducts = await _ticketPersistance.GetHospitalProducts(userHospital);
                return Ok(hospitalProducts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("GetProductDomains")]
        public async Task<IActionResult> GetProductDomains(string productName)
        {
            try
            {
                var productDomains = await _ticketPersistance.GetProductDomains(productName);
                return Ok(productDomains);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetHospitals")]
        public async Task<IActionResult> GetHospitals()
        {
            try
            {
                var hospitals = await _ticketPersistance.GetHospitals();
                return Ok(hospitals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetHospitalUsers")]
        public async Task<IActionResult> GetHospitals(string? hospitalName)
        {
            try
            {
                var hospitals = await _ticketPersistance.GetHospitalUsers(hospitalName ?? "");
                return Ok(hospitals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}



