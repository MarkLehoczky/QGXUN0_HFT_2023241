using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.Crud
{
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public PublisherController(IPublisherLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Post([FromBody] Publisher value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("Publisher created", value);
        }

        [HttpGet("{id}")]
        public Publisher Get(int id)
        {
            return logic.Read(id);
        }

        [HttpPut]
        public void Put([FromBody] Publisher value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("Publisher updated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = logic.Read(id);
            logic.Delete(logic.Read(id));
            hub.Clients.All.SendAsync("Publisher deleted", temp);
        }

        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return logic.ReadAll();
        }
    }
}
