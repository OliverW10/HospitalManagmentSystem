using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalManagmentSystem.Database
{
    internal interface IDbContextConfigurator
    {
        void Configure(DbContextOptionsBuilder options);
    }

    internal class SQLiteContextConfigurator : IDbContextConfigurator
    {
        string DbPath { get; }

        public SQLiteContextConfigurator(string filename = "hospital.db")
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

    internal class SqlServerContextConfigurator : IDbContextConfigurator//, IDesignTimeDbContextFactory<HospitalContext>
    {
        string HostName { get; }
        string DatabaseName { get; }

        public SqlServerContextConfigurator(string hostname = "localhost", string dbName = "HospitalAssignment")
        {
            HostName = hostname;
            DatabaseName = dbName;
        }

        public void Configure(DbContextOptionsBuilder options)
        {
        }

        public HospitalContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }


    internal class HospitalContext : DbContext
    {

       public HospitalContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost;Database=HospitalAssignment;Trusted_Connection=True;TrustServerCertificate=True;");
            //_contextProvider.Configure(options);
        }


        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
    }
}
