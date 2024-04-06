using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.Services
{
    class WebObservableCollection<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        NotifyService notify;
        WebService web;
        List<T> items;
        bool hasSignal;
        Type type = typeof(T);

        public WebObservableCollection(string url, string hub = null)
        {
            hasSignal = hub != null;
            this.web = new WebService(url);
            if (hub != null)
            {
                this.notify = new NotifyService(url + hub);
                this.notify.AddHandler<T>(type.Name + "Created", (T item) =>
                {
                    items.Add(item);
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                });
                this.notify.AddHandler<T>(type.Name + "Deleted", (T item) =>
                {
                    var element = items.FirstOrDefault(t => t.Equals(item));
                    if (element != null)
                    {
                        items.Remove(item);
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    else
                    {
                        Init();
                    }

                });
                this.notify.AddHandler<T>(type.Name + "Updated", (T item) =>
                {
                    Init();
                });

                this.notify.Init();
            }
            Init();
        }

        private async Task Init()
        {
            items = (List<T>)await web.GetListAsync<T>(typeof(T).Name);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (items != null)
            {
                return items.GetEnumerator();
            }
            else return new List<T>().GetEnumerator();
        }

        public void Add(T item)
        {
            if (hasSignal)
            {
                this.web.PostAsync<T>(typeof(T).Name, item);
            }
            else
            {
                this.web.PostAsync<T>(typeof(T).Name, item).ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
            }

        }

        public void Update(T item)
        {
            if (hasSignal)
            {
                this.web.PutAsync<T>(typeof(T).Name, item);
            }
            else
            {
                this.web.PutAsync<T>(typeof(T).Name, item).ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
            }
        }

        public void Delete(int id)
        {
            if (hasSignal)
            {
                this.web.DeleteAsync<T>(typeof(T).Name, id);
            }
            else
            {
                this.web.DeleteAsync<T>(typeof(T).Name, id).ContinueWith((t) =>
                {
                    Init().ContinueWith(z =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        });
                    });
                });
            }
        }
    }
}
