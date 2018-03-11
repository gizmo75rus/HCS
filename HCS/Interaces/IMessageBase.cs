using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Globals;

namespace HCS.Interaces
{
    public interface IMessageBase
    {
        /// <summary>
        /// Конечная точка (адресат)
        /// </summary>
        EndPoints EndPoint { get; set; }

        /// <summary>
        /// Тип запроса
        /// </summary>
        Type RequestType { get; set; }

        /// <summary>
        /// Ожидаемый тип ответа
        /// </summary>
        Type ResultType { get; set; }
        
        /// <summary>
        /// Запрос
        /// </summary>
        object Request { get; set; }

        /// <summary>
        /// Ответ
        /// </summary>
        object Result { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        Guid MessageGUID { get; set; }

        /// <summary>
        /// Идентификатор ответа ГИС
        /// </summary>
        Guid ResponceGUID { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        DateTime SendDate { get; set; }

        /// <summary>
        /// Дата получения ответа
        /// </summary>
        DateTime ResponceDate { get; set; }

        /// <summary>
        /// Статус сообщения
        /// </summary>
        MessageStatuses MessageStatus { get; set; }

    }
}
