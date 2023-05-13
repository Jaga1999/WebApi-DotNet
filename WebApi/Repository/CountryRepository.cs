using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class CountryRepository : GenericRepository<Country>,ICountryRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task Update(Country entity)
        {
            _dbcontext.Countries.Update(entity);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
