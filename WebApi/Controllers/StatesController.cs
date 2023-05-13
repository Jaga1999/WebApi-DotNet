using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.States;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StatesController> _logger;

        public StatesController(IStatesRepository statesRepository, IMapper mapper, ILogger<StatesController> logger)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll() 
        { 
            var states = await _statesRepository.GetAll();

            if (states == null)
            {
                return NoContent();
            }

            var stateDto = _mapper.Map<List<StatesDto>>(states);

            return Ok(stateDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var state = await _statesRepository.Get(id);

            var stateDto = _mapper.Map<StatesDto>(state);

            if (stateDto == null)
            {
                return NoContent();
            }

            return Ok(stateDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public  async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesDto)
        {
            var result = _statesRepository.IsRecordExists(x => x.Name.ToLower().Trim() == statesDto.Name.ToLower().Trim());

            if (result)
            {
                return Conflict("State Already Exists in Database");
            }

            var state = _mapper.Map<States>(statesDto);

            await _statesRepository.Create(state);
            return CreatedAtAction("GetById", new { id = state.Id }, state);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto statesDto)
        {
            if (statesDto == null || id != statesDto.Id)
            {
                return BadRequest();
            }

            var state = _mapper.Map<States>(statesDto);

            await _statesRepository.Update(state);
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
            var state = await _statesRepository.Get(id);
            
            if(state == null)
            {
                return NotFound();
            }

            await _statesRepository.Delete(state);
            return NoContent();
        }


    }
}
