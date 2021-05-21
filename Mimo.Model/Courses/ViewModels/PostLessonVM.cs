using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Courses.ViewModels
{
    public class PostLessonVM
    {
        public int LessonId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }

    }
}
