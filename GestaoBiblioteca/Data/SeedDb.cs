using GestaoBiblioteca.Data.Entities;
using GestaoBiblioteca.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }



        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Leitor");

            var user = await _userHelper.GetUserByEmailAsync("reinaldo_7531@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Reinaldo",
                    LastName = "Souza",
                    Email = "reinaldo_7531@hotmail.com",
                    EmailConfirmed= true,
                    UserName = "reinaldo_7531@hotmail.com",
                    PhoneNumber = "936232511"
                };
                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user  in seeder");
                }
                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!await _context.Livros.AnyAsync())
            {
                await SeedLivrosReservaAsync();
            }
        }

        private async Task SeedLivrosReservaAsync()
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == "Literatura");

            if (categoria == null)
            {
                categoria = new Categoria
                {
                    Nome = "Literatura",
                    Descricao = "Obras literárias clássicas e contemporâneas"
                };

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
            }
            var livrosReserva = new[]
            {
                new { Titulo = "Dom Quixote", Autor = "Miguel de Cervantes", Ano = 1605 },
                new { Titulo = "Orgulho e Preconceito", Autor = "Jane Austen", Ano = 1813 },
                new { Titulo = "Os Maias", Autor = "Eça de Queirós", Ano = 1888 },
                new { Titulo = "1984", Autor = "George Orwell", Ano = 1949 },
                new { Titulo = "O Principezinho", Autor = "Antoine de Saint-Exupéry", Ano = 1943 }
            };
            foreach (var item in livrosReserva)
            {
                var livroExistente = await _context.Livros
                    .FirstOrDefaultAsync(l => l.Titulo == item.Titulo);

                if (livroExistente != null)
                {
                    continue;
                }

                var livro = new Livro
                {
                    Titulo = item.Titulo,
                    AnoPublicacao = item.Ano,
                    CategoriaId = categoria.Id,
                    Disponivel = true,
                    ImagemCapaDados = null,
                    ImagemCapaTipo = null
                };

                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();

                var autor = await _context.Autores
                    .FirstOrDefaultAsync(a => a.Nome == item.Autor);

                if (autor == null)
                {
                    autor = new Autor
                    {
                        Nome = item.Autor
                    };

                    _context.Autores.Add(autor);
                    await _context.SaveChangesAsync();
                }

                _context.LivrosAutores.Add(new LivroAutor
                {
                    LivroId = livro.Id,
                    AutorId = autor.Id
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
