using Mimo.Model.Achievements;
using Mimo.Model.Courses;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Users
{
    public class UserAchievement
    {

        #region Properties

        [Key]
        public int UserAchievementId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }

        public DateTime CompletionDate { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.UserAchievementId == (obj as UserAchievement).UserAchievementId;
        }

        public override int GetHashCode()
        {
            return this.UserAchievementId % 7;
        }

        #endregion

    }
}
