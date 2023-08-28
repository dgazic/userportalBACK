using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN2.UserPortal.Core.Responses
{
    public class UserLogInResponse : ResponseBase
    {
        public string Token { get; set; }
    }
}
