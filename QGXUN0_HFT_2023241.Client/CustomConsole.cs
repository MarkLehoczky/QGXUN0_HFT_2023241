using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QGXUN0_HFT_2023241.Client
{
    static class CustomConsole
    {
        private static int MaxHeight { get; set; }
        private static int MaxWidth { get; set; }

        static CustomConsole()
        {
            MaxHeight = Console.LargestWindowHeight;
            MaxWidth = Console.LargestWindowWidth;

            if (OperatingSystem.IsWindows())
            {
                Console.BufferHeight = MaxHeight;
                Console.WindowHeight = MaxHeight;
                Console.BufferWidth = MaxWidth;
                Console.WindowWidth = MaxWidth;
            }
            else
            {
                MaxHeight = Console.WindowHeight;
                MaxWidth = Console.WindowWidth;
            }

            Console.CursorVisible = false;
            Reset();
        }

        public static void Reset()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        public static string CenterText(string text)
        {
            return new string(' ', (MaxWidth - text.Length) / 2) + text;
        }

        public static void Menu(string title, params Tuple<string, Action>[] items)
        {
            ConsoleKey key;
            int states = items.Length + 1;
            uint selected = (uint)(states * 100);

            while (true)
            {
                Reset();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(CenterText(title));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n\n\n");

                int count = 0;
                foreach (var item in items)
                {
                    if (selected % states == count++) Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(CenterText(item.Item1));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (selected % states == count) Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("[Esc] Return"));

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow || key == ConsoleKey.UpArrow) selected--;
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.DownArrow) selected++;
                else if (key == ConsoleKey.Backspace || key == ConsoleKey.Escape) return;
                else if (key == ConsoleKey.Enter && selected % states == states - 1) return;
                else if (key == ConsoleKey.Enter) items[selected % states].Item2?.Invoke();
            }
        }

        public static string MultiLineFormat<T>(T obj)
        {
            if (obj == null) return "";

            var sb = new StringBuilder();

            foreach (var property in typeof(T).GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");


                if (property.GetValue(obj) != null && property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    sb.AppendLine();
                    foreach (var item in property.GetValue(obj) as IEnumerable)
                    {
                        sb.Append(" > ");
                        sb.AppendLine(item.ToString());
                    }
                }
                else
                {
                    sb.AppendLine(property.GetValue(obj)?.ToString());
                }

            }

            return sb.ToString();
        }

        public static string LineFormat(Author obj)
        {
            int w = MaxWidth - 7;

            StringBuilder sb = new StringBuilder();
            sb.Append("| ");
            sb.Append((obj.AuthorName + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.1)));
            sb.Append(" | ");
            sb.Append((string.Join(';', obj.Books.Select(t => t.Title)) + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.9)));
            sb.Append(" |");

            return sb.ToString();
        }
        public static string LineFormat(Book obj)
        {
            int w = MaxWidth - 16;

            StringBuilder sb = new StringBuilder();
            sb.Append("| ");
            sb.Append((string.Join(';', obj.Authors.Select(t => t.AuthorName)) + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.2)));
            sb.Append(" | ");
            sb.Append((obj.Title + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.6)));
            sb.Append(" | ");
            sb.Append((obj.Year.ToString() + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.025)));
            sb.Append(" | ");
            sb.Append((obj.Publisher?.PublisherName + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.11)));
            sb.Append(" | ");
            sb.Append((obj.Price?.ToString() + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.04)));
            sb.Append(" | ");
            sb.Append((obj.Rating?.ToString() + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.025)));
            sb.Append(" |");

            return sb.ToString();
        }
        public static string LineFormat(Collection obj)
        {
            int w = MaxWidth - 10;

            StringBuilder sb = new StringBuilder();
            sb.Append("| ");
            sb.Append((obj.CollectionName + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.1)));
            sb.Append(" | ");
            sb.Append((string.Join(';', obj.Books.Select(t => t.Title)) + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.85)));
            sb.Append(" | ");
            sb.Append((obj.IsSeries?.ToString() + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.05)));
            sb.Append(" |");

            return sb.ToString();
        }
        public static string LineFormat(Publisher obj)
        {
            int w = MaxWidth - 10;

            StringBuilder sb = new StringBuilder();
            sb.Append("| ");
            sb.Append((obj.PublisherName + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.1)));
            sb.Append(" | ");
            sb.Append((string.Join(';', obj.Books.Select(t => t.Title)) + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.8)));
            sb.Append(" | ");
            sb.Append((obj.Website?.ToString() + new string(' ', MaxWidth)).Substring(0, (int)(w * 0.10)));
            sb.Append(" |");

            return sb.ToString();
        }
        public static string LineFormat<T>(T obj)
        {
            if (obj is Author) return LineFormat(obj as Author);
            else if (obj is Book) return LineFormat(obj as Book);
            else if (obj is Collection) return LineFormat(obj as Collection);
            else if (obj is Publisher) return LineFormat(obj as Publisher);
            else if (MaxWidth - 2 < obj.ToString().Replace('\n', '\t').Length) return obj.ToString().Replace('\n', '\t').Substring(0, MaxWidth - 1);
            else return obj.ToString().Replace('\n', '\t');
        }
        public static string ShowLine<T>(T obj, params Tuple<string, double>[] propSize)
        {
            int w = MaxWidth - (propSize.Length - 1) * 3 - 4;
            var sb = new StringBuilder();

            sb.Append("| ");

            foreach (var item in propSize)
            {
                sb.Append(
                    (typeof(T)
                    .GetProperty(item.Item1)
                    .GetValue(obj)?.ToString() + new string(' ', MaxWidth))
                    .Substring(0, (int)(w * (item.Item2 / 100.0)))
                    );
                sb.Append(" | ");
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static void Single<T>(T item)
        {
            Reset();

            Console.ForegroundColor = ConsoleColor.White;

            var lines = MultiLineFormat(item);
            if (lines.Split("\n").Length > MaxHeight && OperatingSystem.IsWindows())
                Console.BufferHeight = lines.Split("\n").Length;

            Console.WriteLine(lines);

            Console.ReadKey(true);

            if (OperatingSystem.IsWindows())
                Console.BufferHeight = MaxHeight;
        }

        public static T List<T>(string title, IEnumerable<T> items, IEnumerable<T> picked = null)
        {
            ConsoleKey key;
            int states = items.Count() + 1;
            uint selected = (uint)(states * 100);

            int h = MaxHeight - 7;

            while (true)
            {
                Reset();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(CenterText(title));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n");

                int count = (int)((selected % states - 5) > 0 ? selected % states - 5 : 0);
                foreach (var item in items.Skip((int)(selected % states - 5)).Take(h))
                {
                    if (picked != null && picked.Contains(item)) Console.ForegroundColor = ConsoleColor.DarkGreen;
                    if (selected % states == count++) Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(LineFormat(item));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (selected % states == count) Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("[Esc] Return"));

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow || key == ConsoleKey.UpArrow) selected--;
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.DownArrow) selected++;
                else if (key == ConsoleKey.Backspace || key == ConsoleKey.Escape) return default;
                else if (key == ConsoleKey.Enter && selected % states == states - 1) return default;
                else if (key == ConsoleKey.Enter) return items.ElementAt((int)(selected % states));
            }
        }

        public static Author CreateAuthor()
        {
            while (true)
            {
                Reset();
                Console.Write("Author name: ");
                string name = Console.ReadLine();
                Console.WriteLine();

                try { return Author.Parse($"1;{name}", ";", true); }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
            }
        }
        public static Book CreateBook()
        {
            while (true)
            {
                Reset();
                Console.Write("Book title: ");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Book release year: ");
                string year = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Book's publisher's ID: ");
                string id = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Book price: ");
                string price = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Book rating: ");
                string rating = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (rating != "" && price != "" && id != "")
                        return Book.Parse($"1;{name};{year};{id};{price};{rating}", ";", true);
                    else if (price != "" && id != "")
                        return Book.Parse($"1;{name};{year};{id};{price}", ";", true);
                    else if (id != "")
                        return Book.Parse($"1;{name};{year};{id}", ";", true);
                    else
                        return Book.Parse($"1;{name};{year}", ";", true);
                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
            }
        }
        public static Collection CreateCollection()
        {
            while (true)
            {
                Reset();
                Console.Write("Collection name: ");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Is collection series: ");
                string isseries = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (isseries != "")
                        return Collection.Parse($"1;{name};{isseries}", ";", true);
                    else
                        return Collection.Parse($"1;{name}", ";", true);
                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
            }
        }
        public static Publisher CreatePublisher()
        {
            while (true)
            {
                Reset();
                Console.Write("Publisher name: ");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.Write("Publisher website: ");
                string website = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (website != "")
                        return Publisher.Parse($"1;{name};{website}", ";", true);
                    else
                        return Publisher.Parse($"1;{name}", ";", true);
                }
                catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
            }
        }

        public static Author UpdateAuthor(Author author)
        {
            ConsoleKey key;

            Reset();
            Console.WriteLine(MultiLineFormat(author));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Update 'AuthorName'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'AuthorName': ");
                    author.AuthorName = Console.ReadLine();
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);
            Console.WriteLine();

            return author;
        }
        public static Book UpdateBook(Book book)
        {
            ConsoleKey key;

            Reset();
            Console.WriteLine(MultiLineFormat(book));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Update 'Title'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'Title': ");
                    book.Title = Console.ReadLine();
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'Year'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'Year'(int): ");
                    try { book.Year = int.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'PublisherID'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'PublisherID'(int): ");
                    try { book.PublisherID = int.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'Price'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'Price'(double): ");
                    try { book.Price = double.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'Rating'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'Rating'(double): ");
                    try { book.Rating = double.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            return book;
        }
        public static Collection UpdateCollection(Collection collection)
        {
            ConsoleKey key;

            Reset();
            Console.WriteLine(MultiLineFormat(collection));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Update 'CollectionName'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'CollectionName': ");
                    collection.CollectionName = Console.ReadLine();
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'IsSeries'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'IsSeries'(bool): ");
                    try { collection.IsSeries = bool.Parse(Console.ReadLine()); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);
            Console.WriteLine();

            return collection;
        }
        public static Publisher UpdatePublisher(Publisher publisher)
        {
            ConsoleKey key;

            Reset();
            Console.WriteLine(MultiLineFormat(publisher));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Update 'PublisherName'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'PublisherName': ");
                    publisher.PublisherName = Console.ReadLine();
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine();
            Console.WriteLine("Update 'Website'? (y/n)");
            Console.WriteLine();
            do
            {
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y || key == ConsoleKey.I)
                {
                    Console.Write("New 'Website': ");
                    publisher.Website = Console.ReadLine();
                }
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);
            Console.WriteLine();

            return publisher;
        }


        public static T Update<T>(T obj)
        {
            ConsoleKey key;

            Reset();
            Console.WriteLine(MultiLineFormat(obj));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;


            foreach (var property in typeof(T).GetProperties().Where(t =>
                !t.GetAccessors().Any(t => t.IsVirtual) &&
                !t.GetCustomAttributes<Attribute>().Any(t =>
                    t is KeyAttribute ||
                    t is System.Text.Json.Serialization.JsonIgnoreAttribute ||
                    t is Newtonsoft.Json.JsonIgnoreAttribute)))
            {
                Console.WriteLine($"Update '{property.Name}? (y/n)'");
                do
                {
                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Y || key == ConsoleKey.I)
                    {

                        if (property.PropertyType.IsGenericType)
                        {
                            if (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                                    if (Nullable.GetUnderlyingType(property.PropertyType).GetMethod("Parse", new[] { typeof(string) }) != null)
                                    {
                                        Console.Write($"New '{property.Name}'({property.PropertyType.Name}): ");
                                        property.SetValue(obj, property.PropertyType.GetMethod("Parse", new[] { typeof(string) })?.Invoke(null, new[] { Console.ReadLine() }));

                                    }
                        }
                        else if (property.PropertyType.IsGenericType &&
                            property.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                        {
                            Console.Write($"New '{property.Name}'({property.PropertyType.Name}): ");
                            property.SetValue(obj, Console.ReadLine());
                        }
                    }
                } while (key != ConsoleKey.Y && key != ConsoleKey.I && key != ConsoleKey.N);
            }
            return obj;
        }



        public static T Value<T>(string text)
        {
            while (true)
            {
                Reset();
                Console.Write(text + ": ");
                object input = Console.ReadLine();
                Console.WriteLine();

                if (typeof(T).GetMethod("Parse", new[] { typeof(string) }) != null)
                {
                    try { return (T)(typeof(T).GetMethod("Parse", new[] { typeof(string) })?.Invoke(null, new object[] { input })); }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
                }
                else
                {
                    try { return (T)input; }
                    catch (Exception e) { Console.WriteLine("Error: " + e.Message); Console.ReadKey(true); }
                }
            }
        }
    }
}
