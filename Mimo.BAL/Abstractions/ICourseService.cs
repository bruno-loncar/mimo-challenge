using Mimo.Model.Users;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface ICourseService
    {

        #region Public methods

        Task<UserLesson> InsertUserLesson(UserLesson userLesson);

        #endregion

    }
}