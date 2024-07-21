using HospitalManagmentSystem.Data.Models;

namespace HospitalManagmentSystem.Database.Models
{
    public class UserData
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required byte[] Password { get; set; }
    }
}
