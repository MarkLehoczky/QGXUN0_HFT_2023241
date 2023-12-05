using Microsoft.AspNetCore.Mvc;
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

        public BookController(IBookLogic logic) { this.logic = logic; }

        [HttpPost] public void Post([FromBody] Book value) => logic.Create(value);
        [HttpGet("{id}")] public Book Get(int id) => logic.Read(id);
        [HttpPut] public void Put([FromBody] Book value) => logic.Update(value);
        [HttpDelete("{id}")] public void Delete(int id) => logic.Delete(logic.Read(id));
        [HttpGet] public IEnumerable<Book> ReadAll() => logic.ReadAll();
    }
}
