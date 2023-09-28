using ApplicationCore.Specifications;

namespace ApplicationCore.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Update(T entity);

        Task AddAsync(T entity);

        void Save();

        IEnumerable<T> Queryable(ISpecification<T> spec, bool tracking = false);
    }
}