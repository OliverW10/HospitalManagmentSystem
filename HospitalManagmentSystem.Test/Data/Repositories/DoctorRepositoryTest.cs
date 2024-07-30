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
    internal class DoctorRepositoryTest
    {
        private Mock<HospitalContext> _mockContext;
        private Mock<DbSet<DoctorModel>> _mockDoctorSet;
        private DoctorRepository _doctorRepository;
        private DoctorModel _testDoctor;

        [SetUp]
        public void Setup()
        {
            _testDoctor = new DoctorModel { Id = 3, User = TestData.TestUser };
            var doctors = new List<DoctorModel>
            {
                _testDoctor
            }.AsQueryable();

            _mockDoctorSet = MockDbSetHelper.CreateMockDbSet<DoctorModel>(doctors);
            _mockContext = new Mock<HospitalContext>();
            _mockContext.Setup(c => c.Doctors).Returns(_mockDoctorSet.Object);
            _mockContext.Setup(c => c.Set<DoctorModel>()).Returns(_mockDoctorSet.Object);
            _doctorRepository = new DoctorRepository(_mockContext.Object);
        }

        [Test]
        public void TestRepositoryGetById()
        {
            var doctor = _doctorRepository.GetById(3);

            Assert.That(doctor, Is.EqualTo(_testDoctor));
        }

        [Test]
        public void TestRepositoryGetById_WhenIdDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _doctorRepository.GetById(2);
            });
        }

        [Test]
        public void TestRepositorySetsDiscriminator()
        {
            var model = new DoctorModel { User = TestData.TestUser };
            _doctorRepository.Add(model);

            Assert.That(model.User.Discriminator, Is.EqualTo(UserType.Doctor));
        }

        [Test]
        public void TestGetRandomUsesGivenRandom()
        {
            var doctor1 = _doctorRepository.GetRandom(new Random(3));
            var doctor2 = _doctorRepository.GetRandom(new Random(3));

            Assert.That(doctor1, Is.EqualTo(doctor2));
            Assert.That(doctor1, Is.EqualTo(_testDoctor));
        }

        [Test]
        public void TestGetRandomNoElements()
        {
            var doctors = new List<DoctorModel>().AsQueryable();

            var mockDoctorSet = MockDbSetHelper.CreateMockDbSet<DoctorModel>(doctors);
            var mockContext = new Mock<HospitalContext>();
            mockContext.Setup(c => c.Doctors).Returns(mockDoctorSet.Object);
            var doctorRepository = new DoctorRepository(mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => doctorRepository.GetRandom(Random.Shared));
        }
    }
}
