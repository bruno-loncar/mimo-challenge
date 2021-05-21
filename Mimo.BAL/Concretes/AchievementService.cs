using Mimo.DAL.Abstractions;
using Mimo.DAL.Data;
using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.DAL.Concretes
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepo achievementRepo;

        public AchievementService(IAchievementRepo achievementRepo)
        {
            this.achievementRepo = achievementRepo;
        }

        public async Task CheckForAchievementsUpdate(UserLesson userLesson)
        {


            //userLesson = await lessonRepo.InsertUserLesson(userLesson);


            //return await lessonRepo.InsertUserLesson(userLesson);
        }

        public async Task<List<Achievement>> GetAchievementsForUser(int userId)
        {
            return await achievementRepo.GetAchievementsForUser(userId);
        }

        public async Task InsertUserAchievement(int userId, int achievementId)
        {
            var userAchievement = new UserAchievement()
            {
                AchievementId = achievementId,
                UserId = userId,
                CompletionDate = DateTime.UtcNow
            };
            await achievementRepo.InsertUserAchievement(userAchievement);
        }


        public async Task<Achievement> GetAchievementForLessonCount(int lessonCount)
        {
            return await achievementRepo.GetAchievementForLessonCount(lessonCount);
        }

        public async Task<Achievement> GetAchievementForChapterCount(int chapterCount)
        {
            return await achievementRepo.GetAchievementForChapterCount(chapterCount);
        }

        public async Task<Achievement> GetAchievementForCourseCompleted(int courseId)
        {
            return await achievementRepo.GetAchievementForCourseCompleted(courseId);
        }

    }
}
