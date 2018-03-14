using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Polices
{
    public class SoupFaultPolicy 
    {
        List<ActionByFault> _policesByCode;
        public SoupFaultPolicy()
        {
            _policesByCode = new List<ActionByFault>() {
                new ActionByFault{Action = Actions.Abort, ErrorCodes = new string[] { "AUT011000", "AUT011003", "AUT011004", "AUT011005", "AUT011009", "FMT001300", "FMT001301", "FMT001307", "FMT001310", "INT002013", "FIL001001", "FIL001002", "FIL001004", "FIL001005" } },
                new ActionByFault{Action = Actions.TryAgain, ErrorCodes = new string[] { "EXP002002", "EXP002001", "EXP002003", "INT002018" } },
                new ActionByFault{Action = Actions.Empty, ErrorCodes = new string[] { "INT002012", "INT002000" } },
                new ActionByFault{Action = Actions.NeedException,ErrorCodes = new string[]{ "EXP001000" } }
            };
        }

        public Actions GetAction(string errorCode)
        {
            if (_policesByCode.FirstOrDefault(x => x.ErrorCodes.Contains(errorCode)) is ActionByFault policy)
                return policy.Action;

            return Actions.Error;
        }

    }
}
