using System.Collections.Generic;
using System.Threading.Tasks;
using HackerRank1.Entities.Models;

namespace HackerRank1.BusinessLogic.Repositories
{
    public interface ILibrariesRepository
    {
        Task<IEnumerable<Library>> Get(int[] ids);

        Task<Library> Add(Library library);

        Task<IEnumerable<Library>> AddRange(IEnumerable<Library> libraries);

        Task<Library> Update(Library library);

        Task<bool> Delete(Library library);
    }
}
