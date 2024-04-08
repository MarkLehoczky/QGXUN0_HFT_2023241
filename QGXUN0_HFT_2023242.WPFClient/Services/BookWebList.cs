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
    public class BookWebList : WebService
    {
        public IList<Book> Items = new List<Book>();
        public int Count => Items?.Count ?? 0;
        public Book this[int index] => Items[index];

        private NotifyService notifyService;

        public BookWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Book>("BookCreate", item => { Init(); });
            notifyService.AddHandler<Book>("BookUpdate", item => { Init(); });
            notifyService.AddHandler<Book>("BookDelete", item => { Init(); });
            notifyService.AddHandler<Book>("BookAuthorsUpdate", item => { Init(); });

            notifyService.AddHandler<Book>("AuthorUpdate", item => { Init(); });
            notifyService.AddHandler<Book>("AuthorDelete", item => { Init(); });

            notifyService.AddHandler<Book>("PublisherUpdate", item => { Init(); });
            notifyService.AddHandler<Book>("PublisherDelete", item => { Init(); });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items = (IList<Book>) await base.GetListAsync<Book>("Book");
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
    }
}
