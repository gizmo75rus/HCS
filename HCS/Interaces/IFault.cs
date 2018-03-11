using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Interaces
{
    public interface IFault
    {
        string ErrorCode { get; set; }
        string ErrorMessage { get; set; }
    }
}
