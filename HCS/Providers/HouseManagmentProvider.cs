using System;
using System.Security.Cryptography.X509Certificates;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interaces;
using HCS.Service.Async.HouseManagement.v11_2_0_6;

namespace HCS.Providers {
    /// <summary>
    /// Служит для отпавки запросов к сервису
    /// </summary>
    /// <typeparam name="TRequest">тип запроса</typeparam>
    /// <typeparam name="TAck">тип объекта ответа</typeparam>
    public class HouseManagmentProvider:  ClientBaseType,IProviderBase {

        public HouseManagmentProvider(ClientConfig config) : base(config) {
            remoteAddress = GetEndpointAddress(Constants.EndPointPath.HouseManagementAsync);
        }

        /// <summary>
        /// Метод отравления запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns>Объект реализующий IAck</returns>
        IAck IProviderBase.Send<T>(T request)
        {
            using (var proxy = new HouseManagementPortsTypeAsyncClient(binding, remoteAddress)) {

                proxy.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());

                if (!base.config.IsPPAK) {
                    proxy.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    proxy.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!base.config.UseTunnel) {
                    proxy.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base.config.CertificateThumbprint);
                }

                switch (typeof(T).Name) {
                    case nameof(importAccountDataRequest):
                        return proxy.importAccountData(request as importAccountDataRequest).AckRequest.Ack;
                    case nameof(importCharterDataRequest):
                        return proxy.importCharterData(request as importCharterDataRequest).AckRequest.Ack;
                    case nameof(importContractDataRequest):
                        return proxy.importContractData(request as importContractDataRequest).AckRequest.Ack;
                    case nameof(importHouseOMSDataRequest):
                        return proxy.importHouseOMSData(request as importHouseOMSDataRequest).AckRequest.Ack;
                    case nameof(importHouseRSODataRequest):
                        return proxy.importHouseRSOData(request as importHouseRSODataRequest).AckRequest.Ack;
                    case nameof(importHouseUODataRequest):
                        return proxy.importHouseUOData(request as importHouseUODataRequest).AckRequest.Ack;
                    case nameof(importMeteringDeviceDataRequest1):
                        return proxy.importMeteringDeviceData(request as importMeteringDeviceDataRequest1).AckRequest.Ack;
                    case nameof(importNotificationDataRequest):
                        return proxy.importNotificationData(request as importNotificationDataRequest).AckRequest.Ack;
                    case nameof(importPublicPropertyContractRequest1):
                        return proxy.importPublicPropertyContract(request as importPublicPropertyContractRequest1).AckRequest.Ack;
                    case nameof(importSupplyResourceContractDataRequest):
                        return proxy.importSupplyResourceContractData(request as importSupplyResourceContractDataRequest).AckRequest.Ack;
                    case nameof(importVotingProtocolRequest1):
                        return proxy.importVotingProtocol(request as importVotingProtocolRequest1).AckRequest.Ack;
                    case nameof(exportAccountDataRequest):
                        return proxy.exportAccountData(request as exportAccountDataRequest).AckRequest.Ack;
                    case nameof(exportAccountIndividualServicesRequest1):
                        return proxy.exportAccountIndividualServices(request as exportAccountIndividualServicesRequest1).AckRequest.Ack;
                    case nameof(exportCAChDataRequest):
                        return proxy.exportCAChData(request as exportCAChDataRequest).AckRequest.Ack;
                    case nameof(exportHouseDataRequest):
                        return proxy.exportHouseData(request as exportHouseDataRequest).AckRequest.Ack;
                    case nameof(exportMeteringDeviceDataRequest1):
                        return proxy.exportMeteringDeviceData(request as exportMeteringDeviceDataRequest1).AckRequest.Ack;
                    case nameof(exportStatusCAChDataRequest):
                        return proxy.exportStatusCAChData(request as exportStatusCAChDataRequest).AckRequest.Ack;
                    case nameof(exportStatusPublicPropertyContractRequest1):
                        return proxy.exportStatusPublicPropertyContract(request as exportStatusPublicPropertyContractRequest1).AckRequest.Ack;
                    case nameof(exportSupplyResourceContractDataRequest):
                        return proxy.exportSupplyResourceContractData(request as exportSupplyResourceContractDataRequest).AckRequest.Ack;
                    case nameof(exportVotingProtocolRequest1):
                        return proxy.exportVotingProtocol(request as exportVotingProtocolRequest1).AckRequest.Ack;
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
        public bool GetResult(IAck ack, out IGetStateResult result)
        {
            result = null;

            using (var proxy = new HouseManagementPortsTypeAsyncClient(binding, remoteAddress)) {
                proxy.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());

                if (!base.config.IsPPAK) {
                    proxy.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    proxy.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!base.config.UseTunnel) {
                    proxy.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base.config.CertificateThumbprint);
                }

                try {

                    var responce = proxy.getState(new getStateRequest1 {
                        RequestHeader = new RequestHeader {
                            MessageGUID = ack.MessageGUID,
                            ItemElementName = ItemChoiceType.orgPPAGUID,
                            Item = config.OrgPPAGUID,
                            Date = DateTime.Now
                        }
                    });

                    if (responce.getStateResult.RequestState == 3) {
                        result = responce.getStateResult;
                        return true;
                    }
                }
                catch (System.ServiceModel.FaultException<Fault> ex) {
                    throw new Exception($"При получении результата выполнения запроса на ГИС произошла ошибка:{ex.Detail.ErrorCode},{ex.Detail.ErrorMessage}");
                }
                return false;
            }
        }
    }
}
