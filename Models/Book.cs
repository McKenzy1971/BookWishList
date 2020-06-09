using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWishList.Models
{
    [Serializable]
    public class Book
    {
        public string Titel { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public Uri URL { get; set; }
    }
}
