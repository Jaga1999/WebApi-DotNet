using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;

        public CountryController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            return _dbcontext.Countries.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Country> GetById(int id)
        {
            return _dbcontext.Countries.Find(id);
        }

        [HttpPost]
        public ActionResult<Country> Create([FromBody]Country country)
        {
            _dbcontext.Countries.Add(country);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult<Country> Update([FromBody]Country country)
        {
            _dbcontext.Countries.Update(country);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Country> DeleteById(int id)
        {
            var country = _dbcontext.Countries.Find(id);
            _dbcontext.Remove(country);
            _dbcontext.SaveChanges();
            return Ok();
        }
    }
}
