using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interfaces;

namespace HCS.Providers
{
    public class PaymentsProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.;
        public PaymentsProvider(ClientConfig config):base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

       

        public IAck Send<T>(T request)
        {
            throw new NotImplementedException();
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            throw new NotImplementedException();
        }
    }
}
