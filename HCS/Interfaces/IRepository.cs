using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HCS.BaseTypes;

namespace HCS.Interfaces
{
    public interface IRepository<T> where T : class
    {
        ActionResult Add(T entity);

        ActionResult Edit(T entity);

        ActionResult Delete(T entity);

        T GetById(int id);

        ICollection<T> GetAll();

        ICollection<T> Get(Expression<Func<T, bool>> func);

        ICollection<T> Include(params Expression<Func<T, object>>[] includes);

        ICollection<T> Include(Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes);

        T Include(int id, params Expression<Func<T, object>>[] includes);
    }
}
