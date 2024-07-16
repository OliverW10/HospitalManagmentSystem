using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Database.Models
{
    public class UserModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required byte[] Password { get; set; }
        public required UserType Discriminator { get; set; }
    }

    public enum UserType
    {
        Doctor, Patient, Admin
    }
}
