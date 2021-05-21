using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Chapter
    {

        #region Properties

        [Key]
        public int ChapterId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<UserChapter> UserChapters { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.ChapterId == (obj as Chapter).ChapterId;
        }

        public override int GetHashCode()
        {
            return this.ChapterId % 7;
        }

        #endregion

    }
}
