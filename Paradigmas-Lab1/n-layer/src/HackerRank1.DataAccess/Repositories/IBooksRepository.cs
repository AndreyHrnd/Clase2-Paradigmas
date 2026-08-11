using System.Collections.Generic;
using System.Threading.Tasks;
using HackerRank1.Entities.Models;

namespace HackerRank1.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> Get(int libraryId, int[] ids);

        Task<Book> Add(Book book);

        Task<Book> Update(Book book);

        Task<bool> Delete(Book book);
    }
}
