using GestaoBiblioteca.Data;
using GestaoBiblioteca.Data.Entities;

namespace GestaoBiblioteca.Repositories
{
    public class LeitorRepository:GenericRepository<Leitor>, ILeitorRepository
    {
        private readonly DataContext _context;

        public LeitorRepository(DataContext context):base(context)
        {
            _context = context;
        }
    }
}
