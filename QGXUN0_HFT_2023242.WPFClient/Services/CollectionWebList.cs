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
    public class CollectionWebList : WebService, INotifyCollectionChanged, IEnumerable<Collection>
    {
        public ObservableCollection<Collection> Items = new ObservableCollection<Collection>();
        public int Count => Items?.Count ?? 0;
        public Collection this[int index] => Items[index];

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private NotifyService notifyService;

        public CollectionWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Collection>("CollectionCreate", item =>
            {
                Items.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
                CollectionCommand.NotifyChanges();
            });
            notifyService.AddHandler<Collection>("CollectionUpdate", item =>
            {
                Init();
            });
            notifyService.AddHandler<Collection>("CollectionDelete", item =>
            {
                Items.Remove(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
                CollectionCommand.NotifyChanges();
            });
            notifyService.AddHandler<Collection>("CollectionBooksUpdate", item =>
            {
                Init();
            });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items.Clear();
            foreach (var item in await base.GetListAsync<Collection>("Collection"))
                Items.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            CollectionCommand.NotifyChanges();
        }

        public async Task<int?> Create(Collection item)
        {
            return await base.PostAsync<int?>("Collection", item);
        }

        public Collection? Read(int id)
        {
            return base.Get<Collection?>("Collection", id);
        }

        public async Task<bool> Update(Collection item)
        {
            return await base.PutAsync<bool>("Collection", item);
        }

        public async Task<bool> Delete(int id)
        {
            return await base.DeleteAsync<bool>("Collection", id);
        }

        public IEnumerable<Collection> ReadAll()
        {
            return base.GetList<Collection>("Collection");
        }


        public IEnumerator<Collection> GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Collection>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Collection>().GetEnumerator();
        }
    }
}
