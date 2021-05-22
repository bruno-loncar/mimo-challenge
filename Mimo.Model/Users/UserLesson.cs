using Mimo.Model.Courses;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Users
{
    public class UserLesson
    {

        #region Properties

        [Key]
        public int UserLessonId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

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

            return this.UserLessonId == (obj as UserLesson).UserLessonId;
        }

        public override int GetHashCode()
        {
            return this.UserLessonId % 7;
        }

        #endregion

    }
}
