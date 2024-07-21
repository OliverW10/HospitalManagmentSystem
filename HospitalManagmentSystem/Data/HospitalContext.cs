using HospitalManagmentSystem.Data;
using HospitalManagmentSystem.Data.Repositories;
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


    internal class HospitalContext : DbContext, IUnitOfWork
    {

        HospitalContext(IDbContextConfigurator contextProvider)
        {
            _contextProvider = contextProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _contextProvider.Configure(options);
        }

        DbSet<UserModel> Users { get; set; }

        public IRepository<AdminModel> AdminRepository => throw new NotImplementedException();
        DbSet<AdminModel> Admins { get; set; }

        public IRepository<AppointmentModel> AppointmentRepository => throw new NotImplementedException();
        DbSet<AppointmentModel> Appointments { get; set; }

        public IRepository<DoctorModel> DoctorRepository => throw new NotImplementedException();
        DbSet<DoctorModel> Doctors { get; set; }

        public IRepository<PatientModel> PatientRepository => throw new NotImplementedException();
        DbSet<PatientModel> Patients { get; set; }

        IDbContextConfigurator _contextProvider;
    }
}
