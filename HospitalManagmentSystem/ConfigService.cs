namespace HospitalManagmentSystem
{
    internal class ConfigService
    {
        public string ApplcationName => "DOTNET Hospital Managment System";

        // TODO: read from config file or something
        public string SqlServerConnectionString => "Server=localhost;Database=AuditAPIDemo;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
