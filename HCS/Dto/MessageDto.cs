using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Globals;
using HCS.Interfaces;

namespace HCS.Dto
{
    public class MessageDto :BaseTypes.DtoObject, IMessageType
    {
       public EndPoints EndPoint { get; set; }
       public Type RequestType { get; set; }
       public object Request { get; set; }
       public object Result { get; set; }
       public Guid MessageGUID { get; set; }
       public Guid ResponceGUID { get; set; }
       public DateTime SendDate { get; set; }
       public DateTime CompliteDate { get; set; }
       public MessageStatuses MessageStatus { get; set; }
    }
}
