using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Controllers
{
    internal class LoginModule
    {
        public LoginModule(IMenuBuilder menuFactory, IRepository<UserModel> userRepo, IUnitOfWork unitOfWork, IModuleLocator moduleFactory)
        {
            _menuFactory = menuFactory;
            _moduleFactory = moduleFactory;
            _users = userRepo;
            _uow = unitOfWork;
        }

        public Menu? GetLoginMenu()
        {
            int? loginId = null;
            byte[]? loginHashedPassword = null;
            var menu = _menuFactory
                .Title("Login");
            while (true)
            {
                menu.PromptForNumber("ID: ", id => loginId = id)
                    .PromptForPassword("Password: ", pw => loginHashedPassword = pw);

                if (GetUser(loginId, loginHashedPassword) is UserModel user)
                {
                    menu.Text("Valid Credentials");
                    // Delay to allow 'Valid Credentials' to be read
                    //Thread.Sleep(1000);
                    switch (user.Discriminator)
                    {
                        case UserType.Admin:
                            var admin = _uow.AdminRepository.Find(a => a.Id == loginId).First();
                            return () => _moduleFactory.GetAdminModule(admin);
                        case UserType.Doctor:
                            var doctor = _uow.DoctorRepository.Find(d => d.Id == loginId).First();
                            return () => _moduleFactory.GetDoctorModule(doctor);
                        case UserType.Patient:
                            var patient = _uow.PatientRepository.Find(p => p.Id == loginId).First();
                            return () => _moduleFactory.GetPatientModule(patient);
                    }
                }
                menu.Text("No matching account. Try again.");
            }
        }

        UserModel? GetUser(int? loginId, byte[]? hashedPassword)
        {
            return _users.Find(u => u.Id == loginId && u.Password == hashedPassword).FirstOrDefault();
        }

        public Menu? MainMenu()
        {
            throw new NotImplementedException();
        }

        IMenuBuilder _menuFactory;
        IModuleLocator _moduleFactory;
        IUnitOfWork _uow;
        IRepository<UserModel> _users;
    }
}
