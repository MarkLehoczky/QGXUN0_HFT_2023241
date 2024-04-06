using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.Crud
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public BookController(IBookLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpPost]
        public void Post([FromBody] Book value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("Book created", value);
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return logic.Read(id);
        }

        [HttpPut]
        public void Put([FromBody] Book value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("Book updated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var temp = logic.Read(id);
            logic.Delete(logic.Read(id));
            hub.Clients.All.SendAsync("Book deleted", temp);
        }

        [HttpGet]
        public IEnumerable<Book> ReadAll()
        {
            return logic.ReadAll();
        }
    }
}
