using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GoodShoppingRD.Repository.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<IEnumerable<T>> GetAll();
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        public Task<T> Get(Guid Id);
        public Task Remove(Guid Id);
    }
}
