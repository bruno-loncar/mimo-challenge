using Mimo.Model.Courses;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface ICourseRepo
    {

        #region Lessions

        Task<UserLesson> InsertUserLesson(UserLesson userLesson);
        Task<Lesson> GetLesson(int lessonId);
        Task<List<Lesson>> GetLessonsForChapter(int chapterId);
        Task<List<Lesson>> GetCompletedLessonsForUserDistinct(int userId);

        #endregion

        #region Chapters
        Task<UserChapter> InsertUserChapter(UserChapter userChapter);
        Task<Chapter> GetChapter(int chapterId);
        Task<List<Chapter>> GetChaptersForCourse(int courseId);
        Task<List<Chapter>> GetCompletedChaptersForUser(int userId);

        #endregion

        #region Courses

        Task<UserCourse> InsertUserCourse(UserCourse userCourse);
        Task<Course> GetCourse(int courseId);
        Task<List<Course>> GetCompletedCoursesForUser(int userId);

        #endregion

    }
}