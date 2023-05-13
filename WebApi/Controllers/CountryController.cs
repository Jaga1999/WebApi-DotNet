using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.DTO.Country;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository,IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var countries = await _countryRepository.GetAll();

            var countriesDto = _mapper.Map<List<CountryDto>>(countries);

            if (countries == null)
            {
                _logger.LogError("Error While try to get all record");
                return NoContent(); 
            }

            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                _logger.LogError($"Error While try to get record id:{id}");
                return NoContent();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody]CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsRecordExists(x => x.Name.ToLower().Trim() == countryDto.Name.ToLower().Trim());
            if (result)
            {
                return Conflict("Country Already Exists in Database");
            }

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Country>> Update(int id,[FromBody]UpdateCountryDto countryDto)
        {
            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            var county = _mapper.Map<Country>(countryDto);

            await _countryRepository.Update(county);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
