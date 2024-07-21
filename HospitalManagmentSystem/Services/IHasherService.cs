namespace HospitalManagmentSystem.Services
{
    internal interface IHasherService
    {
        byte[] HashPassword(string password);
    }
}
