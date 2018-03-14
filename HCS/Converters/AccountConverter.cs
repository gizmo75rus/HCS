using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Dto;
using HCS.Interfaces;
using HCS.Service.Async.HouseManagement.v11_10_0_13;

namespace HCS.Converters
{
    public class AccountConverter : IConverter<AccountDto, importAccountDataRequest, getStateResult>
    {
        public IEnumerable<AccountDto> ConvertToDto(getStateResult result)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<importAccountDataRequest> ConvertToRequest(IEnumerable<AccountDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
