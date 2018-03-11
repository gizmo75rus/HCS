using HCS.Interfaces;

namespace HCS.Service.Async.HouseManagement.v11_10_0_13 {
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}
namespace HCS.Service.Async.DeviceMetering.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.Bills.v11_10_0_13 {
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.Payment.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.Licenses.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}
namespace HCS.Service.Async.Nsi.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.NsiCommon.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult
    {
        public object[] Items {
            get
            {
                if (Item != null)
                    return new object[] { Item };
                else
                    return null;
            }
            set { }
        }
    }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.OrganizationsRegistry.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}

namespace HCS.Service.Async.OrganizationsRegistry.v11_10_0_13
{
    public partial class AckRequestAck : IAck { }
    public partial class getStateResult : IGetStateResult { }
    public partial class Fault : IFault { }
    public partial class HeaderType : IHeaderType { }
}
