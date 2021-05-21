using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mimo.API.Data;
using Mimo.Model.Achievements;
using Mimo.Model.Courses.ViewModels;
using Mimo.Model.Users;

namespace Mimo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MimoController : ControllerBase
    {
        private readonly MimoDbContext _context;

        public MimoController(MimoDbContext context)
        {
            _context = context;
        }

        [HttpPost("lesson")]
        public async Task<ActionResult<int>> PostLesson([FromBody] PostLessonVM postLessonVM)
        {
            var userId = GetUserIdFromToken();

            var userLessonToAdd = new UserLesson() {
                LessonId = postLessonVM.LessonId,
                StartDate = postLessonVM.StartDate,
                CompletionDate = postLessonVM.CompletionDate,
                UserId = userId
            };

            var userLesson = await _context.UserLessons.AddAsync(userLessonToAdd);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        private int GetUserIdFromToken()
        {
            // TODO: Replace with Id from token
            return 1;
        }
    }
}
