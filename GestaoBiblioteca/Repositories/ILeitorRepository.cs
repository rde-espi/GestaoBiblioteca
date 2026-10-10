using GestaoBiblioteca.Data.Entities;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public interface ILeitorRepository : IGenericRepository<Leitor>
    {
        Task<Leitor> GetByUserIdAsync(string userId);
    }
}
