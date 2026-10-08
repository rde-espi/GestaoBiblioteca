using GestaoBiblioteca.Data.Entities;
using GestaoBiblioteca.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBiblioteca.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            return View(_categoriaRepository.GetAll().OrderBy(c => c.Nome));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _categoriaRepository.CreateAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id.Value);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id.Value);

            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, Categoria categoria)
        {
            if(id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoriaRepository.UpdateAsync(categoria);
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        public async Task<IActionResult>Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id.Value);

            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            await _categoriaRepository.DeleteAsync(categoria);

            return RedirectToAction(nameof(Index));
        }
    }
}
