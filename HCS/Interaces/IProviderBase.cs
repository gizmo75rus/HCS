﻿using HCS.Globals;

namespace HCS.Interaces
{
    public interface IProviderBase
    {
        /// <summary>
        /// Конечная точка провайдера
        /// </summary>
        EndPointNames EndPointName { get; }

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
        bool GetResult(IAck ack, out IGetStateResult result);
    }
}