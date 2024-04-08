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
    public class AuthorWindowLogic
    {
        private AuthorWebList authorList;

        public AuthorWindowLogic()
        {
            authorList = new AuthorWebList();
        }


        public void Create()
        {
            var author = new Author();
            if (new AuthorUpdateWindow().ShowDialog(ref author) == true)
                authorList?.Create(author).GetAwaiter();
        }

        public void Read()
        {
            if (new NumberInputWindow().ShowDialog(out int? id) == true && id != null)
                new AuthorWindow(authorList.Read(id.Value)).ShowDialog();
        }

        public void Update()
        {
            if (new AuthorListWindow(authorList.Items, "Select author to update").ShowDialog(out Author? author) == true && author != null)
            {
                new AuthorUpdateWindow().ShowDialog(ref author);
                authorList?.Update(author);
            }
        }

        public void Delete()
        {
            if (new AuthorListWindow(authorList.Items, "Select author to delete").ShowDialog(out Author? author) == true && author != null)
            {
                authorList?.Delete(author?.AuthorID ?? int.MinValue);
            }
        }

        public void ReadAll()
        {
            new AuthorListWindow(authorList.Items, "Authors").ShowDialog();
        }


        public void HighestRated()
        {
            new AuthorWindow(authorList.Get<KeyValuePair<double?, Author>>("Author/HighestRated").Value).ShowDialog();
        }

        public void LowestRated()
        {
            new AuthorWindow(authorList.Get<KeyValuePair<double?, Author>>("Author/LowestRated").Value).ShowDialog();
        }

        public void Series()
        {
            if (new AuthorListWindow(authorList.Items, "Select an author to list their series").ShowDialog(out Author? author) == true && author != null)
                new CollectionListWindow(authorList.Post<IEnumerable<Collection>>("Author/Series", author), $"Series of {author.AuthorName}").ShowDialog();
        }

        public void SelectBook()
        {
            if (new AuthorListWindow(authorList.Items, "Select an author").ShowDialog(out Author? author) == true && author != null
                && new ItemSelectorWindow(new List<object>(){
                BookFilter.MostExpensive,
                BookFilter.LeastExpensive,
                BookFilter.HighestRated,
                BookFilter.LowestRated,
            }).ShowDialog(out object? filter) == true && filter != null && filter is BookFilter)
                new BookWindow(authorList.Post<Book>("Author/SelectBook", new Tuple<Author, BookFilter>(author, (BookFilter)filter))).ShowDialog();
        }


        public bool DefaultCondition() => authorList != null;

        public bool NotEmptyCondition() => authorList != null && authorList.Count > 0;
    }
}
