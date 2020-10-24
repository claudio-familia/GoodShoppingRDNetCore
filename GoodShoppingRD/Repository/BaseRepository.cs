using GoodShoppingRD.Models;
using GoodShoppingRD.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly GoodShoppingRdDbContext _context;
        public BaseRepository(GoodShoppingRdDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<T> Get(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Remove(Guid Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
