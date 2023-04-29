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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            var countries = _dbcontext.Countries.ToList();

            if (countries == null)
            {
                return NoContent(); 
            }

            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Country> GetById(int id)
        {
            var country = _dbcontext.Countries.Find(id);

            if (country == null)
            {
                return NoContent();
            }

            return Ok(country);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Country> Create([FromBody]Country country)
        {
            var result = _dbcontext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == country.Name.ToLower().Trim()).Any();
            if (result)
            {
                return Conflict("Country Already Exists in Database");
            }

            _dbcontext.Countries.Add(country);
            _dbcontext.SaveChanges();
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> Update(int id,[FromBody]Country country)
        {
            if (country == null || id != country.Id)
            {
                return BadRequest();
            }

            var countryFromDB = _dbcontext.Countries.Find(id);

            if(countryFromDB == null)
            {
                return NotFound();
            }

            countryFromDB.Name = country.Name;
            countryFromDB.ShortName = country.ShortName;
            countryFromDB.CountryCode = country.CountryCode;

            _dbcontext.Countries.Update(countryFromDB);
            _dbcontext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var country = _dbcontext.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }

            _dbcontext.Remove(country);
            _dbcontext.SaveChanges();
            return NoContent();
        }
    }
}
