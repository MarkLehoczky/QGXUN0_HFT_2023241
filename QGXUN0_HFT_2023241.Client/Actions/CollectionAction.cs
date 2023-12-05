using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Client.Actions
{
    class CollectionAction
    {
        private static CollectionAction singleton = new CollectionAction();
        public static CollectionAction Static { get => singleton; }
        public static WebService web { get; set; }


        public Action Create = () =>
        {
            web.Post<int?>("Collection", CustomConsole.CreateCollection());
        };

        public Action List = () =>
        {
            CustomConsole.List("List of collections", web.GetList<Collection>("Collection"));
        };

        public Action Read = () =>
        {
            CustomConsole.Single(web.Get<Collection>("Collection", CustomConsole.Value<int>("Enter th ID of the collection")));
        };

        public Action Update = () =>
        {
            var t = CustomConsole.UpdateCollection(CustomConsole.List("Select a collection to update", web.GetList<Collection>("Collection")));
            if (t != null) web.Put<bool>("Collection", t);
        };

        public Action Delete = () =>
        {
            var t = CustomConsole.List("Select a collection to delete", web.GetList<Collection>("Collection"));
            if (t != null) web.Delete<bool>("Collection", t.CollectionID);
        };


        public Action AddBooks = () =>
        {
            var t = CustomConsole.List("Select a collection to add books", web.GetList<Collection>("Collection"));
            if (t == null) return;

            List<Book> b = new List<Book>();
            do
            {
                var u = CustomConsole.List($"Select a book for the '{t.CollectionName}' collection ({b.Count()} selected)", web.GetList<Book>("Book"), b);
                if (b.Contains(u)) b.Remove(u);
                else b.Add(u);
            } while (b.LastOrDefault() != null);

            web.Put<bool>("Collection/AddBooks/enum", new Tuple<Collection, IEnumerable<Book>>(t, b.SkipLast(1)));
        };

        public Action RemoveAuthors = () =>
        {
            var t = CustomConsole.List("Select a collection to remove books", web.GetList<Collection>("Collection"));
            if (t == null) return;

            List<Book> b = new List<Book>();
            do
            {
                var u = CustomConsole.List($"Select a book from the '{t.CollectionName}' collection ({b.Count()} selected)", web.GetList<Book>("Book"), b);
                if (b.Contains(u)) b.Remove(u);
                else b.Add(u);
            } while (b.LastOrDefault() != null);

            web.Put<bool>("Collection/RemoveBooks/enum", new Tuple<Collection, IEnumerable<Book>>(t, b.SkipLast(1)));
        };

        public Action ClearBooks = () =>
        {
            var t = CustomConsole.List("Select a collection to remove all books", web.GetList<Collection>("Collection"));
            if (t != null) web.Put<bool>("Collection/ClearBooks", t);
        };

        public Action Series = () =>
        {
            CustomConsole.List("Series collections", web.GetList<Collection>("Collection/Series"));
        };

        public Action NonSeries = () =>
        {
            CustomConsole.List("Not series collections", web.GetList<Collection>("Collection/NonSeries"));
        };

        public Action InYear = () =>
        {
            CustomConsole.List("Collections in given year", web.GetList<Collection>($"Collection/InYear?year={CustomConsole.Value<int>("Enter the year")}"));
        };

        public Action BetweenYears = () =>
        {
            CustomConsole.List("Collections between given years", web.GetList<Collection>($"Collection/BetweenYears?min={CustomConsole.Value<int>("Enter the minimum year")}&max={CustomConsole.Value<int>("Enter the maximum year")}"));
        };

        public Action Price = () =>
        {
            var t = CustomConsole.List("Select collection", web.GetList<Collection>("Collection"));
            if (t != null) CustomConsole.Single(web.Post<double?>("Collection/Price", t));
        };

        public Action Rating = () =>
        {
            var t = CustomConsole.List("Select collection", web.GetList<Collection>("Collection"));
            if (t != null) CustomConsole.Single(web.Post<double?>("Collection/Rating", t));
        };

        public Action Select = () =>
        {
            var f1 = CustomConsole.List("Choose a book filter", new List<BookFilter?>
            {
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated
            });
            var f2 = CustomConsole.List("Choose a collection filter", new List<CollectionFilter?>
            {
                CollectionFilter.Collection,
                CollectionFilter.Series,
                CollectionFilter.NonSeries
            });
            if (f1 != null && f2 != null) CustomConsole.Single(web.Post<KeyValuePair<double?, Collection>>("Collection/Select", new Tuple<BookFilter, CollectionFilter>((BookFilter)f1, (CollectionFilter)f2)));
        };


        public Action SelectBook = () =>
        {
            var t = CustomConsole.List("Select collection", web.GetList<Collection>("Collection"));
            var f = CustomConsole.List("Choose a book filter", new List<BookFilter?>
            {
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated
            });
            if (t != null && f != null) CustomConsole.Single(web.Post<Book>("Collection/SelectBook", new Tuple<Collection, BookFilter>(t, (BookFilter)f)));
        };
    }
}
