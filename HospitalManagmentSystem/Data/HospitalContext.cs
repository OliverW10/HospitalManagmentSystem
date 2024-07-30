using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HospitalManagmentSystem.Database
{
    internal interface IDbContextConfigurator
    {
        void Configure(DbContextOptionsBuilder options);
    }

    // Configues the DbContext to use a Sqlite3 file database
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

    // Configues the DbContext to use a local Microsoft Sql Server instance
    internal class LocalSqlServerConfigurator : IDbContextConfigurator
    {
        string _hostname { get; }
        string _dbName { get; }

        public LocalSqlServerConfigurator(string hostname = "localhost", string dbName = "HospitalAssignment")
        {
            _hostname = hostname;
            _dbName = dbName;
        }

        public void Configure(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($"Server={_hostname};Database={_dbName};Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }


    internal class HospitalContext : DbContext
    {

        public HospitalContext() : this(new SQLiteContextConfigurator())
        {
        }

        public HospitalContext(IDbContextConfigurator configurator)
        {
            _contextProvider = configurator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _contextProvider.Configure(options);
            // Logs the executed sql queries to the Output>Build tab in visual studio
            options.LogTo(message => Debug.WriteLine(message));
        }


        public virtual DbSet<AdminModel> Admins { get; set; }
        public virtual DbSet<AppointmentModel> Appointments { get; set; }
        public virtual DbSet<DoctorModel> Doctors { get; set; }
        public virtual DbSet<PatientModel> Patients { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }

        IDbContextConfigurator _contextProvider;
    }
}
