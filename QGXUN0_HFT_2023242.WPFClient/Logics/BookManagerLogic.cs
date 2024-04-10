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
    public class BookManagerLogic : IBookManagerLogic
    {
        private WebList<Book> webList;
        private IMessenger messenger;


        public BookManagerLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }


        public void Create()
        {
            var book = new Book();
            if (new BookUpdateWindow().ShowDialog(ref book, webList.GetList<Publisher>("Publisher")) == true)
            {
                webList.Add(book);
                messenger.Send("Book added", "BookModification");
            }
        }

        public void Read(Book book)
        {
            new BookWindow(webList.Get(book.BookID)).ShowDialog();
        }

        public void Update(Book book)
        {
            if (new BookUpdateWindow().ShowDialog(ref book, webList.GetList<Publisher>("Publisher")) == true)
            {
                webList.Replace(book);
                messenger.Send("Book replaced", "BookModification");
            }
        }

        public void Delete(Book book)
        {
            webList.Remove(book.BookID);
            messenger.Send("Book removed", "BookModification");
        }

        public void ReadAll()
        {
            new BookListWindow(webList).ShowDialog();
        }


        public void Setup(WebList<Book> webList)
        {
            this.webList = webList;
        }


        public void AddAuthors(Book book)
        {
            if (new AuthorListWindow(webList.GetList<Author>("Author"), "Select authors to add to the book").ShowDialog(out IEnumerable<Author> authors) == true)
            {
                webList.Put<bool>("Book/AddAuthors/enum", new Tuple<Book, IEnumerable<Author>>(book, authors));
                messenger.Send("Authors added to book", "BookModification");
            }
        }

        public void RemoveAuthors(Book book)
        {
            if (new AuthorListWindow(book.Authors, "Select authors to remove from the book").ShowDialog(out IEnumerable<Author> authors) == true)
            {
                webList.Put<bool>("Book/RemoveAuthors/enum", new Tuple<Book, IEnumerable<Author>>(book, authors));
                messenger.Send("Authors removed from book", "BookModification");
            }
        }

        public void InYear()
        {
            if (new NumberInputWindow("Enter the year").ShowDialog(out int? year) == true
                && year != null)
            {
                new BookListWindow(webList.Get<IEnumerable<Book>>($"Book/InYear?year={year}"), $"Books in year {year}").ShowDialog();
            }
        }

        public void BetweenYears()
        {
            if (new NumberInputWindow("Enter the minimum year").ShowDialog(out int? minYear) == true
                && new NumberInputWindow("Enter the maximum year").ShowDialog(out int? maxYear) == true
                && minYear != null
                && maxYear != null)
            {
                new BookListWindow(webList.Get<IEnumerable<Book>>($"Book/BetweenYears?min={minYear}&max={maxYear}"), $"Books between year {minYear} & {maxYear}").ShowDialog();
            }
        }

        public void TitleContains()
        {
            if (new TitleInputWindow().ShowDialog(out IEnumerable<string> titles) == true)
            {
                foreach (var item in webList.Post<IDictionary<string, IEnumerable<Book>>>("Book/TitleContains/enum", titles))
                {
                    new BookListWindow(item.Value, $"{item.Key} titles").ShowDialog();
                }
            }
        }

        public void Select()
        {
            if (new ItemSelectorWindow(new List<object>() { BookFilter.MostExpensive, BookFilter.LeastExpensive, BookFilter.HighestRated, BookFilter.LowestRated }).ShowDialog(out object? filter) == true
            && filter != null
            && filter is BookFilter bookFilter)
            {
                new BookWindow(webList.Post<Book>("Book/Select", bookFilter)).ShowDialog();
            }
        }
    }
}
