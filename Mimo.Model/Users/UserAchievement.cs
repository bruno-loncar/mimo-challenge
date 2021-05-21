using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Users
{
    public class UserAchievement
    {
        [Key]
        public int UserAchievementId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}
