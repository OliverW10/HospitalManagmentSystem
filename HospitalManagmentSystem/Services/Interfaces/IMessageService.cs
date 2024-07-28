namespace HospitalManagmentSystem.Services.Interfaces
{
    internal interface IMessageService
    {
        void Send(string toAddress, string contents);
    }
}
