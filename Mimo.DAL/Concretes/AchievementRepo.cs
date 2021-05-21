using Microsoft.EntityFrameworkCore;
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
    public class AchievementRepo : IAchievementRepo
    {
        private readonly MimoDbContext context;

        public AchievementRepo(MimoDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Achievement>> GetAchievementsForUser(int userId)
        {
            return await (from ua in context.UserAchievements
                          join a in context.Achievements
                            on ua.AchievementId equals a.AchievementId
                          where ua.UserId == userId
                          select a)
                          .ToListAsync();
        }

        public async Task InsertUserAchievement(UserAchievement userAchievement)
        {
            context.UserAchievements.Add(userAchievement);
            await context.SaveChangesAsync();
        }

        public async Task<Achievement> GetAchievementForLessonCount(int lessonCount)
        {
            return await (from a in context.Achievements
                          where a.CompletionObject == CompletionObject.Lesson && 
                                    a.AmountRequiredToComplete == lessonCount
                          select a)
              .FirstOrDefaultAsync();
        }

        public async Task<Achievement> GetAchievementForChapterCount(int chapterCount)
        {
            return await (from a in context.Achievements
                          where a.CompletionObject == CompletionObject.Chapter && 
                                    a.AmountRequiredToComplete == chapterCount
                          select a)
                .FirstOrDefaultAsync();
        }

        public async Task<Achievement> GetAchievementForCourseCompleted(int courseId)
        {
            return await (from a in context.Achievements
                          where a.CompletionObject == CompletionObject.Course &&
                                    a.CourseId.HasValue && a.CourseId == courseId
                          select a)
                .FirstOrDefaultAsync();
        }


    }
}
