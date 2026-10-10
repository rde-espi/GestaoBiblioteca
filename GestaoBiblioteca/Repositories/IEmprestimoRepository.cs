using GestaoBiblioteca.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public interface IEmprestimoRepository : IGenericRepository<Emprestimo>
    {
        IQueryable<Emprestimo> GetAllWithDetails();
        Task<Emprestimo> GetByIdWithDetailsAsync(int id);
        Task<bool> CreateEmprestimoAsync(Emprestimo emprestimo);
        Task<bool> DevolverAsync(int id);
    }
}
