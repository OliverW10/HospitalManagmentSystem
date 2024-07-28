using HospitalManagmentSystem.Services.Interfaces;
using System.Text;

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
