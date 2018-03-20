using System;
using System.Security.Cryptography.X509Certificates;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Helpers;
using HCS.Interfaces;
using HCS.Service.Async.OrganizationsRegistryCommon.v11_10_0_13;

namespace HCS.Providers
{
    public class OrganizationsRegistryCommon : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.OrgRegistryCommonAsync;
        public OrganizationsRegistryCommon(ClientConfig config) : base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public IAck Send<T>(T request)
        {
            using (var client = new RegOrgPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
                    case nameof(exportDataProviderRequest1):
                        return client.exportDataProvider(request as exportDataProviderRequest1).AckRequest.Ack;
                    case nameof(exportDelegatedAccessRequest1):
                        return client.exportDelegatedAccess(request as exportDelegatedAccessRequest1).AckRequest.Ack;
                    case nameof(exportObjectsDelegatedAccessRequest1):
                        return client.exportObjectsDelegatedAccess(request as exportObjectsDelegatedAccessRequest1).AckRequest.Ack;
                    case nameof(exportOrgRegistryRequest1):
                        return client.exportOrgRegistry(request as exportOrgRegistryRequest1).AckRequest.Ack;
                    case nameof(exportTerritoryDelegatedAccessRequest1):
                        return client.exportTerritoryDelegatedAccess(request as exportTerritoryDelegatedAccessRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new RegOrgPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
                    ISRequestHeader = RequestHelper.Create<ISRequestHeader>(_config.OrgPPAGUID, _config.Role),
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
