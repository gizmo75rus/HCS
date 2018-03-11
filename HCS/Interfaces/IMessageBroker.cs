using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;

namespace HCS.Interfaces
{
    public interface IMessageBroker : IDisposable
    {
        void Publish<T>(object source, T message);
        void Subscribe<T>(Action<MessagePackage<T>> subscription);
        void Unsubscribe<T>(Action<MessagePackage<T>> subscription);
    }
}
