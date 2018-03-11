using System;
using System.Security.Cryptography.X509Certificates;

using HCS.Service.Async.Bills.v11_10_0_13;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interfaces;
using HCS.Helpers;

namespace HCS.Providers
{
    public class BillsProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.BillsAsync;

        public BillsProvider(ClientConfig config):base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new BillsPortsTypeAsyncClient(_binding, _remoteAddress)) {
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

        public IAck Send<T>(T request)
        {
            using (var client = new BillsPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
                    case nameof(exportInsuranceProductRequest1):
                        return client.exportInsuranceProduct(request as exportInsuranceProductRequest1).AckRequest.Ack;
                    case nameof(exportNotificationsOfOrderExecutionRequest1):
                        return client.exportNotificationsOfOrderExecution(request as exportNotificationsOfOrderExecutionRequest1).AckRequest.Ack;
                    case nameof(exportNotificationsOfOrderExecutionPaginalRequest1):
                        return client.exportNotificationsOfOrderExecutionPaginal(request as exportNotificationsOfOrderExecutionPaginalRequest1).AckRequest.Ack;
                    case nameof(exportPaymentDocumentDataRequest):
                        return client.exportPaymentDocumentData(request as exportPaymentDocumentDataRequest).AckRequest.Ack;
                    case nameof(exportSettlementsRequest1):
                        return client.exportSettlements(request as exportSettlementsRequest1).AckRequest.Ack;
                    case nameof(importAcknowledgmentRequest1):
                        return client.importAcknowledgment(request as importAcknowledgmentRequest1).AckRequest.Ack;
                    case nameof(importIKUSettlementsRequest1):
                        return client.importIKUSettlements(request as importIKUSettlementsRequest1).AckRequest.Ack;
                    case nameof(importInsuranceProductRequest1):
                        return client.importInsuranceProduct(request as importInsuranceProductRequest1).AckRequest.Ack;
                    case nameof(importPaymentDocumentDataRequest):
                        return client.importPaymentDocumentData(request as importPaymentDocumentDataRequest).AckRequest.Ack;
                    case nameof(importRSOSettlementsRequest1):
                        return client.importRSOSettlements(request as importRSOSettlementsRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }
        }
    }
}
