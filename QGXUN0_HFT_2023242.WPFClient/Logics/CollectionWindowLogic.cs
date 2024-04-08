using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;
using QGXUN0_HFT_2023242.WPFClient.Windows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityWindows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class CollectionWindowLogic
    {
        private CollectionWebList collectionList;

        public CollectionWindowLogic()
        {
            collectionList = new CollectionWebList();
        }


        public void Create()
        {
            var collection = new Collection();
            if (new CollectionUpdateWindow().ShowDialog(ref collection) == true)
                collectionList?.Create(collection).GetAwaiter();
        }

        public void Read()
        {
            if (new NumberInputWindow().ShowDialog(out int? id) == true && id != null)
                new CollectionWindow(collectionList.Read(id.Value)).ShowDialog();
        }

        public void Update()
        {
            if (new CollectionListWindow(collectionList.Items, "Select collection to update").ShowDialog(out Collection? collection) == true && collection != null)
            {
                new CollectionUpdateWindow().ShowDialog(ref collection);
                collectionList?.Update(collection);
            }
        }

        public void Delete()
        {
            if (new CollectionListWindow(collectionList.Items, "Select collection to delete").ShowDialog(out Collection? collection) == true && collection != null)
            {
                collectionList?.Delete(collection?.CollectionID ?? int.MinValue);
            }
        }

        public void ReadAll()
        {
            new CollectionListWindow(collectionList.Items, "Collections").ShowDialog();
        }


        public void AddBooks()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to add books").ShowDialog(out Collection? collection) == true && collection != null
                && new BookListWindow(collectionList.GetList<Book>("Book"), "Select books to add to the collection").ShowDialog(out IEnumerable<Book> books) == true)
                collectionList.Put<bool>("Collection/AddBooks/enum", new Tuple<Collection, IEnumerable<Book>>(collection, books));
        }

        public void RemoveBooks()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to remove books").ShowDialog(out Collection? collection) == true && collection != null
                && new BookListWindow(collection.Books, "Select books to remove from the collection").ShowDialog(out IEnumerable<Book> books) == true)
                collectionList.Put<bool>("Collection/RemoveBooks/enum", new Tuple<Collection, IEnumerable<Book>>(collection, books));
        }

        public void ClearBooks()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to remove every book").ShowDialog(out Collection? collection) == true && collection != null)
                collectionList.Put<bool>("Collection/ClearBooks", collection);
        }

        public void Series()
        {
            new CollectionListWindow(collectionList.GetList<Collection>("Collection/Series"), "Series collections").ShowDialog();
        }

        public void NonSeries()
        {
            new CollectionListWindow(collectionList.GetList<Collection>("Collection/NonSeries"), "Not series collections").ShowDialog();
        }

        public void InYear()
        {
            if (new NumberInputWindow("Enter the year").ShowDialog(out int? year) == true && year != null)
                new CollectionListWindow(collectionList.Get<IEnumerable<Collection>>($"Collection/InYear?year={year}"), $"Collections in year {year}").ShowDialog();
        }

        public void BetweenYears()
        {
            if (new NumberInputWindow("Enter the minimum year").ShowDialog(out int? minYear) == true && minYear != null
                && new NumberInputWindow("Enter the maximum year").ShowDialog(out int? maxYear) == true && maxYear != null)
                new CollectionListWindow(collectionList.Get<IEnumerable<Collection>>($"Collection/BetweenYears?min={minYear}&max={maxYear}"), $"Collections between year {minYear} & {maxYear}").ShowDialog();
        }

        public void Price()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to get it's price").ShowDialog(out Collection? collection) == true && collection != null)
                new SimpleOutput($"Price of the collection '{collection.CollectionName}'", collectionList.Post<double?>("Collection/Price", collection)).ShowDialog();
        }

        public void Rating()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to get it's average rating").ShowDialog(out Collection? collection) == true && collection != null)
                new SimpleOutput($"Rating of the collection '{collection.CollectionName}'", collectionList.Post<double?>("Collection/Rating", collection)).ShowDialog();
        }

        public void Select()
        {
            if (new ItemSelectorWindow(new List<object>(){
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated,
            }).ShowDialog(out object? bookFilter) == true && bookFilter != null && bookFilter is BookFilter
            && new ItemSelectorWindow(new List<object>()
            {
                CollectionFilter.Collection,
                CollectionFilter.Series,
                CollectionFilter.NonSeries,
            }).ShowDialog(out object? collectionFilter) == true && collectionFilter is CollectionFilter)
                new CollectionWindow(collectionList.Post<KeyValuePair<double?, Collection>>("Collection/Select", new Tuple<BookFilter, CollectionFilter>((BookFilter)bookFilter, (CollectionFilter)collectionFilter)).Value).ShowDialog();
        }

        public void SelectBook()
        {
            if (new CollectionListWindow(collectionList.Items, "Select a collection to filtering").ShowDialog(out Collection? collection) == true && collection != null
                && new ItemSelectorWindow(new List<object>(){
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated,
            }).ShowDialog(out object? filter) == true && filter != null && filter is BookFilter)
                new BookWindow(collectionList.Post<Book>("Collection/SelectBook", new Tuple<Collection, BookFilter>(collection, (BookFilter)filter))).ShowDialog();
        }


        public bool DefaultCondition() => collectionList != null;

        public bool NotEmptyCondition() => collectionList != null && collectionList.Count > 0;
    }
}
