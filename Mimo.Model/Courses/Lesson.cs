using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Lesson
    {

        #region Properties

        [Key]
        public int LessonId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; }

        public virtual ICollection<UserLesson> UserLessons { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.LessonId == (obj as Lesson).LessonId;
        }

        public override int GetHashCode()
        {
            return this.LessonId % 7;
        }

        #endregion

    }
}
