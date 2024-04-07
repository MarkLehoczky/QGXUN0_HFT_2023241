using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;
using QGXUN0_HFT_2023242.WPFClient.Windows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityWindows;
using System;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class BookWindowLogic
    {
        private BookWebList bookList;

        public BookWindowLogic()
        {
            bookList = new BookWebList();
        }


        public void Create()
        {
            var book = new Book();
            if (new BookUpdateWindow().ShowDialog(ref book, bookList.GetList<Publisher>("Publisher")) == true)
                bookList?.Create(book).GetAwaiter();
        }

        public void Read()
        {
            if (new NumberInputWindow().ShowDialog(out int? id) == true && id != null)
                new BookWindow(bookList.Read(id.Value)).ShowDialog();
        }

        public void Update()
        {
            if (new BookListWindow(bookList, "Select book to update").ShowDialog(out Book? book) == true && book != null)
            {
                new BookUpdateWindow().ShowDialog(ref book, bookList.GetList<Publisher>("Publisher"));
                bookList?.Update(book);
            }
        }

        public void Delete()
        {
            if (new BookListWindow(bookList, "Select book to delete").ShowDialog(out Book? book) == true && book != null)
            {
                bookList?.Delete(book?.BookID ?? int.MinValue);
            }
        }

        public void ReadAll()
        {
            new BookListWindow(bookList.Items, "Books").ShowDialog();
        }


        public void AddAuthors()
        {
            if (new BookListWindow(bookList.Items, "Select a book to add authors").ShowDialog(out Book? book) == true && book != null
                && new AuthorListWindow(bookList.GetList<Author>("Author"), "Select authors to add to the book").ShowDialog(out IEnumerable<Author> authors) == true)
                bookList.Put<bool>("Book/AddAuthors/enum", new Tuple<Book, IEnumerable<Author>>(book, authors));
        }

        public void RemoveAuthors()
        {
            if (new BookListWindow(bookList.Items, "Select a book to remove authors").ShowDialog(out Book? book) == true && book != null
                && new AuthorListWindow(bookList.GetList<Author>("Author"), "Select authors to remove from the book").ShowDialog(out IEnumerable<Author> authors) == true)
                bookList.Put<bool>("Book/RemoveAuthors/enum", new Tuple<Book, IEnumerable<Author>>(book, authors));
        }

        public void InYear()
        {
            if (new NumberInputWindow("Enter the year").ShowDialog(out int? year) == true && year != null)
                new BookListWindow(bookList.Get<IEnumerable<Book>>($"Book/InYear?year={year}"), $"Books in year {year}").ShowDialog();
        }

        public void BetweenYears()
        {
            if (new NumberInputWindow("Enter the minimum year").ShowDialog(out int? minYear) == true && minYear != null
                && new NumberInputWindow("Enter the maximum year").ShowDialog(out int? maxYear) == true && maxYear != null)
                new BookListWindow(bookList.Get<IEnumerable<Book>>($"Book/BetweenYears?min={minYear}&max={maxYear}"), $"Books between year {minYear} & {maxYear}").ShowDialog();
        }

        public void TitleContains()
        {
            if (new TitleInputWindow().ShowDialog(out IEnumerable<string> titles) == true)
                foreach (var item in bookList.Post<IDictionary<string, IEnumerable<Book>>>("Book/TitleContains/enum", titles))
                    new BookListWindow(item.Value, $"{item.Key} titles").ShowDialog();
        }

        public void Select()
        {
            if (new ItemSelectorWindow(new List<object>(){
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated,
            }).ShowDialog(out object? filter) == true && filter != null && filter is BookFilter)
                new BookWindow(bookList.Post<Book>("Book/Select",(BookFilter)filter)).ShowDialog();
        }


        public bool DefaultCondition() => bookList != null;

        public bool NotEmptyCondition() => bookList != null && bookList.Count > 0;
    }
}
