using GestaoBiblioteca.Data;
using GestaoBiblioteca.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public class EmprestimoRepository:GenericRepository<Emprestimo>,IEmprestimoRepository
    {
        private readonly DataContext _context;

        public EmprestimoRepository(DataContext context): base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateEmprestimoAsync(Emprestimo emprestimo)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == emprestimo.LeitorId);

            if(livro == null || !livro.Disponivel)
            {
                return false;
            }

            livro.Disponivel = false;

            await _context.Emprestimos.AddAsync(emprestimo);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DevolverAsync(int id)
        {
            var emprestimo = await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(emprestimo == null != emprestimo.DataDevolucao.HasValue)
            {
                return false;
            }

            emprestimo.DataDevolucao = DateTime.Now;
            emprestimo.Livro.Disponivel = true;

            return await _context.SaveChangesAsync() > 0;

        }

        public IQueryable<Emprestimo> GetAllWithDetails()
        {
            return _context.Emprestimos
                .Include(e => e.Leitor)
                .Include(e => e.Livro)
                .AsNoTracking();
        }

        public Task<Emprestimo> GetByIdWithDetailsAsync(int id)
        {
            return _context.Emprestimos
                .Include(e => e.Leitor)
                .Include(e => e.Livro)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
