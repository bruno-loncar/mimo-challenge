using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class User
    {

        #region Properties

        [Key]
        public int UserId { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserLesson> UserLessons { get; set; }

        public virtual ICollection<UserChapter> UserChapters { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }

        public virtual ICollection<UserAchievement> UserAchievements { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.UserId == (obj as User).UserId;
        }

        public override int GetHashCode()
        {
            return this.UserId % 7;
        }

        #endregion

    }
}
