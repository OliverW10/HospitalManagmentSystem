namespace HospitalManagmentSystem.Services.Interfaces
{
    internal interface IMessageSender
    {
        void Send(string toAddress, string subject, string contents);
    }
}
