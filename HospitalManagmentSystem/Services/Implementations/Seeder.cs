using HospitalManagmentSystem.Data;
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
            for (int i = 0; i < 3; i++)
            {
                _uow.AdminRepository.Add(new AdminModel { User = GetRandomizedUser() });
            }
            var doctors = new List<DoctorModel>();
            for (int i = 0; i < 5; i++)
            {
                var doctor = new DoctorModel { User = GetRandomizedUser() };
                _uow.DoctorRepository.Add(doctor);
                doctors.Add(doctor);
            }
            var patients = new List<PatientModel>();
            for (int i = 0; i < 20; i++)
            {
                var patient = new PatientModel { User = GetRandomizedUser(), Doctor = doctors.GetRandom(_rand) };
                _uow.PatientRepository.Add(patient);
                patients.Add(patient);
            }

            for (int i = 0; i < 30; i++)
            {
                AddAppointment(doctors, patients);
            }

            _uow.SaveChanges();
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

        UserModel GetRandomizedUser()
        {
            return new UserModel { Address = Address, Email = Email, Name = Name, Password = Password, Phone = Phone };
        }

        public void AddAppointment(IEnumerable<DoctorModel> doctors, IEnumerable<PatientModel> patients)
        {
            var doctor = doctors.GetRandom(_rand);
            var patient = patients.GetRandom(_rand);
            _uow.AppointmentRepository.Add(new AppointmentModel { Doctor = doctor, Patient = patient, Description = Description });
        }

        string[] FirstNames => new[] { "Normand", "Stan", "Irma", "Pierre", "Erick", "Jonty", "Kiara", "Ameera", "Bartosz", "Calvin", "Zak", "Kayleigh", "Tommy" };
        string[] LastNames => new[] { "Hooper", "Rose", "Melton", "Pena", "Kelley", "Callahan", "Warren", "Lowe", "Mercado" };
        string[] Streets => new[] { "Oak Way", "Marion Drive", "Boggess Street", "Pearlman Avenue", "Creekside Lane" };
        string[] Cities => new[] { "Cleveland", "San Luis Obispo", "Pensacola", "Weekiwachee Spgs.", "Marshalltown" };
        string[] EmailDomains => new[] { "mailing.com", "internet.net", "fakemail.gov.au", "pmail.com", "waaaa.io" };
        string[] AppointmentDescriptions => new[] { "sick", "unwell", "poorly", "ill", "disease ridden", "ailed", "malaise", "woozy", "bad", "afflicted" };

        IUnitOfWork _uow;
        IHasherService _hasher;
        Random _rand;
    }
}
