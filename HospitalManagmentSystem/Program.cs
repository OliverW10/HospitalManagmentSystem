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
                .AddTransient<IMenuService, CommandLineMenuService>()
                //.AddKeyedTransient<IController, PatientMenuController>(1)
                .BuildServiceProvider();

            var menuStack = new Stack<IController>();
            menuStack.Push(LoginMenuController.GetLoginMenu(new CommandLineMenuService()));
            while (menuStack.Any())
            {
                var nextController = menuStack.Peek().Execute();
                if (nextController == null)
                {
                    menuStack.Pop();
                }
                else
                {
                    menuStack.Push(nextController);
                }
            }
        }
    }
}
