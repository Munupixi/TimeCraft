using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCraft
{
    internal class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Category(int categoryId, string title, string description = null)
        {
            CategoryId = categoryId;
            Title = title;
            Description = description;
        }
    }
}