using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Course
    {

        #region Properties

        [Key]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.CourseId == (obj as Course).CourseId;
        }

        public override int GetHashCode()
        {
            return this.CourseId % 7;
        }

        #endregion

    }
}
