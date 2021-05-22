using Mimo.Model.Courses;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Users
{
    public class UserCourse
    {

        #region Properties

        [Key]
        public int UserCourseId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.UserCourseId == (obj as UserCourse).UserCourseId;
        }

        public override int GetHashCode()
        {
            return this.UserCourseId % 7;
        }

        #endregion

    }
}
