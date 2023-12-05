using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023241.Client.Actions
{
    class BookAction
    {
        private static BookAction singleton = new BookAction();
        public static BookAction Static { get => singleton; }
        public static WebService web { get; set; }


        public Action Create = () =>
        {
            web.Post<int?>("Book", CustomConsole.CreateBook());
        };

        public Action List = () =>
        {
            CustomConsole.List("List of books", web.GetList<Book>("Book"));
        };

        public Action Read = () =>
        {
            CustomConsole.Single(web.Get<Book>("Book", CustomConsole.Value<int>("Enter th ID of the book")));
        };

        public Action Update = () =>
        {
            var t = CustomConsole.UpdateBook(CustomConsole.List("Select a book to update", web.GetList<Book>("Book")));
            if (t != null) web.Put<bool>("Book", t);
        };

        public Action Delete = () =>
        {
            var t = CustomConsole.List("Select a book to delete", web.GetList<Book>("Book"));
            if (t != null) web.Delete<bool>("Book", t.BookID);
        };


        public Action AddAuthors = () =>
        {
            var t = CustomConsole.List("Select a book to add authors", web.GetList<Book>("Book"));
            if (t == null) return;

            List<Author> a = new List<Author>();
            do
            {
                var u = CustomConsole.List($"Select an author for the '{t.Title}' book ({a.Count()} selected)", web.GetList<Author>("Author"), a);
                if (a.Contains(u)) a.Remove(u);
                else a.Add(u);
            } while (a.LastOrDefault() != null);

            web.Put<bool>("Book/AddAuthors/enum", new Tuple<Book, IEnumerable<Author>>(t, a.SkipLast(1)));
        };

        public Action RemoveAuthors = () =>
        {
            var t = CustomConsole.List("Select a book to remove authors", web.GetList<Book>("Book"));
            if (t == null) return;

            List<Author> a = new List<Author>();
            do
            {
                var u = CustomConsole.List($"Select an author from the '{t.Title}' book ({a.Count()} selected)", web.GetList<Author>("Author"), a);
                if (a.Contains(u)) a.Remove(u);
                else a.Add(u);
            } while (a.LastOrDefault() != null);

            web.Put<bool>("Book/RemoveAuthors/enum", new Tuple<Book, IEnumerable<Author>>(t, a.SkipLast(1)));
        };

        public Action InYear = () =>
        {
            CustomConsole.List("Books in given year", web.GetList<Book>($"Book/InYear?year={CustomConsole.Value<int>("Enter the year")}"));
        };

        public Action BetweenYears = () =>
        {
            CustomConsole.List("Books between given years", web.GetList<Book>($"Book/BetweenYears?min={CustomConsole.Value<int>("Enter the minimum year")}&max={CustomConsole.Value<int>("Enter the maximum year")}"));
        };

        public Action TitleContains = () =>
        {
            var t = new List<string>();
            do
            {
                t.Add(CustomConsole.Value<string>($"Enter a subtext to search in the title ({t.Count} entered)"));
            } while (t.LastOrDefault() != "");

            CustomConsole.List("Books between given years", web.Post<IDictionary<string, IEnumerable<Book>>>("Book/TitleContains/enum", t.SkipLast(1)));
        };

        public Action Select = () =>
        {
            var f = CustomConsole.List("Choose a book filter", new List<BookFilter?>
            {
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated
            });
            if (f != null) CustomConsole.Single(web.Post<Book>("Book/Select", (BookFilter)f));
        };
    }
}
