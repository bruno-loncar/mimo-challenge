using Mimo.DAL.Abstractions;
using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimo.DAL.Concretes
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo courseRepo;
        private readonly IAchievementService achievementService;

        public CourseService(ICourseRepo courseRepo, 
            IAchievementService achievementService)
        {
            this.courseRepo = courseRepo;
            this.achievementService = achievementService;
        }

        #region Public methods

        public async Task<UserLesson> InsertUserLesson(UserLesson userLesson)
        {
            userLesson = await courseRepo.InsertUserLesson(userLesson);
            await HandleLessonAchievements(userLesson);

            return userLesson;
        }

        #endregion

        #region LessonAchievements

        private async Task HandleLessonAchievements(UserLesson userLesson)
        {
            int userId = userLesson.UserId;
            List<Achievement> achievementsForUser = await achievementService.GetAchievementsForUser(userId);
            List<Lesson> lessonsForUser = await courseRepo.GetCompletedLessonsForUserDistinct(userId);

            // Handling LESSON achievement
            Achievement achievementForLessonCount = await achievementService.GetAchievementForLessonCount(lessonsForUser.Count);
            if (achievementForLessonCount != null && !achievementsForUser.Contains(achievementForLessonCount))
            {
                await achievementService.InsertUserAchievement(userId, achievementForLessonCount.AchievementId);
            }


            Lesson lesson = await courseRepo.GetLesson(userLesson.LessonId);
            Chapter chapter = await courseRepo.GetChapter(lesson.ChapterId);

            // Handling CHAPTER achievement
            bool isLessonLastInChapter = await DidUserCompleteTheChapter(userId, chapter);
            if (isLessonLastInChapter)
            {
                await HandleChapterAchievement(userId, achievementsForUser, chapter);
            }


            Course course = await courseRepo.GetCourse(chapter.CourseId);

            // Handling COURSE achievement
            bool isChapterLastInCourse = await DidUserCompleteTheCourse(userId, course);
            if (isLessonLastInChapter && isChapterLastInCourse)
            {
                await HandleCourseAchievement(userId, achievementsForUser, course);
            }
        }

        private async Task HandleChapterAchievement(int userId, List<Achievement> achievementsForUser, Chapter chapter)
        {
            List<Chapter> chaptersForUser = await courseRepo.GetCompletedChaptersForUser(userId);
            if (!chaptersForUser.Contains(chapter))
            {
                await InsertUserChapter(userId, chapter.ChapterId);

                Achievement achievementForChapterCount = await achievementService.GetAchievementForChapterCount(chaptersForUser.Count + 1);
                if (achievementForChapterCount != null && !achievementsForUser.Contains(achievementForChapterCount))
                {
                    await achievementService.InsertUserAchievement(userId, achievementForChapterCount.AchievementId);
                }
            }
        }

        private async Task HandleCourseAchievement(int userId, List<Achievement> achievementsForUser, Course course)
        {
            List<Course> coursesForUser = await courseRepo.GetCompletedCoursesForUser(userId);
            if (!coursesForUser.Contains(course))
            {
                await InsertUserCourse(userId, course.CourseId);

                Achievement achievementForCourseCompleted = await achievementService.GetAchievementForCourseCompleted(course.CourseId);
                if (achievementForCourseCompleted != null && !achievementsForUser.Contains(achievementForCourseCompleted))
                {
                    await achievementService.InsertUserAchievement(userId, achievementForCourseCompleted.AchievementId);
                }
            }
        }

        #endregion

        #region Helpers

        public async Task InsertUserChapter(int userId, int chapterId)
        {
            var userChapter = new UserChapter()
            {
                ChapterId = chapterId,
                UserId = userId,
                CompletionDate = DateTime.UtcNow
            };
            await courseRepo.InsertUserChapter(userChapter);
        }

        public async Task InsertUserCourse(int userId, int courseId)
        {
            var userCourse = new UserCourse()
            {
                CourseId = courseId,
                UserId = userId,
                CompletionDate = DateTime.UtcNow
            };
            await courseRepo.InsertUserCourse(userCourse);
        }

        private async Task<bool> DidUserCompleteTheChapter(int userId, Chapter chapter)
        {
            List<Lesson> lessonsForChapter = await courseRepo.GetLessonsForChapter(chapter.ChapterId);
            List<Lesson> lessonsForUserForChapter = (await courseRepo.GetCompletedLessonsForUserDistinct(userId))
                                                    .Where(lesson => lesson.ChapterId == chapter.ChapterId).ToList();

            return lessonsForChapter.Count == lessonsForUserForChapter.Count;
        }

        private async Task<bool> DidUserCompleteTheCourse(int userId, Course course)
        {
            List<Chapter> chaptersForCourse = await courseRepo.GetChaptersForCourse(course.CourseId);
            List<Chapter> chaptersForUserForCourse = (await courseRepo.GetCompletedChaptersForUser(userId))
                                                    .Where(chapter => chapter.CourseId == course.CourseId).ToList();

            return chaptersForCourse.Count == chaptersForUserForCourse.Count;
        }

        #endregion

    }
}
