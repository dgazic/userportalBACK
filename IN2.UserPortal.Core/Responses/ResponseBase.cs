using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IN2.UserPortal.Core.Responses
{
    public class ResponseBase
    {

        
        public bool Success { get; set; }
        public IList<string> ValidationErrors { get; set; }

        public ResponseBase()
        {
            Success = true;
            ValidationErrors = new List<string>();
        }

    }
}
