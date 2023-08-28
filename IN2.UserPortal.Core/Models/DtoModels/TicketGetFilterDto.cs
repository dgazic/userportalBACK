using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN2.UserPortal.Core.Models.DtoModels
{
    public class TicketGetFilterDto
    {
        public DateTime? enrollmentTimeDateFrom { get; set; }
        public DateTime? enrollmentTimeDateTo { get; set; }
    }
}
