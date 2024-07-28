namespace HospitalManagmentSystem.Services.Interfaces
{
    public interface IHasherService
    {
        byte[] HashPassword(string password);
    }
}
