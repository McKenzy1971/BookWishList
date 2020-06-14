using System;

namespace BookWishList.Models
{
    public enum Theme { Psychology, Novel, Photography, Thriller, SelfDiscovery, NonFiction }

    [Serializable]
    public class Book
    {
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Author { get; set; }
        public Theme Theme { get; set; } = Theme.Psychology;
        public DateTime ReadAt { get; set; }
    }
}