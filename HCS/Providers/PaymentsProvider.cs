using System;
using System.Security.Cryptography.X509Certificates;
using HCS.Service.Async.Payment.v11_10_0_13;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interfaces;
using HCS.Helpers;

namespace HCS.Providers
{
    public class PaymentsProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.PaymentsAsync;
        public PaymentsProvider(ClientConfig config):base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public IAck Send(object request)
        {
            using (var client = new PaymentPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
                    case nameof(exportPaymentDocumentDetailsRequest1):
                        return client.exportPaymentDocumentDetails(request as exportPaymentDocumentDetailsRequest1).AckRequest.Ack;
                    case nameof(importNotificationsOfOrderExecutionRequest1):
                        return client.importNotificationsOfOrderExecution(request as importNotificationsOfOrderExecutionRequest1).AckRequest.Ack;
                    case nameof(importNotificationsOfOrderExecutionCancellationRequest1):
                        return client.importNotificationsOfOrderExecutionCancellation(request as importNotificationsOfOrderExecutionCancellationRequest1).AckRequest.Ack;
                    case nameof(importSupplierNotificationsOfOrderExecutionRequest1):
                        return client.importSupplierNotificationsOfOrderExecution(request as importSupplierNotificationsOfOrderExecutionRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new PaymentPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
