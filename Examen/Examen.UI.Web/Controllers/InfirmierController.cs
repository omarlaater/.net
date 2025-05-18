using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Examen.UI.Web.Controllers
{
    public class InfirmierController : Controller
    {
        private readonly IInfirmierService _infirmierService;
        private readonly ILaboratoireService _laboratoireService;
        private readonly IPatientService _patientService;
        private readonly IBilanService _bilanService;

        public InfirmierController(
            IInfirmierService infirmierService,
            ILaboratoireService laboratoireService,
            IPatientService patientService,
            IBilanService bilanService)
        {
            _infirmierService = infirmierService;
            _laboratoireService = laboratoireService;
            _patientService = patientService;
            _bilanService = bilanService;
        }

        // GET: Infirmier
        public IActionResult Index()
        {
            var infirmiers = _infirmierService.GetMany(includeProperties: "Laboratoire");
            return View(infirmiers);
        }

        // GET: Infirmier/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infirmier = _infirmierService.GetById(id);
            if (infirmier == null)
            {
                return NotFound();
            }

            return View(infirmier);
        }

        // GET: Infirmier/Create
        public IActionResult Create()
        {
            ViewBag.Laboratoires = new SelectList(_laboratoireService.GetAll(), "LaboratoireId", "Intitule");
            ViewBag.Specialites = new SelectList(System.Enum.GetValues(typeof(Specialite)));
            return View();
        }

        // POST: Infirmier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Infirmier infirmier)
        {
            if (ModelState.IsValid)
            {
                _infirmierService.Add(infirmier);
                _infirmierService.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Laboratoires = new SelectList(_laboratoireService.GetAll(), "LaboratoireId", "Intitule", infirmier.LaboratoireId);
            ViewBag.Specialites = new SelectList(System.Enum.GetValues(typeof(Specialite)), infirmier.Specialite);
            return View(infirmier);
        }

        // GET: Infirmier/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infirmier = _infirmierService.GetById(id);
            if (infirmier == null)
            {
                return NotFound();
            }
            ViewBag.Laboratoires = new SelectList(_laboratoireService.GetAll(), "LaboratoireId", "Intitule", infirmier.LaboratoireId);
            ViewBag.Specialites = new SelectList(System.Enum.GetValues(typeof(Specialite)), infirmier.Specialite);
            return View(infirmier);
        }

        // POST: Infirmier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Infirmier infirmier)
        {
            if (id != infirmier.CodeInfirmier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _infirmierService.Update(infirmier);
                _infirmierService.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Laboratoires = new SelectList(_laboratoireService.GetAll(), "LaboratoireId", "Intitule", infirmier.LaboratoireId);
            ViewBag.Specialites = new SelectList(System.Enum.GetValues(typeof(Specialite)), infirmier.Specialite);
            return View(infirmier);
        }

        // GET: Infirmier/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infirmier = _infirmierService.GetById(id);
            if (infirmier == null)
            {
                return NotFound();
            }

            return View(infirmier);
        }

        // POST: Infirmier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var infirmier = _infirmierService.GetById(id);
            _infirmierService.Delete(infirmier);
            _infirmierService.Commit();
            return RedirectToAction(nameof(Index));
        }

        // GET: Infirmier/Patients/5
        public IActionResult Patients(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var infirmier = _infirmierService.GetById(id);
            if (infirmier == null)
            {
                return NotFound();
            }

            // Récupérer tous les bilans de cet infirmier
            var bilans = _bilanService.GetMany(b => b.CodeInfirmier == id);
            
            // Récupérer les patients uniques à partir des bilans
            var patientIds = bilans.Select(b => b.CodePatient).Distinct().ToList();
            var patients = new List<Patient>();
            
            foreach (var patientId in patientIds)
            {
                var patient = _patientService.GetById(patientId);
                if (patient != null)
                {
                    patients.Add(patient);
                }
            }

            ViewBag.InfirmierNom = infirmier.NomComplet;
            return View(patients);
        }
    }
}
