using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Helpers;
using HCS.Interfaces;
using HCS.Service.Async.Licenses.v11_10_0_13;

namespace HCS.Providers
{
    public class LicensesProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.LicensesAsync;

        public LicensesProvider(ClientConfig config) : base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }
        public IAck Send<T>(T request)
        {
            using (var client = new LicensePortsTypeAsyncClient(_binding, _remoteAddress)) {
                client.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());
                if (!_config.IsPPAK) {
                    client.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    client.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!_config.UseTunnel) {
                    client.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base._config.CertificateThumbprint);
                }

                switch (typeof(T).Name) {
                    case nameof(exportDisqualifiedPersonRequest1):
                        return client.exportDisqualifiedPerson(request as exportDisqualifiedPersonRequest1).AckRequest.Ack;
                    case nameof(exportLicenseRequest1):
                        return client.exportLicense(request as exportLicenseRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new LicensePortsTypeAsyncClient(_binding, _remoteAddress)) {
                client.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());
                if (!_config.IsPPAK) {
                    client.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    client.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!_config.UseTunnel) {
                    client.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base._config.CertificateThumbprint);
                }

                var responce = client.getState(new getStateRequest1 {
                    RequestHeader = RequestHelper.Create<RequestHeader>(_config.OrgPPAGUID, _config.Role),
                    getStateRequest = new getStateRequest {
                        MessageGUID = ack.MessageGUID
                    }
                });

                if (responce.getStateResult.RequestState == 3) {
                    result = responce.getStateResult;
                    return true;
                }

                result = null;
                return false;
            }
        }
    }
}
