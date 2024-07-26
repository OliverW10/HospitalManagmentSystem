using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;

namespace HospitalManagmentSystem.Controllers
{
    internal interface IModuleLocator
    {
        IMenu GetLoginModule();
        IMenu GetDoctorModule(DoctorModel user);
        IMenu GetPatientModule(PatientModel user);
        IMenu GetAdminModule(AdminModel user);
    }
}
