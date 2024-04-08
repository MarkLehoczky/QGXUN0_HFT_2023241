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
    public class AuthorWebList : WebService
    {
        public IList<Author> Items = new List<Author>();
        public int Count => Items?.Count ?? 0;
        public Author this[int index] => Items[index];

        private NotifyService notifyService;

        public AuthorWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Author>("AuthorCreate", item => { Init(); });
            notifyService.AddHandler<Author>("AuthorUpdate", item => { Init(); });
            notifyService.AddHandler<Author>("AuthorDelete", item => { Init(); });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items = (IList<Author>)await base.GetListAsync<Author>("Author");
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
    }
}
