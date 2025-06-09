namespace exomine.Models
{
    public class AllAchievementsViewModel
    {
        public List<AchievementDisplay> Achievements { get; set; } = [];

        public class AchievementDisplay
        {
            public string Name { get; set; } = String.Empty;
            public bool IsUnlocked { get; set; }
        }
    }
}