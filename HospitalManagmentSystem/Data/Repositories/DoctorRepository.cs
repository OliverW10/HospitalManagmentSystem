using HospitalManagmentSystem.Database;
using HospitalManagmentSystem.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Data.Repositories
{
    internal class DoctorRepository : BaseRepository<DoctorModel>, IRepository<DoctorModel>
    {
        public DoctorRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<DoctorModel> GetAll()
        {
            return _context.Doctors
                .Include(d => d.User)
                .Include(d => d.Patients).ThenInclude(p => p.User);
        }
    }
}
