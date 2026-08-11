using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerRank1.DataAccess.Repositories;
using HackerRank1.Entities.Models;

namespace HackerRank1.BusinessLogic.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IEnumerable<Book>> Get(int libraryId, int[] ids)
        {
            return await _booksRepository.Get(libraryId, ids);
        }

        public async Task<Book> Add(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }

        public async Task<Book> Update(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Book book)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }
    }

    public interface IBooksService
    {
        Task<IEnumerable<Book>> Get(int libraryId, int[] ids);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);

        Task<bool> Delete(Book book);
    }
}
