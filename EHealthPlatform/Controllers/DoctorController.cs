using Microsoft.AspNetCore.Mvc;
using EHealthPlatform.Data;
using EHealthPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace EHealthPlatform.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            // Simulate doctor id 1
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .Where(a => a.DoctorId == 1)
                .ToListAsync();

            return View(appointments);
        }

        public async Task<IActionResult> MyPatients()
        {
            var patients = await _context.Patients.Include(p => p.User).ToListAsync();
            return View(patients);
        }

        public IActionResult AddRecord(int patientId)
        {
            var patient = _context.Patients.Include(p => p.User).FirstOrDefault(p => p.Id == patientId);
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecord(int patientId, string diagnosis, string treatment, string medication, string dosage)
        {
            // Simulate doctor id 1
            int currentDoctorId = 1;

            var record = new MedicalRecord
            {
                PatientId = patientId,
                DoctorId = currentDoctorId,
                Date = DateTime.UtcNow,
                Diagnosis = diagnosis ?? "Unknown",
                Treatment = treatment ?? "None"
            };

            if (!string.IsNullOrEmpty(medication) && !string.IsNullOrEmpty(dosage))
            {
                record.Prescriptions.Add(new Prescription
                {
                    Medication = medication,
                    Dosage = dosage
                });
            }

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
