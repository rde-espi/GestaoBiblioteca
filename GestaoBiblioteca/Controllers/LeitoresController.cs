using GestaoBiblioteca.Data.Entities;
using GestaoBiblioteca.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Controllers
{
    public class LeitoresController : Controller
    {
        private readonly ILeitorRepository _leitorRepository;

        public LeitoresController(ILeitorRepository leitorRepository)
        {
            _leitorRepository = leitorRepository;
        }

        public IActionResult Index()
        {
            return View(_leitorRepository.GetAll().OrderBy(l => l.Nome));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leitor leitor)
        {
            if (ModelState.IsValid)
            {
                await _leitorRepository.CreateAsync(leitor);
                return RedirectToAction(nameof(Index));
            }
            return View(leitor);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _leitorRepository.GetByIdAsync(id.Value);

            if (leitor == null)
            {
                return NotFound();
            }

            return View(leitor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _leitorRepository.GetByIdAsync(id.Value);

            if (leitor == null)
            {
                return NotFound();
            }

            return View(leitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leitor leitor)
        {
            if (id != leitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _leitorRepository.UpdateAsync(leitor);
                return RedirectToAction(nameof(Index));
            }

            return View(leitor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leitor = await _leitorRepository.GetByIdAsync(id.Value);

            if (leitor == null)
            {
                return NotFound();
            }

            return View(leitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leitor = await _leitorRepository.GetByIdAsync(id);

            if (leitor == null)
            {
                return NotFound();
            }

            await _leitorRepository.DeleteAsync(leitor);

            return RedirectToAction(nameof(Index));
        }
    }
}
