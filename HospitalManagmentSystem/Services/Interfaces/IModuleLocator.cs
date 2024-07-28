using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Services.Interfaces
{
    internal interface IModuleLocator
    {
        Menu GetLoginModule();
        Menu GetDoctorModule(DoctorModel user);
        Menu GetPatientModule(PatientModel user);
        Menu GetAdminModule(AdminModel user);
    }
}
