using Mimo.Model.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimo.Model.Users
{
    public class UserChapter
    {

        #region Properties

        [Key]
        public int UserChapterId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.UserChapterId == (obj as UserChapter).UserChapterId;
        }

        public override int GetHashCode()
        {
            return this.UserChapterId % 7;
        }

        #endregion


    }
}
