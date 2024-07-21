using HospitalManagmentSystem.Controllers;
using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagmentSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ServiceProvider services = new ServiceCollection()
                .AddDbContext<HospitalContext>()
                .AddTransient<LoginController>()
                .AddTransient<PatientController>()
                .AddTransient<DoctorController>()
                .AddTransient<AdminController>()
                .AddSingleton<ConfigService>()
                .AddSingleton<IDbContextConfigurator, SQLiteContextConfigurator>()
                .AddTransient<IMenuBuilderFactory, ConsoleMenuBuilderFactory>()
                .AddTransient<IMessageService, EmailService>()
                .BuildServiceProvider();

            Console.WriteLine(Console.OutputEncoding);
            //var emailer = services.GetRequiredService<IMessageService>();
            //emailer.Send("oliver.warrick2@gmail.com", "this is the contents of my email test test test");

            //var loginController = services.GetRequiredService<LoginController>();
            //IMenu? currentMenu = loginController.GetLoginMenu();
            //var state = new AppState();
            //while (currentMenu != null)
            //{
            //    currentMenu = currentMenu(state);
            //}

            //new DbSet<UserModel>();
        }
    }
}
