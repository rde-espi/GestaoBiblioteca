using GestaoBiblioteca.Models;
using GestaoBiblioteca.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Controllers
{
    [Authorize]
    public class EmprestimosController : Controller
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILeitorRepository _leitorRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimosController(IEmprestimoRepository emprestimoRepository, ILeitorRepository leitorRepository, ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _leitorRepository = leitorRepository;
            _livroRepository = livroRepository;
        }


        public IActionResult Index()
        {
            var emprestimos = _emprestimoRepository
                .GetAllWithDetails();

            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                emprestimos = emprestimos
                    .Where(e => e.Leitor != null && e.Leitor.UserId == userId);
            }

            return View(emprestimos.OrderByDescending(e => e.DataEmprestimo));
        }

        public async Task<IActionResult> Create()
        {
            var model = new EmprestimoViewModel
            {
                Leitores = await _leitorRepository.GetAll().OrderBy(l => l.Nome).ToListAsync(),
                Livros = await _livroRepository.GetAll().Where(l => l.Disponivel == true).OrderBy(l => l.Titulo).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(EmprestimoViewModel model)
        {
            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var leitor = await _leitorRepository.GetByUserIdAsync(userId);

                if (leitor == null)
                {
                    return NotFound("Não existe um leitor associado à conta autenticada.");
                }

                model.Emprestimo.LeitorId = leitor.Id;

                ModelState.Remove("Emprestimo.LeitorId");
            }
            if (model.Emprestimo.DataPrevistaDevolucao <= model.Emprestimo.DataEmprestimo)
            {
                ModelState.AddModelError("Emprestimo.DataPrevistaDevolucao", "A data prevista de devolução deve ser posterior à data do empréstimo");
            }
            
            if (ModelState.IsValid)
            {
                var criado = await _emprestimoRepository.CreateEmprestimoAsync(model.Emprestimo);

                if (criado)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Emprestimo.LivroId", "O livro selecionado já não está disponível");
            }

            model.Leitores = await _leitorRepository.GetAll().OrderBy(l => l.Nome).ToListAsync();
            model.Livros = await _livroRepository.GetAll().Where(l => l.Disponivel == true || l.Id == model.Emprestimo.LivroId).OrderBy(l => l.Titulo).ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var emprestimo = await _emprestimoRepository.GetByIdWithDetailsAsync(id.Value);
            if (emprestimo == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId) || emprestimo.Leitor.UserId != userId)
                {
                    return Forbid();
                }
            }

            

            return View(emprestimo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id)
        {
            var emprestimo = await _emprestimoRepository.GetByIdWithDetailsAsync(id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId) || emprestimo.Leitor?.UserId != userId)
                {
                    return Forbid();
                }
            }

            var devolvido = await _emprestimoRepository.DevolverAsync(id);

            if (!devolvido)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
