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
    internal class PatientRepository : BaseRepository<PatientModel>, IRepository<PatientModel>
    {
        public PatientRepository(HospitalContext ctx) : base(ctx) { }

        public IQueryable<PatientModel> GetAll()
        {
            return _context.Patients
                .Include(p => p.User)
                .Include(p => p.Doctor).ThenInclude(d => d.User);
        }
    }
}
