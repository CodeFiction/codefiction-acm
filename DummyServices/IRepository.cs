using System.Collections.Generic;
using System.Linq.Expressions;

namespace DummyServices
{
    public interface IRepository<T>
    {
        T GetOne(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<T> expression);
        bool Delete(object id);
        T Update(T t);
    }
}