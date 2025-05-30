using exomine.Data.Enums;

namespace exomine.Models
{
    public class AdminGenerateViewModel
    {
        public int Size { get; set; }
        public GridType Type { get; set; }
        public int Count { get; set; }
        public string Message { get; set; } = String.Empty;
    }
}