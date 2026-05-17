using System;
using System.Collections.ObjectModel;
using System.Linq;
using EHealthAvalonia.Data;
using EHealthAvalonia.Models;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EHealthAvalonia.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _greeting = "Единая платформа электронного здравоохранения";

    [ObservableProperty]
    private ObservableCollection<string> _appointments = new();

    [ObservableProperty]
    private ObservableCollection<string> _hospitalizations = new();

    [ObservableProperty]
    private ObservableCollection<string> _doctorPatients = new();

    [ObservableProperty]
    private string _patientInfo = "";

    [ObservableProperty]
    private string _doctorInfo = "";

    [ObservableProperty]
    private bool _isPatientViewVisible = true;

    [ObservableProperty]
    private bool _isDoctorViewVisible = false;

    [ObservableProperty]
    private string _newDiagnosis = "";

    [ObservableProperty]
    private string _newTreatment = "";

    [ObservableProperty]
    private string _newMedication = "";

    [ObservableProperty]
    private string _newDosage = "";

    public MainWindowViewModel()
    {
        InitDb();
        LoadData();
    }

    private void InitDb()
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

    [RelayCommand]
    private void SwitchToPatientView()
    {
        IsPatientViewVisible = true;
        IsDoctorViewVisible = false;
        LoadData();
    }

    [RelayCommand]
    private void SwitchToDoctorView()
    {
        IsPatientViewVisible = false;
        IsDoctorViewVisible = true;
        LoadData();
    }

    [RelayCommand]
    private void AddMedicalRecord()
    {
        if (string.IsNullOrWhiteSpace(NewDiagnosis)) return;

        using var context = new ApplicationDbContext();
        var patient = context.Patients.FirstOrDefault();
        var doctor = context.Doctors.FirstOrDefault();
        if (patient == null || doctor == null) return;

        var record = new MedicalRecord
        {
            PatientId = patient.Id,
            DoctorId = doctor.Id,
            Diagnosis = NewDiagnosis,
            Treatment = NewTreatment
        };

        if (!string.IsNullOrWhiteSpace(NewMedication))
        {
            record.Prescriptions.Add(new Prescription
            {
                Medication = NewMedication,
                Dosage = NewDosage
            });
        }

        context.MedicalRecords.Add(record);
        context.SaveChanges();

        // Clear forms
        NewDiagnosis = "";
        NewTreatment = "";
        NewMedication = "";
        NewDosage = "";
    }

    [RelayCommand]
    private void LoadData()
    {
        using var context = new ApplicationDbContext();

        // Load Patient data
        var patient = context.Patients.Include(p => p.User).FirstOrDefault();
        if (patient != null)
        {
            PatientInfo = $"Пациент: {patient.User.FullName} (СНИЛС: {patient.User.Snils})";

            var apps = context.Appointments
                .Include(a => a.Doctor.User)
                .Where(a => a.PatientId == patient.Id)
                .Select(a => $"{a.Date:g} - Врач: {a.Doctor.User.FullName} [{a.Status}]")
                .ToList();

            Appointments.Clear();
            foreach (var a in apps) Appointments.Add(a);

            var hosps = context.Hospitalizations
                .Include(h => h.Clinic)
                .Where(h => h.PatientId == patient.Id)
                .Select(h => $"{h.RequestedDate:d} - {h.Clinic.Name} - {h.Reason} [{h.Status}]")
                .ToList();

            Hospitalizations.Clear();
            foreach (var h in hosps) Hospitalizations.Add(h);
        }

        // Load Doctor data
        var doctor = context.Doctors.Include(d => d.User).FirstOrDefault();
        if (doctor != null)
        {
            DoctorInfo = $"Врач: {doctor.User.FullName} (Специальность: {doctor.Specialty})";

            var pats = context.Patients
                .Include(p => p.User)
                .Select(p => $"{p.User.FullName} (Полис: {p.InsurancePolicy})")
                .ToList();

            DoctorPatients.Clear();
            foreach(var p in pats) DoctorPatients.Add(p);
        }
    }
}
