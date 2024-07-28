using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Services.Interfaces
{
    internal interface IModuleLocator
    {
        IMenu GetLoginModule();
        IMenu GetDoctorModule(DoctorModel user);
        IMenu GetPatientModule(PatientModel user);
        IMenu GetAdminModule(AdminModel user);
    }
}
