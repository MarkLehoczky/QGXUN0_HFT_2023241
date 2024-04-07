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
    public class BookWebList : WebService, INotifyCollectionChanged, IEnumerable<Book>
    {
        public ObservableCollection<Book> Items = new ObservableCollection<Book>();
        public int Count => Items?.Count ?? 0;
        public Book this[int index] => Items[index];

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private NotifyService notifyService;

        public BookWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Book>("BookCreate", item =>
            {
                Items.Add(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
                BookCommand.NotifyChanges();
            });
            notifyService.AddHandler<Book>("BookUpdate", item =>
            {
                Init();
            });
            notifyService.AddHandler<Book>("BookDelete", item =>
            {
                Items.Remove(item);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
                BookCommand.NotifyChanges();
            });
            notifyService.AddHandler<Book>("BookAuthorsUpdate", item =>
            {
                Init();
            });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items.Clear();
            foreach (var item in await base.GetListAsync<Book>("Book"))
                Items.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            BookCommand.NotifyChanges();
        }

        public async Task<int?> Create(Book item)
        {
            return await base.PostAsync<int?>("Book", item);
        }

        public Book? Read(int id)
        {
            return base.Get<Book?>("Book", id);
        }

        public async Task<bool> Update(Book item)
        {
            return await base.PutAsync<bool>("Book", item);
        }

        public async Task<bool> Delete(int id)
        {
            return await base.DeleteAsync<bool>("Book", id);
        }

        public IEnumerable<Book> ReadAll()
        {
            return base.GetList<Book>("Book");
        }


        public IEnumerator<Book> GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Book>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items?.GetEnumerator() ?? new ObservableCollection<Book>().GetEnumerator();
        }
    }
}
