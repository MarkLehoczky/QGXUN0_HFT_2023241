using Microsoft.AspNetCore.Mvc;
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

        public PublisherController(IPublisherLogic logic) { this.logic = logic; }

        [HttpPost] public void Post([FromBody] Publisher value) => logic.Create(value);
        [HttpGet("{id}")] public Publisher Get(int id) => logic.Read(id);
        [HttpPut] public void Put([FromBody] Publisher value) => logic.Update(value);
        [HttpDelete("{id}")] public void Delete(int id) => logic.Delete(logic.Read(id));
        [HttpGet] public IEnumerable<Publisher> ReadAll() => logic.ReadAll();
    }
}
