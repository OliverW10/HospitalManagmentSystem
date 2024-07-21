using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HospitalManagmentSystem.Services
{
    internal class Seeder
    {
        public Seeder(IUnitOfWork unitOfWork, IHasherService hasher, Random rand)
        {
            _uow = unitOfWork;
            _hasher = hasher;
            _rand = rand;
        }

        public void Seed(Random rand)
        {
            for(int i = 0; i < 10; i++)
            {
                AddAdmin();
                AddDoctor();
                AddPatient();
            }

            for(int i = 0; i < 10; i++)
            {
                AddAppointMent();
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
        byte[] Password => _hasher.HashPassword(_rand.NextDouble().ToString());
        string Phone => string.Join("", Enumerable.Range(0, 8).Select(i => _rand.Next(10)));

        void AddAdmin()
        {
            _uow.AdminRepository.Add(new AdminModel { Address = Address, Email = Email, Name = Name, Password = Password, Phone = Phone });
        }

        void AddDoctor()
        {
            _uow.DoctorRepository.Add(new DoctorModel { Address = Address, Email = Email, Name = Name, Password = Password, Phone = Phone });
        }

        void AddPatient()
        {
            _uow.PatientRepository.Add(new PatientModel { Address = Address, Email = Email, Name = Name, Password = Password, Phone = Phone });
        }

        void AssignPatientToDoctor()
        {

        }

        void AddAppointMent()
        {
            var doctor = _uow.DoctorRepository.GetRandom(_rand);
            var patient = _uow.PatientRepository.GetRandom(_rand);
            _uow.AppointmentRepository.Add(new AppointmentModel { Doctor = doctor, Patient = patient });
        }

        string[] FirstNames => new[] {"Normand", "Stan", "Irma", "Pierre", "Erick"};
        string[] LastNames => new[] { "Hooper", "Rose", "Melton", "Pena", "Kelley"};
        string[] Streets => new[] { "Oak Way", "Marion Drive", "Boggess Street", "Pearlman Avenue", "Creekside Lane" };
        string[] Cities => new[] { "Cleveland", "San Luis Obispo", "Pensacola", "Weekiwachee Spgs.", "Marshalltown" };
        string[] EmailDomains => new[] { "mailing.com", "internet.net", "fakemail.gov.au", "pmail.com", "waaaa.io" };

        IUnitOfWork _uow;
        IHasherService _hasher;
        Random _rand;
    }
}
