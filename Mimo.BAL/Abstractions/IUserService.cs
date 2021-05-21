using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
    }
}