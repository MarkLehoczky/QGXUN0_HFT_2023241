using Microsoft.AspNetCore.Mvc;
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

        public AuthorController(IAuthorLogic logic) { this.logic = logic; }

        [HttpPost] public void Post([FromBody] Author value) => logic.Create(value);
        [HttpGet("{id}")] public Author Get(int id) => logic.Read(id);
        [HttpPut] public void Put([FromBody] Author value) => logic.Update(value);
        [HttpDelete("{id}")] public void Delete(int id) => logic.Delete(logic.Read(id));
        [HttpGet] public IEnumerable<Author> ReadAll() => logic.ReadAll();
    }
}
