using Mimo.DAL.Concretes;
using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.DAL.Abstractions
{
    public interface ILessonService
    {
        Task<UserLesson> InsertUserLesson(UserLesson userLesson);

        Task InsertUserChapter(int userId, int chapterId);

        Task InsertUserCourse(int userId, int courseId);

    }
}