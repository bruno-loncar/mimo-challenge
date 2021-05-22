using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mimo.DAL.Abstractions;
using Mimo.Model.Achievements.ViewModels;
using Mimo.Model.Courses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {

        #region Fields

        private const string errorMessage = "An error has occured.";

        private readonly ILogger<AchievementController> logger;
        private readonly IAchievementService achievementService;

        #endregion

        #region Constructors

        public AchievementController(
            ILogger<AchievementController> logger,
            IAchievementService achievementService)
        {
            this.logger = logger;
            this.achievementService = achievementService;
        }

        #endregion

        #region Endpoints

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<AchievementCompletionVM>>> GetAchievementsForUser(int userId)
        {
            try
            {
                var chapters = await achievementService.GetAchievementCompletionForUser(userId);
                return StatusCode(StatusCodes.Status200OK, chapters);
            }
            catch (Exception e)
            {
                logger.LogError(e, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }

        }

        #endregion

    }
}
