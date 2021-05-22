using System;

namespace Mimo.Model.Achievements.ViewModels
{
    public class AchievementCompletionVM
    {

        #region Properties

        public int AchievementId { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public int Progress { get; set; }

        #endregion

    }
}
