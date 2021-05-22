using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimo.DAL.Abstractions;
using Mimo.DAL.Concretes;
using Mimo.Model.Courses.ViewModels;
using Mimo.Model.Users;
using System;
using System.Threading.Tasks;

namespace Mimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        #region Fields

        private const string errorMessage = "An error has occured.";

        private readonly ILogger<CourseController> logger;
        private readonly ICourseService lessonService;

        #endregion

        #region Constructors

        public CourseController(
            ILogger<CourseController> logger,
            ICourseService lessonService)
        {
            this.logger = logger;
            this.lessonService = lessonService;
        }

        #endregion

        #region Endpoints

        [HttpPost("lesson")]
        public async Task<ActionResult<int>> PostLesson([FromBody] PostLessonVM postLessonVM)
        {
            try
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
            catch (Exception e)
            {
                logger.LogError(e, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        #endregion

        #region Helpers

        private int GetUserIdFromToken()
        {
            // TODO: Replace with Id from token
            return 1;
        }

        #endregion

    }
}
