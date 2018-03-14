using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.BaseTypes
{
    public class ActionResult
    {
        public ActionResult(ActionStatus status, string message = "")
        {
            Status = status;
            Message = message;
        }

        public ActionStatus Status { get; set; }

        public string Message { get; set; }
    }
    public enum ActionStatus
    {
        Ok,
        Error
    }
}
