using System.Linq;
using Xunit;
using EHealthAvalonia.ViewModels;
using EHealthAvalonia.Data;
using Microsoft.EntityFrameworkCore;

namespace EHealthAvalonia.Tests
{
    public class ViewModelTests
    {
        [Fact]
        public void LoadData_PopulatesCollections()
        {
            var vm = new MainWindowViewModel();
            // LoadData is called in ctor
            Assert.NotEmpty(vm.PatientInfo);
            Assert.NotEmpty(vm.Appointments);
            Assert.NotEmpty(vm.Hospitalizations);
        }
    }
}
