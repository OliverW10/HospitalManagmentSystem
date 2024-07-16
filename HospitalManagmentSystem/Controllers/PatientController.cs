using HospitalManagmentSystem.Database.Models;
using HospitalManagmentSystem.Services;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Controllers
{
    internal class PatientController
    {
        public PatientController(IMenuBuilderFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public IMenu? PatientMainMenu(AppState loggedInUser)
        {
            return _menuFactory.GetBuilder()
                .Title("Patient Menu")
                .Text("Welcome to ")
                .Text("Please choose an option:")
                .StartOptions()
                .Option(1, "List patient details", ListPatientDetailsMenu)
                .GetOptionResult();
        }

        public IMenu ListPatientDetailsMenu(AppState patient)
        {
            _menuFactory.GetBuilder()
                .Title("My Details")
                .Text("[name]'s details:");

            return PatientMainMenu;
        }

        IMenuBuilderFactory _menuFactory;
    }
}
