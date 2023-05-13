using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IStatesRepository : IGenericRepository<States>
    {
        Task Update(States entity);
    }
}
