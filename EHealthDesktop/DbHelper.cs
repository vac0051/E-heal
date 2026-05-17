using System;
using System.Linq;
using EHealthDesktop.Data;
using EHealthDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace EHealthDesktop
{
    public static class DbHelper
    {
        public static void InitDb()
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var pUser = new User { FullName = "Иван Иванов", Role = UserRole.Patient, Snils = "123-456-789 00" };
                var dUser = new User { FullName = "Петр Петров (Кардиолог)", Role = UserRole.Doctor, Snils = "987-654-321 00" };
                context.Users.AddRange(pUser, dUser);
                context.SaveChanges();

                var clinic = new Clinic { Name = "Городская Поликлиника №1", Address = "ул. Ленина, 1" };
                context.Clinics.Add(clinic);
                context.SaveChanges();

                var patient = new Patient { UserId = pUser.Id, BirthDate = new DateTime(1980, 1, 1), InsurancePolicy = "1111222233334444" };
                var doctor = new Doctor { UserId = dUser.Id, Specialty = "Кардиолог", ClinicId = clinic.Id };
                context.Patients.Add(patient);
                context.Doctors.Add(doctor);
                context.SaveChanges();

                context.Appointments.Add(new Appointment { PatientId = patient.Id, DoctorId = doctor.Id, Date = DateTime.Now.AddDays(1) });
                context.Hospitalizations.Add(new Hospitalization { PatientId = patient.Id, ClinicId = clinic.Id, Reason = "Обследование" });
                context.SaveChanges();
            }
        }

        public static Patient? GetFirstPatient()
        {
            using var context = new ApplicationDbContext();
            return context.Patients.Include(p => p.User).FirstOrDefault();
        }

        public static Doctor? GetFirstDoctor()
        {
            using var context = new ApplicationDbContext();
            return context.Doctors.Include(d => d.User).FirstOrDefault();
        }

        public static string[] GetPatientAppointments(int patientId)
        {
            using var context = new ApplicationDbContext();
            return context.Appointments
                .Include(a => a.Doctor.User)
                .Where(a => a.PatientId == patientId)
                .Select(a => $"{a.Date.ToShortDateString()} - {a.Doctor.User.FullName} [{a.Status}]")
                .ToArray();
        }

        public static string[] GetPatientHospitalizations(int patientId)
        {
            using var context = new ApplicationDbContext();
            return context.Hospitalizations
                .Include(h => h.Clinic)
                .Where(h => h.PatientId == patientId)
                .Select(h => $"{h.RequestedDate.ToShortDateString()} - {h.Clinic.Name} - {h.Reason} [{h.Status}]")
                .ToArray();
        }

        public static string[] GetDoctorPatients(int doctorId)
        {
            using var context = new ApplicationDbContext();
            // Just returning all patients for simplicity in prototype
            return context.Patients
                .Include(p => p.User)
                .Select(p => $"{p.User.FullName} (СНИЛС: {p.User.Snils})")
                .ToArray();
        }

        public static void AddRecord(int patientId, int doctorId, string diagnosis, string treatment)
        {
            using var context = new ApplicationDbContext();
            var record = new MedicalRecord
            {
                PatientId = patientId,
                DoctorId = doctorId,
                Date = DateTime.Now,
                Diagnosis = diagnosis,
                Treatment = treatment
            };
            context.MedicalRecords.Add(record);
            context.SaveChanges();
        }
    }
}
