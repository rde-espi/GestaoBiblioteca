using GestaoBiblioteca.Data;
using GestaoBiblioteca.Models;

namespace GestaoBiblioteca.Repositories
{
    public class CategoriaRepository:GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly DataContext _context;

        public CategoriaRepository(DataContext context) : base(context) 
        {
            _context = context;
            
        }
    }
}
