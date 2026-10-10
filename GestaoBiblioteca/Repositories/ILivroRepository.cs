using GestaoBiblioteca.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public interface ILivroRepository : IGenericRepository<Livro>
    {
        IQueryable<Livro> GetAllWithDetails();
        Task<Livro> GetByIdWithDetailsAsync(int id);
        Task CreateWithAuthorAsync(Livro livro, List<int> autoresIds);
        Task UpdateWithAuthorsAsync(Livro livro, List<int> autoresIds);
    }
}
