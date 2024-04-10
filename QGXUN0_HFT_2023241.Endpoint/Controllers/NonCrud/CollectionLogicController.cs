using Microsoft.AspNetCore.Mvc;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System;
using QGXUN0_HFT_2023241.Models.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.NonCrud
{
    [Route("Collection/[action]")]
    [ApiController]
    public class CollectionLogicController : ControllerBase
    {
        private readonly ICollectionLogic logic;
        private readonly IHubContext<SignalHub> hub;

        public CollectionLogicController(ICollectionLogic logic, IHubContext<SignalHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [Route("enum")]
        [HttpPut]
        public bool AddBooks([FromBody] Tuple<Collection, IEnumerable<Book>> tuple)
        {
            var temp = logic.AddBooksToCollection(logic.Read(tuple.Item1.CollectionID), tuple.Item2);
            hub.Clients.All.SendAsync("CollectionBooksUpdate", tuple.Item1);
            return temp;
        }

        [Route("array")]
        [HttpPut]
        public bool AddBooks([FromBody] Tuple<Collection, Book[]> tuple)
        {
            var temp = logic.AddBooksToCollection(logic.Read(tuple.Item1.CollectionID), tuple.Item2);
            hub.Clients.All.SendAsync("CollectionBooksUpdate", tuple.Item1);
            return temp;
        }

        [Route("enum")]
        [HttpPut]
        public bool RemoveBooks([FromBody] Tuple<Collection, IEnumerable<Book>> tuple)
        {
            var temp = logic.RemoveBooksFromCollection(logic.Read(tuple.Item1.CollectionID), tuple.Item2);
            hub.Clients.All.SendAsync("CollectionBooksUpdate", tuple.Item1);
            return temp;
        }

        [Route("array")]
        [HttpPut]
        public bool RemoveBooks([FromBody] Tuple<Collection, Book[]> tuple)
        {
            var temp = logic.RemoveBooksFromCollection(logic.Read(tuple.Item1.CollectionID), tuple.Item2);
            hub.Clients.All.SendAsync("CollectionBooksUpdate", tuple.Item1);
            return temp;
        }

        [HttpPut]
        public bool ClearBooks([FromBody] Collection collection)
        {
            var temp = logic.RemoveAllBookFromCollection(logic.Read(collection.CollectionID));
            hub.Clients.All.SendAsync("CollectionBooksUpdate", collection);
            return temp;
        }

        [HttpGet] public IEnumerable<Collection> Series() => logic.GetAllSeries();
        [HttpGet] public IEnumerable<Collection> NonSeries() => logic.GetAllNonSeries();
        [HttpGet] public IEnumerable<Collection> InYear([FromQuery] int year) => logic.GetCollectionsInYear(year);
        [HttpGet] public IEnumerable<Collection> BetweenYears([FromQuery] int min, [FromQuery] int max) => logic.GetCollectionsBetweenYears(min, max);
        [HttpPost] public double? Price([FromBody] Collection collection) => logic.GetPriceOfCollection(logic.Read(collection.CollectionID));
        [HttpPost] public double? Rating([FromBody] Collection collection) => logic.GetRatingOfCollection(logic.Read(collection.CollectionID));

        [HttpPost] public KeyValuePair<double?, Collection> Select([FromBody] Tuple<BookFilter, CollectionFilter> tuple) => logic.SelectCollection(tuple.Item1, tuple.Item2);
        [HttpPost] public Book SelectBook([FromBody] Tuple<Collection, BookFilter> tuple) => logic.SelectBookFromCollection(logic.Read(tuple.Item1.CollectionID), tuple.Item2);
    }
}
