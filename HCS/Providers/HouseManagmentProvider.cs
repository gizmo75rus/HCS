using System;
using System.Security.Cryptography.X509Certificates;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Service.Async.HouseManagement.v11_2_0_6;

namespace HCS.Providers {
    /// <summary>
    /// Служит для отпавки запросов к сервису
    /// </summary>
    /// <typeparam name="TRequest">тип запроса</typeparam>
    /// <typeparam name="TAck">тип объекта ответа</typeparam>
    public class HouseManagmentProvider<TRequest, TAck> :  ClientBaseType
        where TRequest : class
        where TAck : AckRequestAck {

        public HouseManagmentProvider(ClientConfig config) : base(config) {
            remoteAddress = GetEndpointAddress(Constants.EndPointPath.HouseManagementAsync);
        }

        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <param name="requestObject">объект запроса</param>
        /// <returns></returns>
        public TAck Send(TRequest requestObject) {
            using (var proxy = new HouseManagementPortsTypeAsyncClient(binding, remoteAddress))
            {

                proxy.Endpoint.EndpointBehaviors.Add(new MyEndpointBehavior());

                if (!base.config.IsPPAK)
                {
                    proxy.ClientCredentials.UserName.UserName = Constants.UserAuth.Name;
                    proxy.ClientCredentials.UserName.Password = Constants.UserAuth.Passwd;
                }

                if (!base.config.UseTunnel)
                {
                    proxy.ClientCredentials.ClientCertificate.SetCertificate(
                     StoreLocation.CurrentUser,
                     StoreName.My,
                     X509FindType.FindByThumbprint,
                     base.config.CertificateThumbprint);
                }

                switch (typeof(TRequest).Name)
                {
                    case "importMeteringDeviceDataRequest1":
                        var request = requestObject as importMeteringDeviceDataRequest1;
                        return proxy.importMeteringDeviceData(request).AckRequest.Ack as TAck;
                    case "importAccountDataRequest":
                        var accountRequest = requestObject as importAccountDataRequest;
                        return proxy.importAccountData(accountRequest).AckRequest.Ack as TAck;
                    default:
                        throw new ArgumentException($"{requestObject.GetType().Name} - Не распознан");
                }

            }
        }
    }
}
