using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task Update(Country entity);
    }
}
