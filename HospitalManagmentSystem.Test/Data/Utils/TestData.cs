using HospitalManagmentSystem.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Test.Data.Utils
{
    internal class TestData
    {
        public static UserModel TestUser => new UserModel { Address = "asdf", Email = "asdf", Name = "asdf", Password = new byte[] { 0x01 }, Phone = "0011" };
    }
}
