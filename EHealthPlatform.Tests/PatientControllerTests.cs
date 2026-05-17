using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using EHealthPlatform.Controllers;
using EHealthPlatform.Data;
using EHealthPlatform.Models;

namespace EHealthPlatform.Tests
{
    public class PatientControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            var user = new User { Id = 1, FullName = "John Doe", Role = UserRole.Patient, Snils = "123456789012" };
            var docUser = new User { Id = 2, FullName = "Dr. Smith", Role = UserRole.Doctor, Snils = "987654321098" };

            var clinic = new Clinic { Id = 1, Name = "Test Clinic", Address = "123 Main St" };

            var patient = new Patient { Id = 1, UserId = 1, User = user, InsurancePolicy = "A123" };
            var doctor = new Doctor { Id = 1, UserId = 2, User = docUser, ClinicId = 1, Clinic = clinic, Specialty = "General" };

            var appointment = new Appointment { Id = 1, PatientId = 1, DoctorId = 1, Status = "Scheduled", Date = System.DateTime.Now };

            context.Users.AddRange(user, docUser);
            context.Clinics.Add(clinic);
            context.Patients.Add(patient);
            context.Doctors.Add(doctor);
            context.Appointments.Add(appointment);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Dashboard_ReturnsViewResult_WithAppointments()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new PatientController(context);

            // Act
            var result = await controller.Dashboard();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Appointment>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }
    }
}
