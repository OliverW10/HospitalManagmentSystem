using HospitalManagmentSystem.Controllers;
using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagmentSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Configuring Services...");

            // Maps interfaces to the conrete implementation to be used, can configure how the application will behave by chaging what is used
            // e.g. change IDbContextConfigurator to use a different db or IMenuBuilder to change how the ui is displayed
            ServiceProvider services = new ServiceCollection()
                .AddSingleton<IDbContextConfigurator, LocalSqlServerConfigurator>()
                .AddDbContext<HospitalContext>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IRepository<UserModel>, UserRepository>()
                .AddTransient<IRepository<AdminModel>, AdminRepository>()
                .AddTransient<IRepository<AppointmentModel>, AppointmentRepository>()
                .AddTransient<IRepository<DoctorModel>, DoctorRepository>()
                .AddTransient<IRepository<PatientModel>, PatientRepository>()
                .AddTransient<IMenuBuilder, ConsoleMenuBuilder>()
                .AddTransient<IMessageService, EmailService>()
                .AddTransient<IHasherService, HasherService>()
                .AddTransient<IModuleLocator, ModuleLocator>()
                .AddTransient<TableLayoutService>()
                .AddTransient<LoginModule>()
                .AddTransient<PatientModule>()
                .AddTransient<DoctorModule>()
                .AddTransient<AdminModule>()
                .AddTransient<Seeder>()
                .AddSingleton(Random.Shared)
                .BuildServiceProvider();

            EnsureDatabaseIsPopulated(services);

            //var emailer = services.GetRequiredService<IMessageService>();
            //emailer.Send("oliver.warrick2@gmail.com", "this is the contents of my email test test test");

            var loginController = services.GetRequiredService<LoginModule>();
            Menu? currentMenu = loginController.GetLoginMenu();
            while (currentMenu != null)
            {
                currentMenu = currentMenu();
            }
        }

        static void EnsureDatabaseIsPopulated(IServiceProvider services)
        {
            Console.WriteLine("Connecting to Database...");
            var userRepo = services.GetRequiredService<IRepository<UserModel>>();
            var count = userRepo.GetAll().Count();
            // If there are no User's, there cannot be any admins, doctors, patients or appointments due to the foreign key constraints
            // so the database is entirely empty and should be seeded
            if (count == 0)
            {
                SeedDatabase(services);
            }
        }

        static void SeedDatabase(IServiceProvider services)
        {
            Console.WriteLine("Starting Seeding database");
            var seeder = services.GetRequiredService<Seeder>();
            seeder.Seed();
            Console.Write("Finished Seeding Database, Press any key to start application...");
            Console.ReadKey();
        }
    }
}
