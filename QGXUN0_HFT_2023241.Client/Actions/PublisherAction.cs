using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023241.Client.Actions
{
    class PublisherAction
    {
        private static PublisherAction singleton = new PublisherAction();
        public static PublisherAction Static { get => singleton; }
        public static WebService web { get; set; }


        public Action Create = () =>
        {
            web.Post<int?>("Publisher", CustomConsole.CreatePublisher());
        };

        public Action List = () =>
        {
            CustomConsole.List("List of publishers", web.GetList<Publisher>("Publisher"));
        };

        public Action Read = () =>
        {
            CustomConsole.Single(web.Get<Publisher>("Publisher", CustomConsole.Value<int>("Enter th ID of the publisher")));
        };

        public Action Update = () =>
        {
            var t = CustomConsole.UpdatePublisher(CustomConsole.List("Select a publisher to update", web.GetList<Publisher>("Publisher")));
            if (t != null) web.Put<bool>("Publisher", t);
        };

        public Action Delete = () =>
        {
            var t = CustomConsole.List("Select a publisher to delete", web.GetList<Publisher>("Publisher"));
            if (t != null) web.Delete<bool>("Publisher", t.PublisherID);
        };


        public Action Series = () =>
        {
            CustomConsole.List("Series publishers", web.GetList<Publisher>("Publisher/Series"));
        };

        public Action OnlySeries = () =>
        {
            CustomConsole.List("Only series publishers", web.GetList<Publisher>("Publisher/OnlySeries"));
        };

        public Action HighestRated = () =>
        {
            CustomConsole.Single(web.Get<KeyValuePair<double?, Publisher>>("Publisher/HighestRated"));
        };

        public Action LowestRated = () =>
        {
            CustomConsole.Single(web.Get<KeyValuePair<double?, Publisher>>("Publisher/LowestRated"));
        };

        public Action Rating = () =>
        {
            var t = CustomConsole.List("Select publisher", web.GetList<Publisher>("Publisher"));
            if (t != null) CustomConsole.Single(web.Post<double?>("Publisher/Rating", t));
        };

        public Action Authors = () =>
        {
            var t = CustomConsole.List("Select publisher", web.GetList<Publisher>("Publisher"));
            if (t != null) CustomConsole.List($"Authors of the '{t.PublisherName}' publisher", web.Post<IEnumerable<Author>>("Publisher/Authors", t));
        };

        public Action PermanentAuthors = () =>
        {
            CustomConsole.List("Permanent authors", web.GetList<Author>("Publisher/PermanentAuthors"));
        };

        public Action PermanentAuthorsOfPublisher = () =>
        {
            var t = CustomConsole.List("Select publisher", web.GetList<Publisher>("Publisher"));
            if (t != null) CustomConsole.List($"Permanent authors of the '{t.PublisherName}' publisher", web.Post<IEnumerable<Author>>("Publisher/PermanentAuthors", t));
        };
    }
}
