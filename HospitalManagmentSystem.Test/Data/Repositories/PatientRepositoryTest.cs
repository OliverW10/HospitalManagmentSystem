using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Test.Data.Util;
using HospitalManagmentSystem.Test.Data.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Test.Data.Repositories
{
    internal class PatientRepositoryTest
    {
        private Mock<HospitalContext> _mockContext;
        private Mock<DbSet<PatientModel>> _mockPatientSet;
        private PatientRepository _PatientRepository;
        private PatientModel _testPatient;

        [SetUp]
        public void Setup()
        {
            _testPatient = new PatientModel { Id = 3, User = TestData.TestUser };
            var Patients = new List<PatientModel>
            {
                _testPatient
            }.AsQueryable();

            _mockPatientSet = MockDbSetHelper.CreateMockDbSet<PatientModel>(Patients);
            _mockContext = new Mock<HospitalContext>();
            _mockContext.Setup(c => c.Patients).Returns(_mockPatientSet.Object);
            _mockContext.Setup(c => c.Set<PatientModel>()).Returns(_mockPatientSet.Object);
            _PatientRepository = new PatientRepository(_mockContext.Object);
        }

        [Test]
        public void TestRepositoryGetById()
        {
            var Patient = _PatientRepository.GetById(3);

            Assert.That(Patient, Is.EqualTo(_testPatient));
        }

        [Test]
        public void TestRepositoryGetById_WhenIdDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _PatientRepository.GetById(2);
            });
        }

        [Test]
        public void TestRepositorySetsDiscriminator()
        {
            var model = new PatientModel { User = TestData.TestUser };
            _PatientRepository.Add(model);

            Assert.That(model.User.Discriminator, Is.EqualTo(UserType.Patient));
        }

        [Test]
        public void TestGetRandomUsesGivenRandom()
        {
            var Patient1 = _PatientRepository.GetRandom(new Random(3));
            var Patient2 = _PatientRepository.GetRandom(new Random(3));

            Assert.That(Patient1, Is.EqualTo(Patient2));
            Assert.That(Patient1, Is.EqualTo(_testPatient));
        }

        [Test]
        public void TestGetRandomNoElements()
        {
            var Patients = new List<PatientModel>().AsQueryable();

            var mockPatientSet = MockDbSetHelper.CreateMockDbSet<PatientModel>(Patients);
            var mockContext = new Mock<HospitalContext>();
            mockContext.Setup(c => c.Patients).Returns(mockPatientSet.Object);
            var PatientRepository = new PatientRepository(mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => PatientRepository.GetRandom(Random.Shared));
        }
    }
}
