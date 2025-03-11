using Entites;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    //SQL Implementation of IRepository where TEntity is anything that drives with BaseEntity so basically all of our base entities.
    public class SQLRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ILogger<SQLRepository<TEntity>> _logger;
        protected DbContext _context;
        protected DbSet<TEntity> DbSet;

        //DbContext is set to resokve with instance of BBBankContext in DI
        public SQLRepository(DbContext context, ILogger<SQLRepository<TEntity>> logger)
        {
            //so _context goign to have BBBankContext because of DI
            _context = context;
            //DBSet set to incoming Generic Type
            DbSet = _context.Set<TEntity>();
            _logger = logger;
        }
        // function returns colection of base objects by filtering on navigation properties.
        // where navigation properties are passed as expression
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.ToListAsync();
        }
        // function returns single base object by filtering on ID
        public async Task<TEntity> GetAsync(string id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering on id and including one navigation property
        public async Task<TEntity> GetAsync(string id, string include = null)
        {
            var query = DbSet.Include(include);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering on id and by including multiple navigation properties passed in as string array
        public async Task<TEntity> GetAsync(string id, string[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering  on expression
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.SingleOrDefaultAsync(match);
        }
        // function returns collection of base object by filtering  on expression and including multiple navigation properties
        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, string[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.Where(match).ToListAsync();
        }
        // function ads entity to a DbSet and begins tracking
        public async Task AddAsync(TEntity t)
        {
            try
            {
                if (String.IsNullOrEmpty(t.Id))
                {
                    t.Id = Guid.NewGuid().ToString();
                }
                var xx = await DbSet.AddAsync(t);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    string entityName = entry.Entity.GetType().Name; 
                    _logger.LogError(ex, "Concurrency Exception on entity {EntityName}", entityName);
                }
                throw;
            }
            catch (ArgumentNullException ex)
            {
                // Handle null entity
                throw;
            }
            catch (Exception ex)
            {
                //log before sending up the chain.
                throw; // sends exception up the chain.
            }
            finally
            {
                // do something before passing up the chain. 
            }

        }
        // adds multiple items to an entity set and begin tracking
        public async Task<TEntity[]> BatchAddAsync(TEntity[] entities)
        {
            await this._context.AddRangeAsync(entities);
            return entities;
        }
        // function checks existance of an entity to be updated using its id.
        // if it exists it replaces its current values with new incoming values.
        public async Task<TEntity> UpdateAsync(TEntity updated)
        {
            if (updated == null)
                return null;

            var existing = await DbSet.FindAsync(updated.Id);
            if (existing != null)
            {
                _context.Entry(existing).State = EntityState.Detached;
                _context.Attach(updated);
                _context.Entry(updated).State = EntityState.Modified;

                // Ensure navigation properties are also updated
                foreach (var navigationProperty in _context.Entry(updated).Navigations)
                {
                    if (navigationProperty.CurrentValue != null) // Prevent null reference error
                    {
                        _context.Entry(navigationProperty.CurrentValue).State = EntityState.Modified;
                    }
                }
            }
            return updated;
        }
        // functions sets the value for delition
        public void DeleteAsync(TEntity t)
        {
            DbSet.Remove(t);
        }
        // function returns the count of and entity.
        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.AnyAsync(match);
        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, string include = null)
        {
            return await DbSet.Include(include).SingleOrDefaultAsync(match);
        }
        public async Task<List<TEntity>> GetPagedAsync(
    int pageIndex,
    int pageSize,
   string include = null)
        {
            var query = DbSet.AsQueryable();

            // Include navigation properties if provided
            if (include != null)
            {
              
                    query = query.Include(include);
                
            }

            // Apply pagination
            return await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}
