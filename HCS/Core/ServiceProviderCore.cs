using System;
using System.Collections.Generic;
using HCS.BaseTypes;
using HCS.Globals;
using HCS.Interfaces;
using HCS.Providers;

namespace HCS.Core
{
    public class ServiceProviderCore
    {
        Dictionary<EndPoints, IProvider> _providerLocator;
        Polices.SoupFaultPolicy _faultPolicy;

        public ServiceProviderCore(ClientConfig config)
        {
            _providerLocator = new Dictionary<EndPoints, IProvider>();
            _providerLocator.Add(EndPoints.HouseManagementAsync, new HouseManagmentProvider(config));
            _providerLocator.Add(EndPoints.BillsAsync, new BillsProvider(config));
            _providerLocator.Add(EndPoints.DeviceMeteringAsync, new DeviceMeteringProvider(config));
            _faultPolicy = new Polices.SoupFaultPolicy();
        }
        
        public Result<object> Send(ref IMessageType message) 
        {
            var result = new Result<object>();

            try {
                var provider = _providerLocator[message.EndPoint];
                if (provider == null)
                    throw new ArgumentOutOfRangeException($"Для конечной точки {Enum.GetName(typeof(EndPoints),message.EndPoint)} отсутсвует зарегистрированный провайдер");


                // Отправить запрос
                var ack = provider.Send(message.Request);
                message.SendDate = DateTime.Now;
                message.ResponceGUID = Guid.Parse(ack.MessageGUID);
                message.MessageStatus = MessageStatuses.SendOk;
                result.Value = ack;

            }catch(System.ServiceModel.FaultException<IFault> ex) {
                // Выбрать поведение в зависимости от кода ошибки soap ГИС 
                var action = _faultPolicy.GetAction(ex.Detail.ErrorCode);
                string errorMessage = ex.Detail.ErrorCode + " " + ex.Detail.ErrorMessage;

                switch (action) {
                    case Polices.Actions.NeedException:
                        message.MessageStatus = MessageStatuses.SendCriticalError;
                        throw new Exception($"При отправке запроса в ГИС ЖКХ произошла ошибка {errorMessage}");
                    case Polices.Actions.Abort:
                        message.MessageStatus = MessageStatuses.SendCriticalError;
                        break;
                    case Polices.Actions.TryAgain:
                        message.MessageStatus = MessageStatuses.SendError;
                        break;
                    default:
                        throw new NotImplementedException($"Поведение для действия {Enum.GetName(typeof(Polices.Actions), action)} не реализована");
                }
                result.HasError = true;
                result.Fault = ex.Detail;
            }
            catch(TimeoutException) {
                result.HasError = true;
                message.MessageStatus = MessageStatuses.SendTimeout;
            }
            catch(Exception ex) {
                result.HasError = true;
                message.MessageStatus = MessageStatuses.SendCriticalError;
                throw new Exception("При отправке запроса произошло не обработанное исключение", ex);
            }
            
            return result;
        }
    }
}
