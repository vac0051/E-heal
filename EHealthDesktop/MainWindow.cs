using System;
using Terminal.Gui;

namespace EHealthDesktop
{
    public class MainWindow : Window
    {
        private FrameView _contentFrame;

        public MainWindow()
        {
            Title = "Единая платформа электронного здравоохранения (EHealth)";

            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("_File", new MenuItem [] {
                    new MenuItem("_Quit", "", () => Application.RequestStop())
                }),
                new MenuBarItem("_Views", new MenuItem [] {
                    new MenuItem("_Patient Dashboard", "", ShowPatientView),
                    new MenuItem("_Doctor Dashboard", "", ShowDoctorView)
                })
            });
            Add(menu);

            _contentFrame = new FrameView("Добро пожаловать")
            {
                X = 0,
                Y = 1, // Below menu
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            Add(_contentFrame);

            // Show patient view by default
            ShowPatientView();
        }

        private void ShowPatientView()
        {
            _contentFrame.RemoveAll();
            _contentFrame.Title = "Панель Пациента";

            var patient = DbHelper.GetFirstPatient();
            if (patient == null) return;

            var labelInfo = new Label($"Пациент: {patient.User.FullName}\nСНИЛС: {patient.User.Snils}")
            {
                X = 1,
                Y = 1
            };

            var appLabel = new Label("Мои приемы:") { X = 1, Y = Pos.Bottom(labelInfo) + 1 };
            var appointmentsList = new ListView(DbHelper.GetPatientAppointments(patient.Id))
            {
                X = 1,
                Y = Pos.Bottom(appLabel),
                Width = Dim.Fill() - 2,
                Height = 4,
                ColorScheme = Colors.Base
            };

            var hospLabel = new Label("Очередь на госпитализацию:") { X = 1, Y = Pos.Bottom(appointmentsList) + 1 };
            var hospList = new ListView(DbHelper.GetPatientHospitalizations(patient.Id))
            {
                X = 1,
                Y = Pos.Bottom(hospLabel),
                Width = Dim.Fill() - 2,
                Height = 4,
                ColorScheme = Colors.Base
            };

            _contentFrame.Add(labelInfo, appLabel, appointmentsList, hospLabel, hospList);
        }

        private void ShowDoctorView()
        {
            _contentFrame.RemoveAll();
            _contentFrame.Title = "Панель Врача";

            var doctor = DbHelper.GetFirstDoctor();
            if (doctor == null) return;

            var labelInfo = new Label($"Врач: {doctor.User.FullName}\nСпециальность: {doctor.Specialty}")
            {
                X = 1,
                Y = 1
            };

            var patLabel = new Label("Список пациентов:") { X = 1, Y = Pos.Bottom(labelInfo) + 1 };
            var patientsList = new ListView(DbHelper.GetDoctorPatients(doctor.Id))
            {
                X = 1,
                Y = Pos.Bottom(patLabel),
                Width = Dim.Fill() - 2,
                Height = 6,
                ColorScheme = Colors.Base
            };

            var addRecordBtn = new Button("Добавить запись")
            {
                X = 1,
                Y = Pos.Bottom(patientsList) + 1
            };

            addRecordBtn.Clicked += delegate {
                MessageBox.Query("В разработке", "Форма добавления записи будет здесь", "OK");
            };

            _contentFrame.Add(labelInfo, patLabel, patientsList, addRecordBtn);
        }
    }
}
