using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.BaseTypes
{
    public class MessagePackage<T>
    {
        public object Who { get; private set; }
        public T What { get; private set; }
        public DateTime When { get; private set; }
        public MessagePackage(T payload, object source)
        {
            Who = source;
            What = payload;
            When = DateTime.UtcNow;
        }
    }

}
