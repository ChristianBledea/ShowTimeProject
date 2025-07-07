using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.DataAccess.Repositories.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ShowtimeDbContext _context;
        public GenericRepository(ShowtimeDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try 
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving entities.", ex);
            }
           
        }

        public async Task<T> GetById(int id)
        {
          
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            return entity;
        }

        public virtual async Task Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Update: {ex.Message}");
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error in Update: {ex.Message}");
                throw new InvalidOperationException("The entity was modified by another process. Please refresh and try again.", ex);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error in Update: {ex.Message}");
                throw new InvalidOperationException("Failed to update entity in database", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Update: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while updating the entity", ex);
            }
        }
    }
}
