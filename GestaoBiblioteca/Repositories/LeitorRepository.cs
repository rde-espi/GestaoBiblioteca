using GestaoBiblioteca.Data;
using GestaoBiblioteca.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public class LeitorRepository : GenericRepository<Leitor>, ILeitorRepository
    {
        private readonly DataContext _context;

        public LeitorRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Task<Leitor> GetByUserIdAsync(string userId)
        {
            return _context.Leitores.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
