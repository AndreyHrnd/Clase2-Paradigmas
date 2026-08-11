using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerRank1.BusinessLogic.Repositories;
using HackerRank1.DataAccess.Data;
using HackerRank1.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace HackerRank1.DataAccess.Repositories
{
    public class LibrariesRepository : ILibrariesRepository
    {
        private readonly LibraryContext _libraryContext;

        public LibrariesRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Library>> Get(int[] ids)
        {
            var query = _libraryContext.Libraries.AsQueryable();

            if (ids != null && ids.Any())
                query = query.Where(x => ids.Contains(x.Id));

            return await query.ToListAsync();
        }

        public async Task<Library> Add(Library library)
        {
            await _libraryContext.Libraries.AddAsync(library);
            await _libraryContext.SaveChangesAsync();
            return library;
        }

        public async Task<IEnumerable<Library>> AddRange(IEnumerable<Library> libraries)
        {
            await _libraryContext.Libraries.AddRangeAsync(libraries);
            await _libraryContext.SaveChangesAsync();
            return libraries;
        }

        public async Task<Library> Update(Library library)
        {
            var libraryForChanges = await _libraryContext.Libraries.SingleAsync(x => x.Id == library.Id);
            libraryForChanges.Name = library.Name;
            libraryForChanges.Location = library.Location;

            _libraryContext.Libraries.Update(libraryForChanges);
            await _libraryContext.SaveChangesAsync();
            return library;
        }

        public Task<bool> Delete(Library library)
        {
            // Keep the pending lab implementation out of this architecture refactor.
            throw new NotImplementedException();
        }
    }
}
