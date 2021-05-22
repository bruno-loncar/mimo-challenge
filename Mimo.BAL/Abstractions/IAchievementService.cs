using Mimo.Model.Achievements;
using Mimo.Model.Achievements.ViewModels;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface IAchievementService
    {
        Task InsertUserAchievement(int userId, int achievementId);
        Task<List<Achievement>> GetAchievementsForUser(int userId);
        Task<List<AchievementCompletionVM>> GetAchievementCompletionForUser(int userId);

        Task<Achievement> GetAchievementForLessonCount(int lessonCount);
        Task<Achievement> GetAchievementForChapterCount(int chapterCount);
        Task<Achievement> GetAchievementForCourseCompleted(int courseId);
    }
}