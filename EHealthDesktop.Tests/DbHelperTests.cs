using System;
using System.Linq;
using Xunit;
using EHealthDesktop;
using EHealthDesktop.Data;

namespace EHealthDesktop.Tests
{
    public class DbHelperTests : IDisposable
    {
        public DbHelperTests()
        {
            // Seed DB just for test logic coverage
            DbHelper.InitDb();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void InitDb_CreatesData()
        {
            var patient = DbHelper.GetFirstPatient();
            Assert.NotNull(patient);
            Assert.Equal("Иван Иванов", patient.User.FullName);

            var doctor = DbHelper.GetFirstDoctor();
            Assert.NotNull(doctor);
            Assert.Equal("Петр Петров (Кардиолог)", doctor.User.FullName);
        }

        [Fact]
        public void GetPatientAppointments_ReturnsData()
        {
            var patient = DbHelper.GetFirstPatient();
            var appointments = DbHelper.GetPatientAppointments(patient.Id);
            Assert.NotEmpty(appointments);
            Assert.Contains("Петр Петров (Кардиолог)", appointments[0]);
        }
    }
}
