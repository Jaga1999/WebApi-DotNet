using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    public class StatesRepository : GenericRepository<States>,IStatesRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public StatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task Update(States entity)
        {
            _dbcontext.States.Update(entity);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
