using System.Linq;
using System.Threading.Tasks;
using HackerRank1.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.Api.Features.Books
{
    [ApiController]
    [Route("api/libraries/{libraryId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public BooksController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int libraryId)
        {
            var books = await _libraryContext.Books
                .Where(b => b.LibraryId == libraryId)
                .ToListAsync();

            return Ok(books);
        }

        // POST remains out of scope for the architecture variant.
    }
}
