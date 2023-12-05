using Microsoft.AspNetCore.Mvc;
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

        public CollectionController(ICollectionLogic logic) { this.logic = logic; }

        [HttpPost] public void Post([FromBody] Collection value) => logic.Create(value);
        [HttpGet("{id}")] public Collection Get(int id) => logic.Read(id);
        [HttpPut] public void Put([FromBody] Collection value) => logic.Update(value);
        [HttpDelete("{id}")] public void Delete(int id) => logic.Delete(logic.Read(id));
        [HttpGet] public IEnumerable<Collection> ReadAll() => logic.ReadAll();
    }
}
