using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagmentSystem.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mock = new Mock<DbSet<AdminModel>>().Setup(s => s.);
            Assert.Pass();
        }
    }
}