using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Interfaces
{
    public interface IHeaderType
    {
        string MessageGUID { get; set; }
        DateTime Date { get; set; }
    }
}
