using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CountryRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task Create(Country entity)
        {
            await _dbcontext.Countries.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Country entity)
        {
            _dbcontext.Countries.Remove(entity);
            await Save() ;
        }

        public async Task<List<Country>> GetAll()
        {
            List<Country> countries = await _dbcontext.Countries.ToListAsync();
            return countries;
        }

        public async Task<Country> GetById(int id)
        {
            Country country = await _dbcontext.Countries.FindAsync(id);
            return country;
        }

        public bool IsCountryExists(string name)
        {
            var result = _dbcontext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Update(Country entity)
        {
            _dbcontext.Countries.Update(entity);
            await Save();
        }
    }
}
