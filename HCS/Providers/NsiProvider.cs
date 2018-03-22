using System;
using System.Security.Cryptography.X509Certificates;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Helpers;
using HCS.Interfaces;
using HCS.Service.Async.Nsi.v11_10_0_13;

namespace HCS.Providers
{
    public class NsiProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.NsiAsync;
        public NsiProvider(ClientConfig config) : base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public IAck Send(object request)
        {
            using (var client = new NsiPortsTypeAsyncClient(_binding, _remoteAddress)) {
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

                switch (request.GetType().Name) {
                    case nameof(exportDataProviderNsiItemRequest1):
                        return client.exportDataProviderNsiItem(request as exportDataProviderNsiItemRequest1).AckRequest.Ack;
                    case nameof(exportDataProviderPagingNsiItemRequest):
                        return client.exportDataProviderPagingNsiItem(request as exportDataProviderPagingNsiItemRequest).AckRequest.Ack;
                    case nameof(importAdditionalServicesRequest1):
                        return client.importAdditionalServices(request as importAdditionalServicesRequest1).AckRequest.Ack;
                    case nameof(importBaseDecisionMSPRequest1):
                        return client.importBaseDecisionMSP(request as importBaseDecisionMSPRequest1).AckRequest.Ack;
                    case nameof(importCapitalRepairWorkRequest1):
                        return client.importCapitalRepairWork(request as importCapitalRepairWorkRequest1).AckRequest.Ack;
                    case nameof(importMunicipalServicesRequest1):
                        return client.importMunicipalServices(request as importMunicipalServicesRequest1).AckRequest.Ack;
                    case nameof(importOrganizationWorksRequest1):
                        return client.importOrganizationWorks(request as importOrganizationWorksRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }

        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new NsiPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
