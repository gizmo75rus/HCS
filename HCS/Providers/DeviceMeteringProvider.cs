using System;
using HCS.Service.Async.DeviceMetering.v11_10_0_13;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interfaces;
using System.Security.Cryptography.X509Certificates;
using HCS.Helpers;

namespace HCS.Providers
{
    class DeviceMeteringProvider : SoapClientBase, IProvider
    {
        public EndPoints EndPoint => EndPoints.DeviceMeteringAsync;

        public DeviceMeteringProvider(ClientConfig config) : base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new DeviceMeteringPortTypesAsyncClient(_binding, _remoteAddress)) {
                client.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());

                if (!base._config.IsPPAK) {
                    client.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    client.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!base._config.UseTunnel) {
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
            using(var client = new DeviceMeteringPortTypesAsyncClient(_binding, _remoteAddress)) {
                client.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());

                if (!base._config.IsPPAK) {
                    client.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    client.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!base._config.UseTunnel) {
                    client.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base._config.CertificateThumbprint);
                }

                switch (typeof(T).Name) {
                    case nameof(exportMeteringDeviceHistoryRequest1):
                        return client.exportMeteringDeviceHistory(request as exportMeteringDeviceHistoryRequest1).AckRequest.Ack;
                    case nameof(importMeteringDeviceValuesRequest1):
                        return client.importMeteringDeviceValues(request as importMeteringDeviceValuesRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }
            }
        }
    }
}
