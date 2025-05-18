using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Examen.UI.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public IActionResult Index()
        {
            return View(_patientService.GetAll());
        }

        // GET: Patient/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.Add(patient);
                _patientService.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patient/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Patient patient)
        {
            if (id != patient.CodePatient)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _patientService.Update(patient);
                _patientService.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var patient = _patientService.GetById(id);
            _patientService.Delete(patient);
            _patientService.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
