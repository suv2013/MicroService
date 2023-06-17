using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.DTOS;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(AppDbContext context,IPlatformRepo repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPlatforms()
        {
          if (_context.Platforms == null)
          {
              return NotFound();
          }
            return await _context.Platforms.ToListAsync();
        }

        [HttpGet]
        [Route("/GetPlatformsFromRepo")]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatformsFromrepo()
        {
            if (_repository == null || _mapper == null)
            {
                return NotFound("");
            }
            var platformItem = _repository.GetPlatforms();

            var platformReadDTO = _mapper.Map<IEnumerable<PlatformReadDTO>>(platformItem);
            
            return Ok(platformReadDTO);
        }

        //GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatform(int id)
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            var platform = await _context.Platforms.FindAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return platform;
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<PlatformReadDTO> GetPlatformByID(int id)
        {
            if (_repository == null || _mapper == null)
            {
                return BadRequest("");
            }
            var platformItem = _repository.GetPlatformById(id);

            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
            }
            return NotFound();
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, Platform platform)
        {
            if (id != platform.Id)
            {
                return BadRequest();
            }

            _context.Entry(platform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Platform>> PostPlatform(Platform platform)
        {
          if (_context.Platforms == null)
          {
              return Problem("Entity set 'AppDbContext.Platforms'  is null.");
          }
            _context.Platforms.Add(platform);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlatform", new { id = platform.Id }, platform);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<PlatformReadDTO> CreatePlatForm(PlatformCreateDTO platformCreateDTO)
        {
            if(_repository == null && _mapper == null)
            {
                return BadRequest("");
            }

            var platformModel = _mapper.Map<Platform>(platformCreateDTO);
            if (platformModel != null)
            {
                _repository.CreatePlatform(platformModel);
                _repository.SaveChanges();

                var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);
                var response = CreatedAtAction(nameof(CreatePlatForm), new { platformReadDTO.Id, platformReadDTO });
                return response;
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new Platform record");
            }
        }


        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            _context.Platforms.Remove(platform);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatformExists(int id)
        {
            return (_context.Platforms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
