using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Domain.Controllers;
using HospitalManagmentSystem.Domain.Services;
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
                .AddTransient<IMenuBuilderFactory, ConsoleMenuBuilderFactory>()
                .BuildServiceProvider();

            var loginController = services.GetRequiredService<LoginController>();
            IMenu? currentMenu = loginController.GetLoginMenu();
            while (currentMenu != null)
            {
                currentMenu = currentMenu();
            }
        }
    }
}
