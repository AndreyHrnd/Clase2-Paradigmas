using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerRank1.BusinessLogic.Repositories;
using HackerRank1.Entities.Models;

namespace HackerRank1.BusinessLogic.Services
{
    public class LibrariesService : ILibrariesService
    {
        private readonly ILibrariesRepository _librariesRepository;

        public LibrariesService(ILibrariesRepository librariesRepository)
        {
            _librariesRepository = librariesRepository;
        }

        public async Task<IEnumerable<Library>> Get(int[] ids)
        {
            return await _librariesRepository.Get(ids);
        }

        public async Task<Library> Add(Library library)
        {
            return await _librariesRepository.Add(library);
        }

        public async Task<IEnumerable<Library>> AddRange(IEnumerable<Library> projects)
        {
            return await _librariesRepository.AddRange(projects);
        }

        public async Task<Library> Update(Library library)
        {
            return await _librariesRepository.Update(library);
        }

        public async Task<bool> Delete(Library library)
        {
            // Complete the implementation
            throw new NotImplementedException();
        }
    }

    public interface ILibrariesService
    {
        Task<IEnumerable<Library>> Get(int[] ids);

        Task<Library> Add(Library library);

        Task<Library> Update(Library library);

        Task<bool> Delete(Library library);
    }
}
