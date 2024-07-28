using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Models;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Test.Services
{
    internal class SeederTest
    {
        [Test]
        public void TestSeederAdds()
        {
            // Arrange
            var uow = new Mock<IUnitOfWork>();

            var adminRepo = new Mock<IRepository<AdminModel>>();
            var doctorRepo = new Mock<IRepository<DoctorModel>>();
            var patientRepo = new Mock<IRepository<PatientModel>>();
            var appointmentRepo = new Mock<IRepository<AppointmentModel>>();
            uow.Setup(u => u.AdminRepository).Returns(adminRepo.Object);
            uow.Setup(u => u.DoctorRepository).Returns(doctorRepo.Object);
            uow.Setup(u => u.PatientRepository).Returns(patientRepo.Object);
            uow.Setup(u => u.AppointmentRepository).Returns(appointmentRepo.Object);

            var doctors = new List<DoctorModel>() { new DoctorModel { User = new UserModel() { Address = "", Email = "", Name = "", Password = Array.Empty<byte>(), Phone = "" } } };
            doctorRepo.Setup(r => r.GetAll()).Returns(doctors.AsQueryable());
            var patients = new List<PatientModel>() { new PatientModel { User = new UserModel() { Address = "", Email = "", Name = "", Password = Array.Empty<byte>(), Phone = "" } } };
            patientRepo.Setup(r => r.GetAll()).Returns(patients.AsQueryable());

            var hasher = new Mock<IHasherService>();
            var seeder = new Seeder(uow.Object, hasher.Object, new Random(Seed: 123));

            // Act
            seeder.Seed();

            // Assert
            adminRepo.Verify(r => r.Add(It.IsAny<AdminModel>()), Times.Exactly(3));
            doctorRepo.Verify(r => r.Add(It.IsAny<DoctorModel>()), Times.Exactly(5));
            patientRepo.Verify(r => r.Add(It.IsAny<PatientModel>()), Times.Exactly(10));
            appointmentRepo.Verify(r => r.Add(It.IsAny<AppointmentModel>()), Times.Exactly(15));
        }
    }
}
