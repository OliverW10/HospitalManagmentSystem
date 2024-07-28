using HospitalManagmentSystem.Controllers;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagmentSystem.Services.Implementations
{
    // Exists to decouple modules from each other and to allow circular references between them
    internal class ModuleLocator(IServiceProvider serviceProvider) : IModuleLocator
    {
        public IMenu GetAdminModule(AdminModel user)
        {
            return () => serviceProvider.GetRequiredService<AdminModule>().GetAdminMainMenu(user);
        }

        public IMenu GetDoctorModule(DoctorModel user)
        {
            return () => serviceProvider.GetRequiredService<DoctorModule>().GetDoctorMainMenu(user);
        }

        public IMenu GetLoginModule()
        {
            return () => serviceProvider.GetRequiredService<LoginModule>().GetLoginMenu();
        }

        public IMenu GetPatientModule(PatientModel user)
        {
            return () => serviceProvider.GetRequiredService<PatientModule>().GetPatientMainMenu(user);
        }
    }
}
