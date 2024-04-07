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
    public class AuthorWebList : WebService, INotifyCollectionChanged, IEnumerable<Author>
    {
        public ObservableCollection<Author> Items = new ObservableCollection<Author>();
        public int Count => Items?.Count ?? 0;
        public Author this[int index] => Items[index];

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private NotifyService notifyService;

        public AuthorWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Author>("AuthorCreate", item =>
            {
                Items.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
                AuthorCommand.NotifyChanges();
            });
            notifyService.AddHandler<Author>("AuthorUpdate", item =>
            {
                Init();
            });
            notifyService.AddHandler<Author>("AuthorDelete", item =>
            {
                Items.Remove(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
                AuthorCommand.NotifyChanges();
            });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items.Clear();
            foreach (var item in await base.GetListAsync<Author>("Author"))
                Items.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            AuthorCommand.NotifyChanges();
        }

        public async Task<int?> Create(Author item)
        {
            return await base.PostAsync<int?>("Author", item);
        }

        public Author? Read(int id)
        {
            return base.Get<Author?>("Author", id);
        }

        public async Task<bool> Update(Author item)
        {
            return await base.PutAsync<bool>("Author", item);
        }

        public async Task<bool> Delete(int id)
        {
            return await base.DeleteAsync<bool>("Author", id);
        }

        public IEnumerable<Author> ReadAll()
        {
            return base.GetList<Author>("Author");
        }


        public IEnumerator<Author> GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Author>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Author>().GetEnumerator();
        }
    }
}
