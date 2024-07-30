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
    internal class AdminRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
            _testAdmin = new AdminModel { Id = 3, User = TestData.TestUser };
            var Admins = new List<AdminModel>
            {
                _testAdmin
            }.AsQueryable();

            _mockAdminSet = MockDbSetHelper.CreateMockDbSet<AdminModel>(Admins);
            _mockContext = new Mock<HospitalContext>();
            _mockContext.Setup(c => c.Admins).Returns(_mockAdminSet.Object);
            _mockContext.Setup(c => c.Set<AdminModel>()).Returns(_mockAdminSet.Object);
            _AdminRepository = new AdminRepository(_mockContext.Object);
        }

        [Test]
        public void TestRepositoryGetById()
        {
            var Admin = _AdminRepository.GetById(3);

            Assert.That(Admin, Is.EqualTo(_testAdmin));
        }

        [Test]
        public void TestRepositoryGetById_WhenIdDoesntExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _AdminRepository.GetById(2);
            });
        }

        [Test]
        public void TestRepositorySetsDiscriminator()
        {
            var model = new AdminModel { User = TestData.TestUser };
            _AdminRepository.Add(model);

            Assert.That(model.User.Discriminator, Is.EqualTo(UserType.Admin));
        }

        [Test]
        public void TestGetRandomUsesGivenRandom()
        {
            var Admin1 = _AdminRepository.GetRandom(new Random(3));
            var Admin2 = _AdminRepository.GetRandom(new Random(3));

            Assert.That(Admin1, Is.EqualTo(Admin2));
            Assert.That(Admin1, Is.EqualTo(_testAdmin));
        }

        [Test]
        public void TestGetRandomNoElements()
        {
            var Admins = new List<AdminModel>().AsQueryable();

            var mockAdminSet = MockDbSetHelper.CreateMockDbSet<AdminModel>(Admins);
            var mockContext = new Mock<HospitalContext>();
            mockContext.Setup(c => c.Admins).Returns(mockAdminSet.Object);
            var AdminRepository = new AdminRepository(mockContext.Object);

            Assert.Throws<InvalidOperationException>(() => AdminRepository.GetRandom(Random.Shared));
        }

        private Mock<HospitalContext> _mockContext;
        private Mock<DbSet<AdminModel>> _mockAdminSet;
        private AdminRepository _AdminRepository;
        private AdminModel _testAdmin;
    }
}
