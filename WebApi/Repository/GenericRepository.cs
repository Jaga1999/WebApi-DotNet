using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsRecordExists(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
: class
    {
    }
}
