using HueFestivalTicket.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HueFestivalTicket.Repositories.RepositoryService
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includeExpressions != null)
            {
                query = includeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));
            }

            return await query.ToListAsync();
        }

        /*
        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }
        
        public async Task<List<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includeExpressions)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        */
    }
}
