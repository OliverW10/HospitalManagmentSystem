using HospitalManagmentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagmentSystem.Database.Models
{
    public class AdminModel : UserData, IDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
