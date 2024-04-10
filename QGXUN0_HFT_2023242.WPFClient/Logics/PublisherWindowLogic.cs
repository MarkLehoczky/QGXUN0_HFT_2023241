using CommunityToolkit.Mvvm.Messaging;
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
    public class PublisherWindowLogic : IPublisherWindowLogic
    {
        private WebList<Publisher> webList;
        private IMessenger messenger;


        public PublisherWindowLogic(IMessenger messenger)
        {
            this.messenger = messenger;
        }


        public void Create()
        {
            var publisher = new Publisher();
            if (new PublisherUpdateWindow().ShowDialog(ref publisher) == true)
            {
                webList.Add(publisher);
                messenger.Send("Publisher added", "PublisherModification");
            }
        }

        public void Read(Publisher publisher)
        {
            if (new NumberInputWindow("Enter the publisher ID").ShowDialog(out int? id) == true
                && id != null)
            {
                new PublisherWindow(webList.Get(id.Value)).ShowDialog();
            }
        }

        public void Update(Publisher publisher)
        {
            if (new PublisherUpdateWindow().ShowDialog(ref publisher) == true)
            {
                webList.Replace(publisher);
                messenger.Send("Publisher replaced", "PublisherModification");
            }
        }

        public void Delete(Publisher publisher)
        {
            webList.Remove(publisher.PublisherID);
            messenger.Send("Publisher removed", "PublisherModification");
        }

        public void ReadAll()
        {
            new PublisherListWindow(webList).ShowDialog();
        }


        public void Setup(WebList<Publisher> webList)
        {
            this.webList = webList;
        }


        public void Series()
        {
            new PublisherListWindow(webList.GetList<Publisher>("Publisher/Series"), "Series publishers").ShowDialog();
        }

        public void OnlySeries()
        {
            new PublisherListWindow(webList.GetList<Publisher>("Publisher/OnlySeries"), "Only series publishers").ShowDialog();
        }

        public void HighestRated()
        {
            new PublisherWindow(webList.Get<KeyValuePair<double?, Publisher>>("Publisher/HighestRated").Value).ShowDialog();
        }

        public void LowestRated()
        {
            new PublisherWindow(webList.Get<KeyValuePair<double?, Publisher>>("Publisher/LowestRated").Value).ShowDialog();
        }

        public void Rating(Publisher publisher)
        {
            new SimpleOutput($"Rating of publisher '{publisher.PublisherName}'", webList.Post<double?>("Publisher/Rating", publisher)).ShowDialog();
        }

        public void Authors(Publisher publisher)
        {
            new AuthorListWindow(webList.Post<IEnumerable<Author>>("Publisher/Authors", publisher), $"Authors of publisher '{publisher.PublisherName}'").ShowDialog();
        }

        public void PermanentAuthors()
        {
            new AuthorListWindow(webList.GetList<Author>("Publisher/PermanentAuthors"), $"Permanent authors").ShowDialog();
        }

        public void PermanentAuthorsOfPublisher(Publisher publisher)
        {
            new AuthorListWindow(webList.Post<IEnumerable<Author>>("Publisher/PermanentAuthors", publisher), $"Permanent authors of publisher '{publisher.PublisherName}'").ShowDialog();
        }
    }
}
