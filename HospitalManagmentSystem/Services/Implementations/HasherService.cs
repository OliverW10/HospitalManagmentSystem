using System.Security.Cryptography;
using System.Text;
using HospitalManagmentSystem.Services.Interfaces;

namespace HospitalManagmentSystem.Services.Implementations
{
    internal class HasherService : IHasherService
    {
        public byte[] HashPassword(string password)
        {
            // TODO salt
            return Encoding.UTF8.GetBytes(password); // SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
