using System;
using TrainingGenerics.Models;
using TrainingGenerics.WithGenerics;
using TrainingGenerics.WithoutGenerics;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generics training!");
            
            Console.ReadLine();    
            DemonstrateFileStorage();

            Console.WriteLine();
            Console.Write("Press enter to shut down...");
            Console.ReadLine();
        }

        private static void DemonstrateFileStorage()
        {
            var books = new List<Book>();
            var comics = new List<Comic>();

            string booksFile = @"c:\books.csv";
            string comicsFile = @"c:\comics.csv";

            PopulateList(books, comics);

            GenericTextFileProccessor.SaveToTextFile<Book>(books, booksFile);
            GenericTextFileProccessor.SaveToTextFile<Comic>(comics, comicsFile);



            var savedBooks = GenericTextFileProccessor.LoadFromTextFile<Book>(booksFile);
            var savedComics = GenericTextFileProccessor.LoadFromTextFile<Comic>(comicsFile);

            ShowList<Book>(savedBooks, "books");
            ShowList<Comic>(savedComics, "comics");

        }   

        private static void PopulateList(List<Book> books, List<Comic> comics)
        {
            books.Add(new Book { Author = "Stephen King", Title = "IT", ISBN = 1 });
            books.Add(new Book { Author = "Ryan Holiday", Title = "Daily Stoic", ISBN = 2 });
            books.Add(new Book { Author = "Napoleon Hill", Title = "Think and grow rich", ISBN = 3 });

            comics.Add(new Comic { ComicName = "Spiderman", Company = "Marvel", Id = 1 });
            comics.Add(new Comic { ComicName = "Batman", Company = "DC", Id = 2 });
            comics.Add(new Comic { ComicName = "StreetFighter", Company = "Capco", Id = 3 });
        }

        private static void ShowList<T>(List<T> values, String listName) where T: class
        {
            var props = values[0].GetType().GetProperties();

            Console.WriteLine($"*************{listName.ToUpper()}****************");
            foreach(var item in values)
            {
                foreach(var prop in props)
                {
                    Console.Write($" { prop.Name.ToUpper() }: { prop.GetValue(item)} ");
                }

                Console.Write("/\n");
            }
        }

    }
}