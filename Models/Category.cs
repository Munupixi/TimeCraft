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

        public static void CreateAllCategories()
        {
            Category[] categories = {
                new Category(1, "Нет", "Пустая категория", "DarkBlue"),
                new Category(2, "Работа", "Категория для деловых событий", "Red"),
                new Category(3, "Учеба", "Категория для учебных событий", "Blue"),
                new Category(4, "Спорт", "Категория для спортивных событий", "Green"),
                new Category(5, "Здоровье", "Категория для событий, связанных со здоровьем", "Orange"),
                new Category(6, "Личное", "Категория для личных событий", "Purple"),
                new Category(7, "Путешествия", "Категория для путешественных событий", "Yellow"),
                new Category(8, "Развлечения", "Категория для событий развлекательного характера", "LightBlue"),
                new Category(9, "Семья", "Категория для семейных событий", "Brown"),
                new Category(10, "Дом", "Категория для событий, связанных с домашними делами", "Gray"),
                new Category(11, "Финансы", "Категория для событий, связанных с финансами", "Black")};

            using (DataBaseContent db = new DataBaseContent())
            {
                db.Category.AddRange(categories);
                db.SaveChanges();
            }
        }

        public static List<string> GetAllTitles()
        {
            using (DataBaseContent db = new DataBaseContent())
            {
                return db.Category.Select(c => c.Title).ToList();
            }
        }
    }
}