using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.WPFClient.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace QGXUN0_HFT_2023242.WPFClient.Services
{
    public class PublisherWebList : WebService, INotifyCollectionChanged, IEnumerable<Publisher>
    {
        public ObservableCollection<Publisher> Items = new ObservableCollection<Publisher>();
        public int Count => Items?.Count ?? 0;
        public Publisher this[int index] => Items[index];

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private NotifyService notifyService;

        public PublisherWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Publisher>("PublisherCreate", item =>
            {
                Items.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
                PublisherCommand.NotifyChanges();
            });
            notifyService.AddHandler<Publisher>("PublisherUpdate", item =>
            {
                Init();
            });
            notifyService.AddHandler<Publisher>("PublisherDelete", item =>
            {
                Items.Remove(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
                PublisherCommand.NotifyChanges();
            });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items.Clear();
            foreach (var item in await base.GetListAsync<Publisher>("Publisher"))
                Items.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            PublisherCommand.NotifyChanges();
        }

        public async Task<int?> Create(Publisher item)
        {
            return await base.PostAsync<int?>("Publisher", item);
        }

        public Publisher? Read(int id)
        {
            return base.Get<Publisher?>("Publisher", id);
        }

        public async Task<bool> Update(Publisher item)
        {
            return await base.PutAsync<bool>("Publisher", item);
        }

        public async Task<bool> Delete(int id)
        {
            return await base.DeleteAsync<bool>("Publisher", id);
        }

        public IEnumerable<Publisher> ReadAll()
        {
            return base.GetList<Publisher>("Publisher");
        }


        public IEnumerator<Publisher> GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Publisher>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Publisher>().GetEnumerator();
        }
    }
}
