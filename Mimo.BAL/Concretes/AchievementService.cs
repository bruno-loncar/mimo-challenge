using Mimo.DAL.Abstractions;
using Mimo.Model.Achievements;
using Mimo.Model.Achievements.ViewModels;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimo.DAL.Concretes
{
    public class AchievementService : IAchievementService
    {

        #region Fields

        private readonly IAchievementRepo achievementRepo;
        private readonly ICourseRepo courseRepo;

        #endregion

        #region Constructors

        public AchievementService(IAchievementRepo achievementRepo, ICourseRepo courseRepo)
        {
            this.achievementRepo = achievementRepo;
            this.courseRepo = courseRepo;
        }

        #endregion

        #region Public methods

        public async Task<List<Achievement>> GetAchievementsForUser(int userId)
        {
            return await achievementRepo.GetAchievementsForUser(userId);
        }

        public async Task<List<AchievementCompletionVM>> GetAchievementCompletionForUser(int userId)
        {
            List<Achievement> achievements = await achievementRepo.GetAllAchievements();
            List<Achievement> achievementsForUser = await achievementRepo.GetAchievementsForUser(userId);

            int userLessonCount = (await courseRepo.GetCompletedLessonsForUserDistinct(userId)).Count;
            int userChapterCount = (await courseRepo.GetCompletedChaptersForUser(userId)).Count;

            List<AchievementCompletionVM> achievementCompletions = new List<AchievementCompletionVM>();

            // Handle COMPLETED achievements
            achievementCompletions.AddRange(
                achievements.Intersect(achievementsForUser).Select(achievement => new AchievementCompletionVM()
                {
                    AchievementId = achievement.AchievementId,
                    Name = achievement.Name,
                    Completed = true,
                    Progress = GetAchievementProgressForUser(achievement, userLessonCount, userChapterCount, true),
                })
            );

            // Handle UNCOMPLETED achievements
            achievementCompletions.AddRange(
                achievements.Except(achievementsForUser).Select(achievement => new AchievementCompletionVM()
                {
                    AchievementId = achievement.AchievementId,
                    Name = achievement.Name,
                    Completed = false,
                    Progress = GetAchievementProgressForUser(achievement, userLessonCount, userChapterCount, false)
                })
            );

            return achievementCompletions
                .OrderByDescending(a => a.Completed)
                .ThenBy(a => a.AchievementId)
                .ToList();
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

        #endregion

        #region Helpers

        private int GetAchievementProgressForUser(Achievement achievement, int lessonCount, int chapterCount, bool completed)
        {
            return achievement.CompletionObject switch
            {
                CompletionObject.Lesson => lessonCount,
                CompletionObject.Chapter => chapterCount,
                CompletionObject.Course => completed ? 1 : 0,
                _ => 0,
            };
        }

        #endregion

    }
}
