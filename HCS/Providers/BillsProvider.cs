using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interaces;

namespace HCS.Providers
{
    public class BillsProvider : ClientBaseType, IProvider
    {
        public EndPoints EndPoint => EndPoints.BillsAsync;

        public BillsProvider(ClientConfig config):base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public bool GetResult(IAck ack, out IGetStateResult result)
        {
            throw new NotImplementedException();
        }

        public IAck Send<T>(T request)
        {
            throw new NotImplementedException();
        }
    }
}
