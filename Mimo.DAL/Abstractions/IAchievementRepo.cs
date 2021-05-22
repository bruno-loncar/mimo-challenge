using Mimo.Model.Achievements;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface IAchievementRepo
    {

        #region Public methods

        Task InsertUserAchievement(UserAchievement userAchievement);
        Task<List<Achievement>> GetAllAchievements();
        Task<List<Achievement>> GetAchievementsForUser(int userId);

        Task<Achievement> GetAchievementForLessonCount(int lessonCount);
        Task<Achievement> GetAchievementForChapterCount(int chapterCount);
        Task<Achievement> GetAchievementForCourseCompleted(int courseId);

        #endregion

    }
}