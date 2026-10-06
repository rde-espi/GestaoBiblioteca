using GestaoBiblioteca.Data;
using GestaoBiblioteca.Models;

namespace GestaoBiblioteca.Repositories
{
    public class EmprestimoRepository:GenericRepository<Emprestimo>,IEmprestimoRepository
    {
        private readonly DataContext _context;

        public EmprestimoRepository(DataContext context): base(context)
        {
            _context = context;
        }
    }
}
