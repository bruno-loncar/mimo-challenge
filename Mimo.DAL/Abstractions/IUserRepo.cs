using Mimo.Model.Courses;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface IUserRepo
    {
        Task<User> GetUser(int lessonId);
        Task<int> GetUserLessonCount(int userId);
        Task<int> GetUserChapterCount(int userId);
    }
}