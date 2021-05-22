using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mimo.DAL.Abstractions;
using Mimo.Model.Achievements;
using Mimo.Model.Achievements.ViewModels;
using Mimo.Model.Users;

namespace Mimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            this.achievementService = achievementService;
        }

        //[HttpPost("lesson")]
        //public async Task<ActionResult<int>> PostLesson([FromBody] PostLessonVM postLessonVM)
        //{
        //    var userId = GetUserIdFromToken();

        //    var userLessonToAdd = new UserLesson()
        //    {
        //        LessonId = postLessonVM.LessonId,
        //        StartDate = postLessonVM.StartDate,
        //        CompletionDate = postLessonVM.CompletionDate,
        //        UserId = userId
        //    };

        //    UserLesson userLesson = await lessonService.InsertUserLesson(userLessonToAdd);
        //    userLesson.Lesson = null;

        //    return StatusCode(StatusCodes.Status201Created, userLesson);
        //}

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<AchievementCompletionVM>>> GetAchievementsForUser(int userId)
        {
            var chapters = await achievementService.GetAchievementCompletionForUser(userId);
            return StatusCode(StatusCodes.Status200OK, chapters);
        }


    }
}
