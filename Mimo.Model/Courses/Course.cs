using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
