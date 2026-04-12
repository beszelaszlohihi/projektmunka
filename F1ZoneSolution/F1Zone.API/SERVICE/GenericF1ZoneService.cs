using F1Zone.API.INTERFACE;
using F1ZoneLibrary.DATA;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace F1Zone.API.SERVICE
{
    public class GenericF1ZoneService<T> : IGenericF1ZoneService<T> where T : class
    {
        private readonly F1ZoneDbContext _context;
        private readonly DbSet<T> _set;

        public GenericF1ZoneService(F1ZoneDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
