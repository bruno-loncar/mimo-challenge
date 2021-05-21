using Mimo.Model.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mimo.Model.Courses
{
    public class Lesson
    {
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
    }
}
