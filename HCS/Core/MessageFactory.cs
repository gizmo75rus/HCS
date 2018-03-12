using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCS.Interfaces;
using HCS.Dto;
using HCS.Repositories;

namespace HCS.Core
{
    public class MessageFactory
    {
        IRepository<MessageDto> _repo;
        public MessageFactory()
        {
            _repo = new MessageRepository();
        }

        public void CreateMessage<TDto,TRequest>(object dto, IRequestConverter convertor)
        {
            var request = convertor.ConvertToRequest<TDto, TRequest>(dto);
        }
    }
}
