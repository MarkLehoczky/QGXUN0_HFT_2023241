using Microsoft.AspNetCore.Mvc;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.NonCrud
{
    [Route("Book/[action]")]
    [ApiController]
    public class BookLogicController : ControllerBase
    {
        private readonly IBookLogic logic;

        public BookLogicController(IBookLogic logic) { this.logic = logic; }

        [Route("enum")][HttpPut] public bool AddAuthors([FromBody] Tuple<Book, IEnumerable<Author>> tuple) => logic.AddAuthorsToBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
        [Route("array")][HttpPut] public bool AddAuthors([FromBody] Tuple<Book, Author[]> tuple) => logic.AddAuthorsToBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
        [Route("enum")][HttpPut] public bool RemoveAuthors([FromBody] Tuple<Book, IEnumerable<Author>> tuple) => logic.RemoveAuthorsFromBook(logic.Read(tuple.Item1.BookID), tuple.Item2);
        [Route("array")][HttpPut] public bool RemoveAuthors([FromBody] Tuple<Book, Author[]> tuple) => logic.RemoveAuthorsFromBook(logic.Read(tuple.Item1.BookID), tuple.Item2);

        [HttpGet] public IEnumerable<Book> InYear([FromQuery] int year) => logic.GetBooksInYear(year);
        [HttpGet] public IEnumerable<Book> BetweenYears([FromQuery] int min, [FromQuery] int max) => logic.GetBooksBetweenYears(min, max);
        [HttpGet] public IEnumerable<Book> TitleContains([FromBody] string text) => logic.GetBooksWithTitle(text);
        [Route("enum")][HttpPost] public IDictionary<string, IEnumerable<Book>> TitleContains([FromBody] IEnumerable<string> texts) => logic.GetBooksWithTitles(texts);
        [Route("array")][HttpPost] public IDictionary<string, IEnumerable<Book>> TitleContains([FromBody] string[] texts) => logic.GetBooksWithTitles(texts);

        [HttpPost] public Book Select([FromBody] BookFilter filter) => logic.SelectBook(filter);
    }
}
