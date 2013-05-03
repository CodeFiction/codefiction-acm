using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DummyServices
{
    public class FakeRepository<T> : IRepository<T>
    {
        public T GetOne(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<T> expression)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public T Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}