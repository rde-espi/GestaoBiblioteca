using GestaoBiblioteca.Data;
using GestaoBiblioteca.Data.Entities;

namespace GestaoBiblioteca.Repositories
{
    public class AutorRepository : GenericRepository<Autor>, IAutorRepository
    {
        private readonly DataContext _context;

        public AutorRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
