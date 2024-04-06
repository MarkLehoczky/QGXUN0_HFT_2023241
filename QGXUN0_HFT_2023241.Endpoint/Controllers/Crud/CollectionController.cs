using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.Crud
{
    [Route("[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public CollectionController(ICollectionLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Post([FromBody] Collection value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("Collection created", value);
        }

        [HttpGet("{id}")]
        public Collection Get(int id)
        {
            return logic.Read(id);
        }

        [HttpPut]
        public void Put([FromBody] Collection value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("Collection updated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = logic.Read(id);
            logic.Delete(logic.Read(id));
            hub.Clients.All.SendAsync("Collection deleted", temp);
        }

        [HttpGet]
        public IEnumerable<Collection> ReadAll()
        {
            return logic.ReadAll();
        }
    }
}
