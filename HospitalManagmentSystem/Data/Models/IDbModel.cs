using HospitalManagmentSystem.Database.Models;

namespace HospitalManagmentSystem.Data.Models
{
    public interface IDbModel
    {
        int Id { get; set; }
    }

    public interface IDbUserModel : IDbModel
    {
        UserModel User { get; set; }
    }
}
