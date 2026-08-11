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
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _libraryContext;

        public BooksRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Book>> Get(int libraryId, int[] ids)
        {
            var query = _libraryContext.Books.AsQueryable().Where(b => b.LibraryId == libraryId);

            if (ids != null && ids.Any())
                query = query.Where(b => ids.Contains(b.Id));

            return await query.ToListAsync();
        }

        public Task<Book> Add(Book book)
        {
            // Keep the pending lab implementation out of this architecture refactor.
            throw new NotImplementedException();
        }

        public Task<Book> Update(Book book)
        {
            // Keep the pending lab implementation out of this architecture refactor.
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Book book)
        {
            // Keep the pending lab implementation out of this architecture refactor.
            throw new NotImplementedException();
        }
    }
}
