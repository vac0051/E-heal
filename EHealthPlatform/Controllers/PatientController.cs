using Microsoft.AspNetCore.Mvc;
using EHealthPlatform.Data;
using EHealthPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace EHealthPlatform.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            // For now, we simulate a patient id (e.g. 1) as auth is not fully set up.
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Where(a => a.PatientId == 1)
                .ToListAsync();

            return View(appointments);
        }

        public async Task<IActionResult> MedicalHistory()
        {
            var history = await _context.MedicalRecords
                .Include(m => m.Doctor)
                .ThenInclude(d => d.User)
                .Include(m => m.Prescriptions)
                .Where(m => m.PatientId == 1)
                .ToListAsync();

            return View(history);
        }

        public async Task<IActionResult> HospitalizationQueue()
        {
            var queue = await _context.Hospitalizations
                .Include(h => h.Clinic)
                .Where(h => h.PatientId == 1)
                .ToListAsync();

            return View(queue);
        }

        public IActionResult BookAppointment()
        {
            var doctors = _context.Doctors.Include(d => d.User).ToList();
            return View(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointmentPost(int doctorId)
        {
            // Simulate current patient
            int currentPatientId = 1;

            var appointment = new Appointment
            {
                PatientId = currentPatientId,
                DoctorId = doctorId,
                Date = DateTime.Now.AddDays(1), // Schedule for tomorrow for prototype
                Status = "Scheduled"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
