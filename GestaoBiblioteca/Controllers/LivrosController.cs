using GestaoBiblioteca.Data.Entities;
using GestaoBiblioteca.Models;
using GestaoBiblioteca.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Controllers
{
    [Authorize]
    public class LivrosController : Controller
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IAutorRepository _autorRepository;

        public LivrosController(ILivroRepository livroRepository, ICategoriaRepository categoriaRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _categoriaRepository = categoriaRepository;
            _autorRepository = autorRepository;
        }
        public IActionResult Index()
        {
            return View(_livroRepository.GetAllWithDetails().OrderBy(l => l.Categoria).OrderBy(c => c.Titulo));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new LivroViewModel
            {
                Livro = new Livro
                {
                    Disponivel = true
                },
                Categorias = _categoriaRepository.GetAll().OrderBy(c => c.Nome),
                Autores = _autorRepository.GetAll().OrderBy(a => a.Nome),
                AutoresSelecionados = new List<int>()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroViewModel model, IFormFile ImagemCapaUpload)
        {
            if (model.Livro.AnoPublicacao > DateTime.Now.Year)
            {
                ModelState.AddModelError("Livro.AnoPublicacao", "O ano de publicação não pode ser superior ao ano atual");
            }

            if (ImagemCapaUpload != null && ImagemCapaUpload.Length > 0)
            {
                var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/webp" };

                if (ImagemCapaUpload.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError(
                        "ImagemCapaUpload",
                        "A imagem não pode ultrapassar 2 MB.");
                }
                else if (!tiposPermitidos.Contains(ImagemCapaUpload.ContentType.ToLowerInvariant()))
                {
                    ModelState.AddModelError(
                        "ImagemCapaUpload",
                        "Apenas são permitidas imagens JPG, PNG ou WEBP.");
                }
            }

            if (ModelState.IsValid)
            {
                if (ImagemCapaUpload != null && ImagemCapaUpload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ImagemCapaUpload.CopyToAsync(memoryStream);

                        model.Livro.ImagemCapaDados = memoryStream.ToArray();
                        model.Livro.ImagemCapaTipo = ImagemCapaUpload.ContentType.ToLowerInvariant();
                    }
                }

                await _livroRepository.CreateWithAuthorAsync(
                    model.Livro,
                    model.AutoresSelecionados);

                return RedirectToAction(nameof(Index));
            }

            model.Categorias = _categoriaRepository.GetAll().OrderBy(c => c.Nome);
            model.Autores = _autorRepository.GetAll().OrderBy(a => a.Nome);

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepository.GetByIdWithDetailsAsync(id.Value);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepository.GetByIdWithDetailsAsync(id.Value);

            if (livro == null)
            {
                return NotFound();
            }

            var model = new LivroViewModel
            {
                Livro = livro,
                Categorias = _categoriaRepository.GetAll().OrderBy(c => c.Nome),
                Autores = _autorRepository.GetAll().OrderBy(a => a.Nome),
                AutoresSelecionados = livro.LivrosAutores
                .Select(la => la.AutorId)
                .ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LivroViewModel model, IFormFile ImagemCapaUpload)
        {
            if (model.Livro.AnoPublicacao > DateTime.Now.Year)
            {
                ModelState.AddModelError("Livro.AnoPublicacao", "O ano de publicação não pode ser superior ao ano atual");
            }
            if (ModelState.IsValid)
            {
                var livroAtual = await _livroRepository.GetByIdAsync(model.Livro.Id);

                if (livroAtual == null)
                {
                    return NotFound();
                }

                // Mantém a capa já guardada na base de dados
                model.Livro.ImagemCapaDados = livroAtual.ImagemCapaDados;
                model.Livro.ImagemCapaTipo = livroAtual.ImagemCapaTipo;

                // Se foi selecionada uma nova imagem, substitui a anterior
                if (ImagemCapaUpload != null && ImagemCapaUpload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ImagemCapaUpload.CopyToAsync(memoryStream);

                        model.Livro.ImagemCapaDados = memoryStream.ToArray();
                        model.Livro.ImagemCapaTipo = ImagemCapaUpload.ContentType.ToLowerInvariant();
                    }
                }

                await _livroRepository.UpdateWithAuthorsAsync(
                    model.Livro,
                    model.AutoresSelecionados);

                return RedirectToAction(nameof(Index));
            }

            model.Categorias = _categoriaRepository.GetAll().OrderBy(c => c.Nome);
            model.Autores = _autorRepository.GetAll().OrderBy(a => a.Nome);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepository.GetByIdWithDetailsAsync(id.Value);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _livroRepository.GetByIdWithDetailsAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            try
            {
                await _livroRepository.DeleteAsync(livro);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{livro.Titulo} possui empréstimos associados.";

                    ViewBag.ErrorMessage = $"O livro <strong>{livro.Titulo}</strong> não pode ser eliminado porque possui histórico de empréstimos.";
                }

                return View("Error");
            }
        }
    }
}
