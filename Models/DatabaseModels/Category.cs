using System.Collections.Generic;
using System.Linq;

namespace TimeCraft
{
    internal class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public Category(int categoryId, string title, string description = null, string color = null)
        {
            CategoryId = categoryId;
            Title = title;
            Description = description;
            Color = color;
        }
    }
}