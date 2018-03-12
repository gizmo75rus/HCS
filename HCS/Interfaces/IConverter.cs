using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Interfaces
{
    public interface IConverter<TDto, TRequest, TResult>
        where TDto : IDto
        where TResult : IGetStateResult
    {
        IEnumerable<TRequest> ConvertToRequest(IEnumerable<TDto> dto);
        IEnumerable<TDto> ConvertToDto(TResult result);
    }
}
