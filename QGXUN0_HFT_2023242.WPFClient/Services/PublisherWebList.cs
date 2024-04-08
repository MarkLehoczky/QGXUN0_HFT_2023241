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
    public class PublisherWebList : WebService
    {
        public IList<Publisher> Items = new List<Publisher>();
        public int Count => Items?.Count ?? 0;
        public Publisher this[int index] => Items[index];

        private NotifyService notifyService;

        public PublisherWebList(string url = "http://localhost:43016/", string endpoint = "", string hub = "api") : base(url, endpoint)
        {
            notifyService = new NotifyService(url + hub);

            notifyService.AddHandler<Publisher>("PublisherCreate", item => { Init(); });
            notifyService.AddHandler<Publisher>("PublisherUpdate", item => { Init(); });
            notifyService.AddHandler<Publisher>("PublisherDelete", item => { Init(); });

            notifyService.Init();
            Init();
        }


        private async Task Init()
        {
            Items = (IList<Publisher>)await base.GetListAsync<Publisher>("Publisher");
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
    }
}
