using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Interfaces
{
    public interface IConvertor<TDtoObject> where TDtoObject : BaseTypes.DtoObject
    {
        TRequest ConvertToRequest<TRequest>(TDtoObject dto);

        TDtoObject ConvertToDto<TResultObject>(TResultObject resultObject);
    }
}
