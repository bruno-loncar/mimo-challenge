using Mimo.Model.Achievements;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface IAchievementRepo
    {
        Task<List<Achievement>> GetAchievementsForUser(int userId);
        Task InsertUserAchievement(UserAchievement userAchievement);

        Task<List<Achievement>> GetAllAchievements();

        Task<Achievement> GetAchievementForLessonCount(int lessonCount);
        Task<Achievement> GetAchievementForChapterCount(int chapterCount);
        Task<Achievement> GetAchievementForCourseCompleted(int courseId);
    }
}