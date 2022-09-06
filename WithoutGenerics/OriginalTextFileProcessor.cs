using TrainingGenerics.Models;

namespace TrainingGenerics.WithoutGenerics
{
    public static class OriginalTextFileProcessor
    {
        public static void SaveBooks(List<Book> books, string bookFilePath)
        {
            List<string> lines = new List<string>();

            lines.Add("ISBN,Title,Author");

            foreach(var book in books)
            {
                lines.Add($"{ book.ISBN },{ book.Title },{ book.Author } ");
            }

            File.WriteAllLines(bookFilePath, lines);
        }

        public static List<Book> LoadBooks(string bookFilePath)
        {
            List<Book> output = new List<Book>();
            Book b;
            var lines = File.ReadAllLines(bookFilePath).ToList();

            lines.RemoveAt(0);

            foreach(var line in lines)
            {
                var vals = line.Split(',');
                b = new Book
                {
                    ISBN = int.Parse(vals[0]),
                    Title = vals[1],
                    Author = vals[2],
                };

                output.Add(b);
            }

            return output;

        }
    }
}
