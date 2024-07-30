using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Test.Data.Utils
{
    internal class TestData
    {
        public static UserModel TestUser => new UserModel { Address = "asdf", Email = "asdf", Name = "asdf", Password = new byte[] { 0x01 }, Phone = "0011" };
    }
}
