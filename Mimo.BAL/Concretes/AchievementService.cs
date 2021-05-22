using Mimo.DAL.Abstractions;
using Mimo.DAL.Data;
using Mimo.Model.Achievements;
using Mimo.Model.Achievements.ViewModels;
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
        private readonly ICourseRepo courseRepo;
        private readonly IUserRepo userRepo;

        public AchievementService(IAchievementRepo achievementRepo, ICourseRepo courseRepo)
        {
            this.achievementRepo = achievementRepo;
            this.courseRepo = courseRepo;
        }

        public async Task<List<Achievement>> GetAchievementsForUser(int userId)
        {
            return await achievementRepo.GetAchievementsForUser(userId);
        }

        public async Task<List<AchievementCompletionVM>> GetAchievementCompletionForUser(int userId)
        {
            List<AchievementCompletionVM> achievementCompletions = new List<AchievementCompletionVM>();

            List<Achievement> achievements = await achievementRepo.GetAllAchievements();
            List<Achievement> achievementsForUser = await achievementRepo.GetAchievementsForUser(userId);

            int userLessonCount = (await courseRepo.GetCompletedLessonsForUserDistinct(userId)).Count;
            int userChapterCount = (await courseRepo.GetCompletedChaptersForUser(userId)).Count;

            // Handle COMPLETED achievements
            achievementCompletions.AddRange(
                achievements.Intersect(achievementsForUser).Select(achievement => new AchievementCompletionVM()
                {
                    AchievementId = achievement.AchievementId,
                    Name = achievement.Name,
                    Completed = true,
                    Progress = GetAchievementProgressForUser(achievement, userLessonCount, userChapterCount, true)
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
