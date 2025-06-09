using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exomine.Data.Models
{
    public class UserAchievement
    {
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey(nameof(Achievement))]
        public int AchievementId { get; set; }
        public Achievement? Achievement { get; set; }
    }
}