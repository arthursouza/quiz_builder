using ApplicationCore.Entities.Common;
using ApplicationCore.Repositories;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : DatabaseEntity
    {
        private readonly DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<T> Queryable(ISpecification<T> spec, bool tracking = false)
        {
            var queryable = tracking ? context.Set<T>().AsQueryable() : context.Set<T>().AsQueryable().AsNoTracking();

            var queryableResultWithIncludes = spec.Includes
                .Aggregate(queryable,
                    (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            return secondaryResult
                            .Where(spec.Criteria)
                            .AsEnumerable();
        }
    }
}