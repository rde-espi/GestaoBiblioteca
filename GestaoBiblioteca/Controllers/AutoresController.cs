using GestaoBiblioteca.Models;
using GestaoBiblioteca.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorRepository _autorRepository;

        public AutoresController(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }


        public IActionResult Index()
        {
            return View(_autorRepository.GetAll().OrderBy(x => x.Nome));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                await _autorRepository.CreateAsync(autor);
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id.Value);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id.Value);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _autorRepository.UpdateAsync(autor);
                return RedirectToAction(nameof(Index));
            }

            return View(autor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id.Value);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            await _autorRepository.DeleteAsync(autor);

            return RedirectToAction(nameof(Index));
        }
    }
}
