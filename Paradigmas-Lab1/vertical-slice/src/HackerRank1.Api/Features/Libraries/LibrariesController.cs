using System.Linq;
using System.Threading.Tasks;
using HackerRank1.DataAccess.Data;
using HackerRank1.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.Api.Features.Libraries
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public LibrariesController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libraries = await _libraryContext.Libraries.ToListAsync();
            return Ok(libraries);
        }

        [HttpGet("{libraryId}")]
        public async Task<IActionResult> Get(int libraryId)
        {
            var library = await _libraryContext.Libraries.FirstOrDefaultAsync(x => x.Id == libraryId);
            if (library == null)
                return NotFound();

            return Ok(library);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Library library)
        {
            await _libraryContext.Libraries.AddAsync(library);
            await _libraryContext.SaveChangesAsync();
            return Ok(library);
        }

        [HttpPut("{libraryId}")]
        public async Task<IActionResult> Update(int libraryId, Library library)
        {
            var existingLibrary = await _libraryContext.Libraries.FirstOrDefaultAsync(x => x.Id == libraryId);
            if (existingLibrary == null)
                return NotFound();

            existingLibrary.Name = library.Name;
            existingLibrary.Location = library.Location;
            _libraryContext.Libraries.Update(existingLibrary);
            await _libraryContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE remains out of scope for the architecture variant.
    }
}
