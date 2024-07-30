using HospitalManagmentSystem.Services.Interfaces;
using System.Text;

namespace HospitalManagmentSystem.Services.Implementations
{
    internal class HasherService : IHasherService
    {
        // This would be hashing and salting the password
        public byte[] HashPassword(string password)
        {
            return Encoding.UTF8.GetBytes(password);
        }
    }
}
