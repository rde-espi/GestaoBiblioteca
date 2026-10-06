using GestaoBiblioteca.Data;
using GestaoBiblioteca.Models;

namespace GestaoBiblioteca.Repositories
{
    public class LivroRepository:GenericRepository<Livro>,ILivroRepository
    {
        private readonly DataContext _context;

        public LivroRepository(DataContext context):base(context) 
        {
            _context = context;
        }
    }
}
