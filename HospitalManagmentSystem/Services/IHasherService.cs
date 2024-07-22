namespace HospitalManagmentSystem.Services
{
    public interface IHasherService
    {
        byte[] HashPassword(string password);
    }
}
