using HospitalManagmentSystem.Controllers;
using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services.Implementations;
using HospitalManagmentSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

namespace HospitalManagmentSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Configuring Services...");
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
                .AddTransient<TableLayoutService>()
                .AddTransient<IMessageService, EmailService>()
                .AddTransient<IHasherService, HasherService>()
                .AddTransient<LoginModule>()
                .AddTransient<PatientModule>()
                .AddTransient<DoctorModule>()
                .AddTransient<AdminModule>()
                .AddTransient<IModuleLocator, ModuleLocator>()
                .AddTransient<Seeder>()
                .AddSingleton<Random>(Random.Shared)
                .BuildServiceProvider();

            EnsureDatabasePopulated(services);

            //var emailer = services.GetRequiredService<IMessageService>();
            //emailer.Send("oliver.warrick2@gmail.com", "this is the contents of my email test test test");

            var threadHandle = new Thread(() =>
            {
                Console.Write("a");
                Thread.Sleep(1000);
            });

            var loginController = services.GetRequiredService<LoginModule>();
            Menu? currentMenu = loginController.GetLoginMenu();
            while (currentMenu != null)
            {
                currentMenu = currentMenu();
            }
        }

        static void EnsureDatabasePopulated(IServiceProvider services)
        {
            Console.WriteLine("Connecting to Database...");
            var userRepo = services.GetRequiredService<IRepository<UserModel>>();
            var count = userRepo.GetAll().Count();

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
            Console.WriteLine("Finished Seeding database");
        }
    }
}
