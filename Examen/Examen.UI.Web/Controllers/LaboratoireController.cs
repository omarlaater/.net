using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Examen.UI.Web.Controllers
{
    public class LaboratoireController : Controller
    {
        private readonly IService<Laboratoire> _laboratoireService;

        public LaboratoireController(IService<Laboratoire> laboratoireService)
        {
            _laboratoireService = laboratoireService;
        }

        // GET: Laboratoire
        public IActionResult Index()
        {
            return View(_laboratoireService.GetAll());
        }

        // GET: Laboratoire/Details/5
        public IActionResult Details(int id)
        {
            var laboratoire = _laboratoireService.GetById(id);
            if (laboratoire == null)
            {
                return NotFound();
            }

            return View(laboratoire);
        }

        // GET: Laboratoire/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laboratoire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Laboratoire laboratoire)
        {
            if (ModelState.IsValid)
            {
                _laboratoireService.Add(laboratoire);
                _laboratoireService.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratoire);
        }

        // GET: Laboratoire/Edit/5
        public IActionResult Edit(int id)
        {
            var laboratoire = _laboratoireService.GetById(id);
            if (laboratoire == null)
            {
                return NotFound();
            }
            return View(laboratoire);
        }

        // POST: Laboratoire/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Laboratoire laboratoire)
        {
            if (id != laboratoire.LaboratoireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _laboratoireService.Update(laboratoire);
                _laboratoireService.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratoire);
        }

        // GET: Laboratoire/Delete/5
        public IActionResult Delete(int id)
        {
            var laboratoire = _laboratoireService.GetById(id);
            if (laboratoire == null)
            {
                return NotFound();
            }

            return View(laboratoire);
        }

        // POST: Laboratoire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var laboratoire = _laboratoireService.GetById(id);
            _laboratoireService.Delete(laboratoire);
            _laboratoireService.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
