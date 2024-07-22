using HospitalManagmentSystem.Controllers;
using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagmentSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ServiceProvider services = new ServiceCollection()
                .AddTransient<ConfigService>()
                .AddSingleton<IDbContextConfigurator, SqlServerContextConfigurator>()
                .AddDbContext<HospitalContext>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IRepository<AdminModel>, AdminRepository>()
                .AddTransient<IRepository<AppointmentModel>, AppointmentRepository>()
                .AddTransient<IRepository<DoctorModel>, DoctorRepository>()
                .AddTransient<IRepository<PatientModel>, PatientRepository>()
                .AddTransient<IMenuBuilderFactory, ConsoleMenuBuilderFactory>()
                .AddTransient<IMessageService, EmailService>()
                .AddTransient<IHasherService, HasherService>()
                .AddTransient<LoginMenu>()
                .AddTransient<PatientMenu>()
                .AddTransient<DoctorMenu>()
                .AddTransient<AdminMenu>()
                .AddTransient<Seeder>()
                .AddSingleton<Random>(Random.Shared)
                .BuildServiceProvider();

            //if (args.Any(arg => arg == "seed"))
            //{
            var seeder = services.GetRequiredService<Seeder>();
            seeder.Seed();

            //}

            //var emailer = services.GetRequiredService<IMessageService>();
            //emailer.Send("oliver.warrick2@gmail.com", "this is the contents of my email test test test");

            var loginController = services.GetRequiredService<LoginMenu>();
            IMenu? currentMenu = loginController.GetLoginMenu();
            var state = new AppState();
            while (currentMenu != null)
            {
                currentMenu = currentMenu(state);
            }
        }
    }
}
