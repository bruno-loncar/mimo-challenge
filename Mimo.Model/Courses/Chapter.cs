using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Chapter
    {
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
    }
}
