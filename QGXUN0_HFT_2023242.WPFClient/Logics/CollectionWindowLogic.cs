using CommunityToolkit.Mvvm.Messaging;
using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;
using QGXUN0_HFT_2023242.WPFClient.Services;
using QGXUN0_HFT_2023242.WPFClient.Windows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityWindows;
using System;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class CollectionWindowLogic : ICollectionWindowLogic
    {
        private WebList<Collection> webList;
        private IMessenger messenger;


        public CollectionWindowLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }


        public void Create()
        {
            var collection = new Collection();
            if (new CollectionUpdateWindow().ShowDialog(ref collection) == true)
            {
                webList.Add(collection);
                messenger.Send("Collection added", "CollectionModification");
            }
        }

        public void Read(Collection collection)
        {
            new CollectionWindow(webList.Get(collection.CollectionID)).ShowDialog();
        }

        public void Update(Collection collection)
        {
            if (new CollectionUpdateWindow().ShowDialog(ref collection) == true)
            {
                webList.Replace(collection);
                messenger.Send("Collection replaced", "CollectionModification");
            }
        }

        public void Delete(Collection collection)
        {
            webList.Remove(collection.CollectionID);
            messenger.Send("Collection removed", "CollectionModification");
        }

        public void ReadAll()
        {
            new CollectionListWindow(webList).ShowDialog();
        }


        public void Setup(WebList<Collection> webList)
        {
            this.webList = webList;
        }


        public void AddBooks(Collection collection)
        {
            if (new BookListWindow(webList.GetList<Book>("Book"), "Select books to add to the collection").ShowDialog(out IEnumerable<Book> books) == true)
            {
                webList.Put<bool>("Collection/AddBooks/enum", new Tuple<Collection, IEnumerable<Book>>(collection, books));
                messenger.Send("Books added to collection", "CollectionModification");
            }
        }

        public void RemoveBooks(Collection collection)
        {
            if (new BookListWindow(collection.Books, "Select books to remove from the collection").ShowDialog(out IEnumerable<Book> books) == true)
            {
                webList.Put<bool>("Collection/RemoveBooks/enum", new Tuple<Collection, IEnumerable<Book>>(collection, books));
                messenger.Send("Books removed from collection", "CollectionModification");
            }
        }

        public void ClearBooks(Collection collection)
        {
            webList.Put<bool>("Collection/ClearBooks", collection);
            messenger.Send("Books removed from collection", "CollectionModification");
        }

        public void Series()
        {
            new CollectionListWindow(webList.GetList<Collection>("Collection/Series"), "Series collections").ShowDialog();
        }

        public void NonSeries()
        {
            new CollectionListWindow(webList.GetList<Collection>("Collection/NonSeries"), "Not series collections").ShowDialog();
        }

        public void InYear()
        {
            if (new NumberInputWindow("Enter the year").ShowDialog(out int? year) == true
                && year != null)
            {
                new CollectionListWindow(webList.Get<IEnumerable<Collection>>($"Collection/InYear?year={year}"), $"Collections in year {year}").ShowDialog();
            }
        }

        public void BetweenYears()
        {
            if (new NumberInputWindow("Enter the minimum year").ShowDialog(out int? minYear) == true
                && new NumberInputWindow("Enter the maximum year").ShowDialog(out int? maxYear) == true
                && minYear != null
                && maxYear != null)
            {
                new CollectionListWindow(webList.Get<IEnumerable<Collection>>($"Collection/BetweenYears?min={minYear}&max={maxYear}"), $"Collections between year {minYear} & {maxYear}").ShowDialog();
            }
        }

        public void Price(Collection collection)
        {
            new SimpleOutput($"Price of the collection '{collection.CollectionName}'", webList.Post<double?>("Collection/Price", collection)).ShowDialog();
        }

        public void Rating(Collection collection)
        {
            new SimpleOutput($"Rating of the collection '{collection.CollectionName}'", webList.Post<double?>("Collection/Rating", collection)).ShowDialog();
        }

        public void Select()
        {
            if (new ItemSelectorWindow(new List<object>() {
                    BookFilter.MostExpensive,
                    BookFilter.LeastExpensive,
                    BookFilter.HighestRated,
                    BookFilter.LowestRated })
                .ShowDialog(out object? bookfilter) == true
                && bookfilter != null
                && bookfilter is BookFilter bookFilter
                && new ItemSelectorWindow(new List<object>() {
                    CollectionFilter.Collection,
                    CollectionFilter.Series,
                    CollectionFilter.NonSeries })
                .ShowDialog(out object? collectionfilter) == true
                && collectionfilter is CollectionFilter collectionFilter)
            {
                new CollectionWindow(webList.Post<KeyValuePair<double?, Collection>>("Collection/Select", new Tuple<BookFilter, CollectionFilter>(bookFilter, collectionFilter)).Value).ShowDialog();
            }
        }

        public void SelectBook(Collection collection)
        {
            if (new ItemSelectorWindow(new List<object>() {
                    BookFilter.MostExpensive,
                    BookFilter.LeastExpensive,
                    BookFilter.HighestRated,
                    BookFilter.LowestRated })
                .ShowDialog(out object? filter) == true
                && filter != null
                && filter is BookFilter bookFilter)
            {
                new BookWindow(webList.Post<Book>("Collection/SelectBook", new Tuple<Collection, BookFilter>(collection, bookFilter))).ShowDialog();
            }
        }
    }
}
