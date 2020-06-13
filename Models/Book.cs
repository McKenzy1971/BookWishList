using System;

namespace BookWishList.Models
{
    [Serializable]
    public class Book
    {
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Author { get; set; }
    }
}
