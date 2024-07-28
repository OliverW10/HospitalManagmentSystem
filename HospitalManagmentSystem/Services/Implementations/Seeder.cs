using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Services.Implementations
{
    public class Seeder
    {
        public Seeder(IUnitOfWork unitOfWork, IHasherService hasher, Random rand)
        {
            _uow = unitOfWork;
            _hasher = hasher;
            _rand = rand;
        }

        public void Seed()
        {
            //DeleteAll(_uow.AppointmentRepository);
            //DeleteAll(_uow.AdminRepository);
            //DeleteAll(_uow.PatientRepository);
            //DeleteAll(_uow.DoctorRepository);

            List<DoctorModel> docs = new();
            List<PatientModel> patients = new();

            for (int i = 0; i < 3; i++)
            {
                AddAdmin();
            }
            for (int i = 0; i < 5; i++)
            {
                docs.Add(AddDoctor());
            }
            for (int i = 0; i < 20; i++)
            {
                patients.Add(AddPatient());
            }

            // https://stackoverflow.com/questions/46184937/dbcontext-not-returning-local-objects/46330600
            //_uow.SaveChanges();

            for (int i = 0; i < 15; i++)
            {
                patients[i].Doctor = docs.GetRandom(_rand);
            }
            for (int i = 0; i < 30; i++)
            {
                AddAppointment(docs, patients);
            }

            _uow.SaveChanges();
        }

        void DeleteAll<T>(IRepository<T> repo) where T : IDbModel
        {
            foreach (var val in repo.GetAll())
            {
                repo.Remove(val);
            }
        }

        string FirstName => FirstNames[_rand.Next(FirstNames.Length)];
        string LastName => LastNames[_rand.Next(LastNames.Length)];
        string Name => $"{FirstName} {LastName}";
        string Domain => EmailDomains[_rand.Next(EmailDomains.Length)];
        string Email => $"{FirstName}.{LastName}{_rand.Next(100)}@{Domain}";
        string Street => Streets[_rand.Next(Streets.Length)];
        string StreetNum => Math.Round(_rand.NextDouble() * Math.Pow(10, 1 + _rand.Next(3))).ToString();
        string City => Cities[_rand.Next(Cities.Length)];
        string Address => $"{StreetNum} {Street}, {City}";
        byte[] Password => _hasher.HashPassword("asdf");
        string Phone => string.Join("", Enumerable.Range(0, 8).Select(i => _rand.Next(10)));
        string Description => AppointmentDescriptions[_rand.Next(AppointmentDescriptions.Length)];

        UserModel GetUser()
        {
            return new UserModel { Address = Address, Email = Email, Name = Name, Password = Password, Phone = Phone };
        }

        void AddAdmin()
        {
            var user = GetUser();
            user.Discriminator = UserType.Admin;
            _uow.AdminRepository.Add(new AdminModel { User = user });
        }

        DoctorModel AddDoctor()
        {
            var user = GetUser();
            user.Discriminator = UserType.Doctor;
            var doctor = new DoctorModel { User = user };
            _uow.DoctorRepository.Add(doctor);
            return doctor;
        }

        PatientModel AddPatient()
        {
            var user = GetUser();
            user.Discriminator = UserType.Patient;
            var patient = new PatientModel { User = user };
            _uow.PatientRepository.Add(patient);
            return patient;
        }

        public void AddAppointment(IEnumerable<DoctorModel> doctors, IEnumerable<PatientModel> patients)
        {
            var doctor = doctors.GetRandom(_rand);
            var patient = patients.GetRandom(_rand);
            _uow.AppointmentRepository.Add(new AppointmentModel { Doctor = doctor, Patient = patient, Description = Description });
        }

        string[] FirstNames => new[] { "Normand", "Stan", "Irma", "Pierre", "Erick" };
        string[] LastNames => new[] { "Hooper", "Rose", "Melton", "Pena", "Kelley" };
        string[] Streets => new[] { "Oak Way", "Marion Drive", "Boggess Street", "Pearlman Avenue", "Creekside Lane" };
        string[] Cities => new[] { "Cleveland", "San Luis Obispo", "Pensacola", "Weekiwachee Spgs.", "Marshalltown" };
        string[] EmailDomains => new[] { "mailing.com", "internet.net", "fakemail.gov.au", "pmail.com", "waaaa.io" };
        string[] AppointmentDescriptions => new[] { "sick", "unwell", "poorly", "ill", "disease ridden", "ailed" };

        IUnitOfWork _uow;
        IHasherService _hasher;
        Random _rand;
    }
}
