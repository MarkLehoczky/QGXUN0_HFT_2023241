using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.Crud
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public AuthorController(IAuthorLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Post([FromBody] Author value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("AuthorCreate", value);
        }

        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return logic.Read(id);
        }

        [HttpPut]
        public void Put([FromBody] Author value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("AuthorUpdate", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = logic.Read(id);
            logic.Delete(logic.Read(id));
            hub.Clients.All.SendAsync("AuthorDelete", temp);
        }

        [HttpGet]
        public IEnumerable<Author> ReadAll()
        {
            return logic.ReadAll();
        }
    }
}
