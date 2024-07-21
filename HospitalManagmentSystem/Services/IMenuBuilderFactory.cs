namespace HospitalManagmentSystem.Services
{
    internal interface IMenuBuilderFactory
    {
        IInitialMenuBuilder GetBuilder();
    }
}
