using Mimo.Model.Achievements;
using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserLesson> UserLessons { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
