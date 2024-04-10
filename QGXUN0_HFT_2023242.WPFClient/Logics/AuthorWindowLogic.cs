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
    public class AuthorWindowLogic : IAuthorWindowLogic
    {
        private WebList<Author> webList;
        private IMessenger messenger;


        public AuthorWindowLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }


        public void Create()
        {
            var author = new Author();
            if (new AuthorUpdateWindow().ShowDialog(ref author) == true)
            {
                webList.Add(author);
                messenger.Send("Author added", "AuthorModification");
            }
        }

        public void Read(Author author)
        {
            new AuthorWindow(webList.Get(author.AuthorID)).ShowDialog();
        }

        public void Update(Author author)
        {
            if (new AuthorUpdateWindow().ShowDialog(ref author) == true)
            {
                webList.Replace(author);
                messenger.Send("Author replaced", "AuthorModification");
            }
        }

        public void Delete(Author author)
        {
            webList.Remove(author.AuthorID);
            messenger.Send("Author removed", "AuthorModification");
        }

        public void ReadAll()
        {
            new AuthorListWindow(webList).ShowDialog();
        }


        public void Setup(WebList<Author> webList)
        {
            this.webList = webList;
        }


        public void HighestRated()
        {
            new AuthorWindow(webList.Get<KeyValuePair<double?, Author>>("Author/HighestRated").Value).ShowDialog();
        }

        public void LowestRated()
        {
            new AuthorWindow(webList.Get<KeyValuePair<double?, Author>>("Author/LowestRated").Value).ShowDialog();
        }

        public void Series(Author author)
        {
            new CollectionListWindow(webList.Post<IEnumerable<Collection>>("Author/Series", author), $"Series of {author.AuthorName}").ShowDialog();
        }

        public void SelectBook(Author author)
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
                new BookWindow(webList.Post<Book>("Author/SelectBook", new Tuple<Author, BookFilter>(author, bookFilter))).ShowDialog();
            }
        }
    }
}
