using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mimo.API.Data;
using Mimo.DAL.Abstractions;
using Mimo.DAL.Data;
using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using Mimo.Model.Courses.ViewModels;
using Mimo.Model.Users;

namespace Mimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService lessonService;
        private readonly IAchievementService achievementService;

        public CourseController(ICourseService lessonService, IAchievementService achievementService)
        {
            this.lessonService = lessonService;
            this.achievementService = achievementService;
        }

        [HttpPost("lesson")]
        public async Task<ActionResult<int>> PostLesson([FromBody] PostLessonVM postLessonVM)
        {
            var userId = GetUserIdFromToken();

            var userLessonToAdd = new UserLesson()
            {
                LessonId = postLessonVM.LessonId,
                StartDate = postLessonVM.StartDate,
                CompletionDate = postLessonVM.CompletionDate,
                UserId = userId
            };

            UserLesson userLesson = await lessonService.InsertUserLesson(userLessonToAdd);
            userLesson.Lesson = null;

            return StatusCode(StatusCodes.Status201Created, userLesson);
        }

        //[HttpGet("chapters")]
        //public async Task<ActionResult<List<Chapter>>> GetChaptersForUser()
        //{
        //    var chapters = await achievementService.GetAchievementForCourseCompleted(1);
        //    return StatusCode(StatusCodes.Status200OK, chapters);
        //}

        //[HttpGet("lessons")]
        //public async Task<ActionResult<List<Lesson>>> GetLessonsForUser()
        //{
        //    var lessons = await lessonService.GetCompletedLessonsForUser(1);
        //    return StatusCode(StatusCodes.Status200OK, lessons);
        //}

        private int GetUserIdFromToken()
        {
            // TODO: Replace with Id from token
            return 1;
        }
    }
}
