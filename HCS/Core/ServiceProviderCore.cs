using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interaces;
using HCS.Providers;

namespace HCS.Core
{
    public class ServiceProviderCore
    {
        Dictionary<EndPoints, IProvider> _providerLocator;
        public ServiceProviderCore(ClientConfig config)
        {
            _providerLocator = new Dictionary<EndPoints, IProvider>();
            _providerLocator.Add(EndPoints.HouseManagementAsync, new HouseManagmentProvider(config));
        }
        
        public Result<object> Run(IMessageBase message) 
        {
            var result = new Result<object>();

            try {
                var provider = _providerLocator[message.EndPoint];
                if (provider == null)
                    throw new ArgumentOutOfRangeException($"Для конечной точки {Enum.GetName(typeof(EndPoints),message.EndPoint)} отсутсвует зарегистрированный провайдер");
                var ack = provider.Send(message.Request);
                result.Value = ack;

            }catch(System.ServiceModel.FaultException<IFault> ex) {
                
                
            }
            
            return result;
        }
    }
}
