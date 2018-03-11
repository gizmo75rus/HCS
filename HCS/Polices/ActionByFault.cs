using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Polices
{
    public class ActionByFault
    {
        public string[] ErrorCodes { get; set; }
        public Actions Action { get; set; }
    }
}
