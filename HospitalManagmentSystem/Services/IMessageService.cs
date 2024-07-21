namespace HospitalManagmentSystem.Services
{
    internal interface IMessageService
    {
        void Send(string toAddress, string contents);
    }
}
