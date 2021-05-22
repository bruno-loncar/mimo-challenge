using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Achievements.ViewModels
{
    public class AchievementCompletionVM
    {
        public int AchievementId { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public DateTime DateAquired { get; set; }
        public int Progress { get; set; }

    }
}
