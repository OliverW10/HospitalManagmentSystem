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
            new Mock<DbSet<AdminModel>>();
            Assert.Pass();
        }
    }
}