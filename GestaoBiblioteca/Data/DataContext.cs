using GestaoBiblioteca.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoBiblioteca.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroAutor> LivrosAutores { get; set; }
        public DbSet<Leitor> Leitores {  get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LivroAutor>()
                .HasKey(la => new { la.LivroId, la.AutorId }); //Cria chave compostas

            base.OnModelCreating(modelBuilder);
        }
    }
}
