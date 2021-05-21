using Mimo.Model.Courses;
using Mimo.Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Achievements
{
    public class Achievement
    {

        #region Properties

        [Key]
        public int AchievementId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CompletionObject CompletionObject { get; set; }

        public int? AmountRequiredToComplete { get; set; }

        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.AchievementId == (obj as Achievement).AchievementId;
        }

        public override int GetHashCode()
        {
            return this.AchievementId % 7;
        }

        #endregion

    }
}
