using Mimo.Model.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Users
{
    public class UserLesson
    {
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
    }
}
