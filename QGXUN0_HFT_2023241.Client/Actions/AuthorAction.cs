using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023241.Client.Actions
{
    class AuthorAction
    {
        private static AuthorAction singleton = new AuthorAction();
        public static AuthorAction Static { get => singleton; }
        public static WebService web { get; set; }



        public Action Create = () =>
        {
            web.Post<int?>("Author", CustomConsole.CreateAuthor());
        };

        public Action List = () =>
        {
            CustomConsole.List("List of authors", web.GetList<Author>("Author"));
        };

        public Action Read = () =>
        {
            CustomConsole.Single(web.Get<Author>("Author", CustomConsole.Value<int>("Enter th ID of the author")));
        };

        public Action Update = () =>
        {
            var t = CustomConsole.UpdateAuthor(CustomConsole.List("Select an author to update", web.GetList<Author>("Author")));
            if (t != null) web.Put<bool>("Author", t);
        };

        public Action Delete = () =>
        {
            var t = CustomConsole.List("Select an author to delete", web.GetList<Author>("Author"));
            if (t != null) web.Delete<bool>("Author", t.AuthorID);
        };


        public Action HighestRated = () =>
        {
            CustomConsole.Single(web.Get<KeyValuePair<double?, Author>>("Author/HighestRated"));
        };

        public Action LowestRated = () =>
        {
            CustomConsole.Single(web.Get<KeyValuePair<double?, Author>>("Author/LowestRated"));
        };

        public Action Series = () =>
        {
            var t = CustomConsole.List("Select an author to list their series", web.GetList<Author>("Author"));
            if (t != null) CustomConsole.List("Series of " + t.AuthorName, web.Post<IEnumerable<Collection>>("Author/Series", t));
        };

        public Action SelectBook = () =>
        {
            var t = CustomConsole.List("Select an author to list their series", web.GetList<Author>("Author"));
            var f = CustomConsole.List("Choose a book filter", new List<BookFilter?>
            {
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated
            });
            if (t != null && f != null) CustomConsole.Single(web.Post<Book>("Author/SelectBook", new Tuple<Author, BookFilter>(t, (BookFilter)f)));
        };
    }
}
