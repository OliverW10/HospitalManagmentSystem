using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagmentSystem.Database
{
    internal interface IDbContextConfigurator
    {
        void Configure(DbContextOptionsBuilder options);    
    }

    internal class SQLiteContextConfigurator : IDbContextConfigurator
    {
        string DbPath { get; }

        SQLiteContextConfigurator(string filename = "hospital.db")
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, filename);
        }

        public void Configure(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }

    internal class SqlServerContextConfigurator : IDbContextConfigurator
    {
        string HostName { get; }
        string DatabaseName { get; }

        SqlServerContextConfigurator(string hostname = "localhost", string dbName = "HospitalAssignment")
        {
            HostName = hostname;
            DatabaseName = dbName;
        }

        public void Configure(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=HospitalAssignment;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }


    internal class HospitalContext : DbContext, IHospitalContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

        HospitalContext(IDbContextConfigurator contextProvider)
        {
            _contextProvider = contextProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _contextProvider.Configure(options);
        }

        IDbContextConfigurator _contextProvider;
    }
}
