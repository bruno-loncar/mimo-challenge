using Microsoft.EntityFrameworkCore;
using Mimo.DAL.Abstractions;
using Mimo.DAL.Data;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.DAL.Concretes
{
    public class LessonRepo : ILessonRepo
    {

        private readonly MimoDbContext context;

        public LessonRepo(MimoDbContext context)
        {
            this.context = context;
        }


        #region Lessions

        public async Task<UserLesson> InsertUserLesson(UserLesson userLesson)
        {
            context.UserLessons.Add(userLesson);
            await context.SaveChangesAsync();
            return userLesson;
        }

        public async Task<Lesson> GetLesson(int lessonId)
        {
            return await context.Lesson.FindAsync(lessonId);
        }

        public async Task<List<Lesson>> GetLessonsForChapter(int chapterId)
        {
            return await context.Lesson
                .Where(lesson => lesson.ChapterId == chapterId)
                .OrderBy(lesson => lesson.Position)
                .ToListAsync();
        }

        public async Task<List<Lesson>> GetLessonsForUser(int userId)
        {
            return await (from ul in context.UserLessons
                    join l in context.Lesson
                      on ul.LessonId equals l.LessonId
                    where ul.UserId == userId
                    select l).Distinct().ToListAsync();
        }

        #endregion

        #region Chapters

        public async Task<UserChapter> InsertUserChapter(UserChapter userChapter)
        {
            context.UserChapters.Add(userChapter);
            await context.SaveChangesAsync();
            return userChapter;
        }

        public async Task<Chapter> GetChapter(int chapterId)
        {
            return await context.Chapters.FindAsync(chapterId);
        }

        public async Task<List<Chapter>> GetChaptersForCourse(int courseId)
        {
            return await context.Chapters
                .Where(chapter => chapter.CourseId == courseId)
                .OrderBy(chapter => chapter.Position)
                .ToListAsync();
        }

        public async Task<List<Chapter>> GetCompletedChaptersForUser(int userId)
        {
            return await (from uc in context.UserChapters
                          join c in context.Chapters
                            on uc.ChapterId equals c.ChapterId
                          where uc.UserId == userId
                          select c).ToListAsync();
        }

        #endregion

        #region Courses

        public async Task<UserCourse> InsertUserCourse(UserCourse userCourse)
        {
            context.UserCourses.Add(userCourse);
            await context.SaveChangesAsync();
            return userCourse;
        }

        public async Task<Course> GetCourse(int courseId)
        {
            return await context.Courses.FindAsync(courseId);
        }

        public async Task<List<Course>> GetCompletedCoursesForUser(int userId)
        {
            return await (from uc in context.UserCourses
                          join c in context.Courses
                            on uc.CourseId equals c.CourseId
                          where uc.UserId == userId
                          select c).ToListAsync();
        }

        #endregion

    }
}
