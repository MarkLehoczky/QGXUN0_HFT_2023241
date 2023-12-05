using Microsoft.AspNetCore.Mvc;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System;
using QGXUN0_HFT_2023241.Models.Extensions;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.NonCrud
{
    [Route("Author/[action]")]
    [ApiController]
    public class AuthorLogicController : ControllerBase
    {
        private readonly IAuthorLogic logic;

        public AuthorLogicController(IAuthorLogic logic) { this.logic = logic; }

        [HttpGet] public KeyValuePair<double?, Author> HighestRated() => logic.GetHighestRatedAuthor();
        [HttpGet] public KeyValuePair<double?, Author> LowestRated() => logic.GetLowestRatedAuthor();
        [HttpPost] public IEnumerable<Collection> Series([FromBody] Author author) => logic.GetSeriesOfAuthor(logic.Read(author.AuthorID));
        [HttpPost] public Book SelectBook([FromBody] Tuple<Author, BookFilter> tuple) => logic.SelectBookFromAuthor(logic.Read(tuple.Item1.AuthorID), tuple.Item2);
    }
}
