using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;
using QGXUN0_HFT_2023242.WPFClient.Windows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityUpdateWindows;
using QGXUN0_HFT_2023242.WPFClient.Windows.EntityWindows;
using System;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class PublisherWindowLogic
    {
        private PublisherWebList publisherList;

        public PublisherWindowLogic()
        {
            publisherList = new PublisherWebList();
        }


        public void Create()
        {
            var publisher = new Publisher();
            if (new PublisherUpdateWindow().ShowDialog(ref publisher) == true)
                publisherList?.Create(publisher).GetAwaiter();
        }

        public void Read()
        {
            if (new NumberInputWindow().ShowDialog(out int? id) == true && id != null)
                new PublisherWindow(publisherList.Read(id.Value)).ShowDialog();
        }

        public void Update()
        {
            if (new PublisherListWindow(publisherList, "Select publisher to update").ShowDialog(out Publisher? publisher) == true && publisher != null)
            {
                new PublisherUpdateWindow().ShowDialog(ref publisher);
                publisherList?.Update(publisher);
            }
        }

        public void Delete()
        {
            if (new PublisherListWindow(publisherList, "Select publisher to delete").ShowDialog(out Publisher? publisher) == true && publisher != null)
            {
                publisherList?.Delete(publisher?.PublisherID ?? int.MinValue);
            }
        }

        public void ReadAll()
        {
            new PublisherListWindow(publisherList.Items, "Publishers").ShowDialog();
        }


        public void Series()
        {
            new PublisherListWindow(publisherList.GetList<Publisher>("Publisher/Series"), "Series publishers").ShowDialog();
        }

        public void OnlySeries()
        {
            new PublisherListWindow(publisherList.GetList<Publisher>("Publisher/OnlySeries"), "Only series publishers").ShowDialog();
        }

        public void HighestRated()
        {
            new PublisherWindow(publisherList.Get<KeyValuePair<double?, Publisher>>("Publisher/HighestRated").Value);
        }

        public void LowestRated()
        {
            new PublisherWindow(publisherList.Get<KeyValuePair<double?, Publisher>>("Publisher/LowestRated").Value);
        }

        public void Rating()
        {
            if (new PublisherListWindow(publisherList, "Select publisher to get it's rating").ShowDialog(out Publisher? publisher) == true && publisher != null)
                new SimpleOutput($"Rating of publisher '{publisher.PublisherName}'", publisherList.Post<double?>("Publisher/Rating", publisher)).ShowDialog();
        }

        public void Authors()
        {
            if (new PublisherListWindow(publisherList, "Select publisher to get it's authors").ShowDialog(out Publisher? publisher) == true && publisher != null)
                new AuthorListWindow(publisherList.Post<IEnumerable<Author>>("Publisher/Authors", publisher), $"Authors of publisher '{publisher.PublisherName}'").ShowDialog();
        }

        public void PermanentAuthors()
        {
            new AuthorListWindow(publisherList.GetList<Author>("Publisher/PermanentAuthors"), $"Permanent authors").ShowDialog();
        }

        public void PermanentAuthorsOfPublisher()
        {
            if (new PublisherListWindow(publisherList, "Select publisher to get it's permanent authors").ShowDialog(out Publisher? publisher) == true && publisher != null)
                new AuthorListWindow(publisherList.Post<IEnumerable<Author>>("Publisher/PermanentAuthors", publisher), $"Permanent authors of publisher '{publisher.PublisherName}'").ShowDialog();
        }


        public bool DefaultCondition() => publisherList != null;

        public bool NotEmptyCondition() => publisherList != null && publisherList.Count > 0;
    }
}
