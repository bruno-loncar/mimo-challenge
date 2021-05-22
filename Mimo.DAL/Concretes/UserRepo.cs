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
    public class UserRepo : IUserRepo
    {

        private readonly MimoDbContext context;

        public UserRepo(MimoDbContext context)
        {
            this.context = context;
        }


        public async Task<User> GetUser(int userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public async Task<int> GetUserLessonCount(int userId)
        {
            return (await context.UserLessons.Where(ul => ul.UserId == userId).Distinct().ToListAsync()).Count;
        }

        public async Task<int> GetUserChapterCount(int userId)
        {
            return (await context.UserChapters.Where(uc => uc.UserId == userId).Distinct().ToListAsync()).Count;
        }

    }
}
