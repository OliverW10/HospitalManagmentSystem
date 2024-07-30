namespace HospitalManagmentSystem
{
    // In an actual application this stuff would be read from config file or something
    internal static class Constants
    {
        public static string ApplcationName => "DOTNET Hospital Managment System";

        public static string SqlServerConnectionString => "Server=localhost;Database=AuditAPIDemo;Trusted_Connection=True;TrustServerCertificate=True;";

        public static string EmailPassword
        {
            get
            {
                var password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
                if (password == null)
                {
                    throw new ArgumentNullException("Email password enviroment variable not set");
                }
                return password;
            }
        }
    }
}
