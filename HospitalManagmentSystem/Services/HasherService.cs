using System.Security.Cryptography;
using System.Text;

namespace HospitalManagmentSystem.Services
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
