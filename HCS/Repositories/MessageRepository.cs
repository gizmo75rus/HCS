using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HCS.BaseTypes;
using HCS.Dto;
using HCS.Interfaces;

namespace HCS.Repositories
{
    public class MessageRepository : IRepository<MessageDto>
    {
        List<MessageDto> _messages;
        public MessageRepository()
        {
            _messages = new List<MessageDto>();
        }
        public ActionResult Add(MessageDto entity)
        {
            _messages.Add(entity);
            return new ActionResult(ActionStatus.Ok);
        }

        public ActionResult Delete(MessageDto entity)
        {
            _messages.Remove(entity);
            return new ActionResult(ActionStatus.Ok);
        }

        public ActionResult Edit(MessageDto entity)
        {
            _messages[_messages.IndexOf(entity)] = entity;
            return new ActionResult(ActionStatus.Ok);
        }

        public ICollection<MessageDto> Get(Expression<Func<MessageDto, bool>> func)
        {
            return _messages.AsQueryable().Where(func).ToList();
        }

        public ICollection<MessageDto> GetAll()
        {
            return _messages;
        }

        public MessageDto GetById(int id)
        {
            return _messages.FirstOrDefault(x => x.ID == id);
        }

        public ICollection<MessageDto> Include(params Expression<Func<MessageDto, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public ICollection<MessageDto> Include(Expression<Func<MessageDto, bool>> func, params Expression<Func<MessageDto, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public MessageDto Include(int id, params Expression<Func<MessageDto, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
