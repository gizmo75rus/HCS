using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Interfaces
{
    public interface IFault
    {
        string ErrorCode { get; set; }
        string ErrorMessage { get; set; }
    }
}
