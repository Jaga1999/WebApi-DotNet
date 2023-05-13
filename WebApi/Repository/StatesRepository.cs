using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class StatesRepository : IStatesRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public StatesRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task Create(States entity)
        {
            await _dbcontext.States.AddAsync(entity);
            await Save();
        }

        public async Task Delete(States entity)
        {
            _dbcontext.States.Remove(entity);
            await Save();
        }

        public async Task<List<States>> GetAll()
        {
            List<States> states = await _dbcontext.States.ToListAsync();
            return states;
        }

        public async Task<States> GetById(int id)
        {
            States state = await _dbcontext.States.FindAsync(id);
            return state;
        }

        public bool IsStateExists(string name)
        {
            var result = _dbcontext.States.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Update(States entity)
        {
            _dbcontext.States.Update(entity);
            await Save();
        }
    }
}
