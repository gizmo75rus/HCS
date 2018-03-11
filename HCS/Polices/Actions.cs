using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Polices
{
    public enum Actions
    {
        /// <summary>
        /// Требуется бросить исключение
        /// </summary>
        NeedException,

        /// <summary>
        /// Требуется прервать выполнение операции
        /// </summary>
        Abort,

        /// <summary>
        /// Повторить действие
        /// </summary>
        TryAgain,

        /// <summary>
        /// Ошибка, требуется обработка на следующем уравне
        /// </summary>
        Error,

        /// <summary>
        /// Нет объекта
        /// </summary>
        Empty
    }
}
