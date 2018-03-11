using HCS.Globals;

namespace HCS.Interfaces
{
    public interface IProvider
    {
        /// <summary>
        /// Конечная точка провайдера
        /// </summary>
        EndPoints EndPoint { get; }

        /// <summary>
        /// Отправтиь запрос
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        IAck Send<T>(T request);

        /// <summary>
        /// Получить результат выплнения запроса
        /// </summary>
        /// <param name="ack"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetResult(IAck ack, out IGetStateResult result);
    }
}
