using Microsoft.AspNetCore.Mvc;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System;
using QGXUN0_HFT_2023241.Models.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.NonCrud
{
    [Route("Book/[action]")]
    [ApiController]
    public class BookLogicController : ControllerBase
    {
        private readonly IBookLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public BookLogicController(IBookLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [Route("enum")]
        [HttpPut]
        public bool AddAuthors([FromBody] Tuple<Book, IEnumerable<Author>> tuple)
        {
            var temp = logic.AddAuthorsToBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
            hub.Clients.All.SendAsync("BookAuthorsUpdate", tuple.Item1);
            return temp;
        }

        [Route("array")]
        [HttpPut]
        public bool AddAuthors([FromBody] Tuple<Book, Author[]> tuple)
        {
            var temp = logic.AddAuthorsToBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
            hub.Clients.All.SendAsync("BookAuthorsUpdate", tuple.Item1);
            return temp;
        }

        [Route("enum")]
        [HttpPut]
        public bool RemoveAuthors([FromBody] Tuple<Book, IEnumerable<Author>> tuple)
        {
            var temp = logic.RemoveAuthorsFromBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
            hub.Clients.All.SendAsync("BookAuthorsUpdate", tuple.Item1);
            return temp;
        }

        [Route("array")]
        [HttpPut]
        public bool RemoveAuthors([FromBody] Tuple<Book, Author[]> tuple)
        {
            var temp = logic.RemoveAuthorsFromBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
            hub.Clients.All.SendAsync("BookAuthorsUpdate", tuple.Item1);
            return temp;
        }

        [HttpGet] public IEnumerable<Book> InYear([FromQuery] int year) => logic.GetBooksInYear(year);
        [HttpGet] public IEnumerable<Book> BetweenYears([FromQuery] int min, [FromQuery] int max) => logic.GetBooksBetweenYears(min, max);
        [HttpGet] public IEnumerable<Book> TitleContains([FromBody] string text) => logic.GetBooksWithTitle(text);
        [Route("enum")][HttpPost] public IDictionary<string, IEnumerable<Book>> TitleContains([FromBody] IEnumerable<string> texts) => logic.GetBooksWithTitles(texts);
        [Route("array")][HttpPost] public IDictionary<string, IEnumerable<Book>> TitleContains([FromBody] string[] texts) => logic.GetBooksWithTitles(texts);

        [HttpPost] public Book Select([FromBody] BookFilter filter) => logic.SelectBook(filter);
    }
}
