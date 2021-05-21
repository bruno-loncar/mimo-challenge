using Mimo.DAL.Abstractions;
using Mimo.DAL.Data;
using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.DAL.Concretes
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepo lessonRepo;
        private readonly IAchievementService achievementService;

        public LessonService(ILessonRepo lessonRepo, 
            IAchievementService achievementService)
        {
            this.lessonRepo = lessonRepo;
            this.achievementService = achievementService;
        }

        public async Task<UserLesson> InsertUserLesson(UserLesson userLesson)
        {


            userLesson = await lessonRepo.InsertUserLesson(userLesson);

            int userId = userLesson.UserId;
            List<Achievement> achievementsForUser = await achievementService.GetAchievementsForUser(userId);
            List<Lesson> lessonsForUser = await lessonRepo.GetLessonsForUser(userId);

            // Inserting LESSON achievement
            Achievement achievementForLessonCount = await achievementService.GetAchievementForLessonCount(lessonsForUser.Count);
            if (achievementForLessonCount != null && !achievementsForUser.Contains(achievementForLessonCount))
            {
                await achievementService.InsertUserAchievement(userId, achievementForLessonCount.AchievementId);
            }


            Lesson lesson = await lessonRepo.GetLesson(userLesson.LessonId);
            Chapter chapter = await lessonRepo.GetChapter(lesson.ChapterId);

            // Inserting CHAPTER achievement
            bool isLessonLastInChapter = await DidUserCompleteTheChapter(userId, chapter);
            if (isLessonLastInChapter)
            {
                List<Chapter> chaptersForUser = await lessonRepo.GetCompletedChaptersForUser(userId);
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


            Course course = await lessonRepo.GetCourse(chapter.CourseId);

            // Inserting COURSE achievement
            bool isChapterLastInCourse = await DidUserCompleteTheCourse(userId, course);
            if (isLessonLastInChapter && isChapterLastInCourse)
            {
                List<Course> coursesForUser = await lessonRepo.GetCompletedCoursesForUser(userId);
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

            return userLesson;
        }

        public async Task InsertUserChapter(int userId, int chapterId)
        {
            var userChapter = new UserChapter()
            {
                ChapterId = chapterId,
                UserId = userId,
                CompletionDate = DateTime.UtcNow
            };
            await lessonRepo.InsertUserChapter(userChapter);
        }

        public async Task InsertUserCourse(int userId, int courseId)
        {
            var userCourse = new UserCourse()
            {
                CourseId = courseId,
                UserId = userId,
                CompletionDate = DateTime.UtcNow
            };
            await lessonRepo.InsertUserCourse(userCourse);
        }

        private async Task<bool> DidUserCompleteTheChapter(int userId, Chapter chapter)
        {
            List<Lesson> lessonsForChapter = await lessonRepo.GetLessonsForChapter(chapter.ChapterId);
            List<Lesson> lessonsForUserForChapter = (await lessonRepo.GetLessonsForUser(userId))
                                                    .Where(lesson => lesson.ChapterId == chapter.ChapterId).ToList();
            
            return lessonsForChapter.Count == lessonsForUserForChapter.Count;
        }

        private async Task<bool> DidUserCompleteTheCourse(int userId, Course course)
        {
            List<Chapter> chaptersForCourse = await lessonRepo.GetChaptersForCourse(course.CourseId);
            List<Chapter> chaptersForUserForCourse = (await lessonRepo.GetCompletedChaptersForUser(userId))
                                                    .Where(chapter => chapter.CourseId == course.CourseId).ToList();

            return chaptersForCourse.Count == chaptersForUserForCourse.Count;
        }


    }
}
