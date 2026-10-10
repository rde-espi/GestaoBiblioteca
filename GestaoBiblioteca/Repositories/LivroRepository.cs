using GestaoBiblioteca.Data;
using GestaoBiblioteca.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Repositories
{
    public class LivroRepository:GenericRepository<Livro>,ILivroRepository
    {
        private readonly DataContext _context;

        public LivroRepository(DataContext context):base(context) 
        {
            _context = context;
        }

        public async Task CreateWithAuthorAsync(Livro livro, List<int> autoresIds)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();

            if(autoresIds != null)
            {
                foreach(var autorId in autoresIds)
                {
                    var livroAutor = new LivroAutor
                    {
                        LivroId = livro.Id,
                        AutorId = autorId
                    };

                    await _context.LivrosAutores.AddAsync(livroAutor);
                }

                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Livro> GetAllWithDetails()
        {
            return _context.Livros
                .Include(l => l.Categoria)
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .AsNoTracking();
        }

        public Task<Livro> GetByIdWithDetailsAsync(int id)
        {
            return _context.Livros
                .Include(l => l.Categoria)
                .Include(l => l.LivrosAutores)
                .ThenInclude(la => la.Autor)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task UpdateWithAuthorsAsync(Livro livro, List<int> autoresIds)
        {
            var livroExistente = await _context.Livros
                .Include(l => l.LivrosAutores)
                .FirstOrDefaultAsync(l => l.Id == livro.Id);

            if(livroExistente == null)
            {
                return;
            }

            livroExistente.Titulo = livro.Titulo;
            livroExistente.AnoPublicacao = livro.AnoPublicacao;
            livroExistente.CategoriaId = livro.CategoriaId;
            livroExistente.Disponivel = livro.Disponivel;
            livroExistente.ImagemCapaDados = livro.ImagemCapaDados;
            livroExistente.ImagemCapaTipo = livro.ImagemCapaTipo;

            _context.LivrosAutores.RemoveRange(livroExistente.LivrosAutores);

            if(autoresIds != null)
            {
                foreach(var autorId in autoresIds)
                {
                    livroExistente.LivrosAutores.Add(new LivroAutor
                    {
                        LivroId = livro.Id,
                        AutorId = autorId
                    });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
