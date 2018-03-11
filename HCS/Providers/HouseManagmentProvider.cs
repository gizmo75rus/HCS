using System;
using System.Security.Cryptography.X509Certificates;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Helpers;
using HCS.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Providers {

    /// <summary>
    /// Служит для отправки запросов к сервису HouseManagementAsync
    /// </summary>
    public class HouseManagmentProvider : ClientBaseType, IProvider
    {
        public EndPoints EndPoint => EndPoints.HouseManagementAsync;

        public HouseManagmentProvider(ClientConfig config) : base(config)
        {
            _remoteAddress = GetEndpointAddress(Constants.EndPointLocator.GetPath(EndPoint));
        }

        /// <summary>
        /// Метод отравления запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns>Объект реализующий IAck</returns>
        public IAck Send<T>(T request)
        {
            using (var client = new HouseManagementPortsTypeAsyncClient(_binding, _remoteAddress)) {

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
                    case nameof(importAccountDataRequest):
                        return client.importAccountData(request as importAccountDataRequest).AckRequest.Ack;
                    case nameof(importCharterDataRequest):
                        return client.importCharterData(request as importCharterDataRequest).AckRequest.Ack;
                    case nameof(importContractDataRequest):
                        return client.importContractData(request as importContractDataRequest).AckRequest.Ack;
                    case nameof(importHouseOMSDataRequest):
                        return client.importHouseOMSData(request as importHouseOMSDataRequest).AckRequest.Ack;
                    case nameof(importHouseRSODataRequest):
                        return client.importHouseRSOData(request as importHouseRSODataRequest).AckRequest.Ack;
                    case nameof(importHouseUODataRequest):
                        return client.importHouseUOData(request as importHouseUODataRequest).AckRequest.Ack;
                    case nameof(importMeteringDeviceDataRequest1):
                        return client.importMeteringDeviceData(request as importMeteringDeviceDataRequest1).AckRequest.Ack;
                    case nameof(importNotificationDataRequest):
                        return client.importNotificationData(request as importNotificationDataRequest).AckRequest.Ack;
                    case nameof(importPublicPropertyContractRequest1):
                        return client.importPublicPropertyContract(request as importPublicPropertyContractRequest1).AckRequest.Ack;
                    case nameof(importSupplyResourceContractDataRequest):
                        return client.importSupplyResourceContractData(request as importSupplyResourceContractDataRequest).AckRequest.Ack;
                    case nameof(importVotingProtocolRequest1):
                        return client.importVotingProtocol(request as importVotingProtocolRequest1).AckRequest.Ack;
                    case nameof(exportAccountDataRequest):
                        return client.exportAccountData(request as exportAccountDataRequest).AckRequest.Ack;
                    case nameof(exportAccountIndividualServicesRequest1):
                        return client.exportAccountIndividualServices(request as exportAccountIndividualServicesRequest1).AckRequest.Ack;
                    case nameof(exportCAChDataRequest):
                        return client.exportCAChData(request as exportCAChDataRequest).AckRequest.Ack;
                    case nameof(exportHouseDataRequest):
                        return client.exportHouseData(request as exportHouseDataRequest).AckRequest.Ack;
                    case nameof(exportMeteringDeviceDataRequest1):
                        return client.exportMeteringDeviceData(request as exportMeteringDeviceDataRequest1).AckRequest.Ack;
                    case nameof(exportStatusCAChDataRequest):
                        return client.exportStatusCAChData(request as exportStatusCAChDataRequest).AckRequest.Ack;
                    case nameof(exportStatusPublicPropertyContractRequest1):
                        return client.exportStatusPublicPropertyContract(request as exportStatusPublicPropertyContractRequest1).AckRequest.Ack;
                    case nameof(exportSupplyResourceContractDataRequest):
                        return client.exportSupplyResourceContractData(request as exportSupplyResourceContractDataRequest).AckRequest.Ack;
                    case nameof(exportVotingProtocolRequest1):
                        return client.exportVotingProtocol(request as exportVotingProtocolRequest1).AckRequest.Ack;
                    default:
                        throw new ArgumentException($"{request.GetType().Name} - Не верный тип аргумента");
                }

            }
        }

        /// <summary>
        /// Метод получение результата
        /// </summary>
        /// <param name="ack"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetResult(IAck ack, out IGetStateResult result)
        {
            using (var client = new HouseManagementPortsTypeAsyncClient(_binding, _remoteAddress)) {
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
    }
}
