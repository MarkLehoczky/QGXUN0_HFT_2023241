using QGXUN0_HFT_2023241.Models.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Services
{
    public class WebList<T> : WebService, INotifyCollectionChanged, IEnumerable<T>
    {
        protected NotifyService notifyService;

        protected virtual string Endpoint => typeof(T).Name;

        private List<T> items;

        public T this[int index] => items.ElementAt(index);

        public int Count => items?.Count ?? 0;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public WebList(string url, string endpoint) : base(url, endpoint)
        {
            items = new List<T>();
            Reset();
        }


        protected virtual async Task Reset()
        {
            items = (List<T>)await base.GetListAsync<T>(Endpoint);
            if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)));
            else
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        public virtual async Task<int?> Add(T item) => await base.PostAsync<int?>(Endpoint, item);

        public virtual async Task<bool> Replace(T item) => await base.PutAsync<bool>(Endpoint, item);

        public virtual async Task<bool> Remove(int id) => await base.DeleteAsync<bool>(Endpoint, id);

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

        public T Get(int id) => base.Get<T>(Endpoint, id);
    }



    public class AuthorWebList : WebList<Author>
    {
        public AuthorWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            base.notifyService = new NotifyService(url, hub);

            base.notifyService.AddHandler<Author>("AuthorCreate", item => { base.Reset(); });
            base.notifyService.AddHandler<Author>("AuthorUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Author>("AuthorDelete", item => { base.Reset(); });

            base.notifyService.Init();
        }
    }



    public class BookWebList : WebList<Book>
    {
        public BookWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            base.notifyService = new NotifyService(url, hub);

            base.notifyService.AddHandler<Book>("BookCreate", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("BookUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("BookDelete", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("BookAuthorsUpdate", item => { base.Reset(); });

            base.notifyService.AddHandler<Book>("AuthorUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("AuthorDelete", item => { base.Reset(); });

            base.notifyService.AddHandler<Book>("PublisherUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("PublisherDelete", item => { base.Reset(); });

            base.notifyService.Init();
        }
    }



    public class CollectionWebList : WebList<Collection>
    {
        public CollectionWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            base.notifyService = new NotifyService(url, hub);

            base.notifyService.AddHandler<Collection>("CollectionCreate", item => { base.Reset(); });
            base.notifyService.AddHandler<Collection>("CollectionUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Collection>("CollectionDelete", item => { base.Reset(); });
            base.notifyService.AddHandler<Collection>("CollectionBooksUpdate", item => { base.Reset(); });

            base.notifyService.AddHandler<Book>("BookUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Book>("BookDelete", item => { base.Reset(); });

            base.notifyService.Init();
        }
    }



    public class PublisherWebList : WebList<Publisher>
    {
        public PublisherWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            base.notifyService = new NotifyService(url, hub);

            base.notifyService.AddHandler<Publisher>("PublisherCreate", item => { base.Reset(); });
            base.notifyService.AddHandler<Publisher>("PublisherUpdate", item => { base.Reset(); });
            base.notifyService.AddHandler<Publisher>("PublisherDelete", item => { base.Reset(); });

            base.notifyService.Init();
        }
    }
}
