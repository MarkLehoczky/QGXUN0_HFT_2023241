using Microsoft.AspNetCore.Mvc;
using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;

namespace QGXUN0_HFT_2023241.Endpoint.Controllers.NonCrud
{
    [Route("Publisher/[action]")]
    [ApiController]
    public class PublisherLogicController : ControllerBase
    {
        private readonly IPublisherLogic logic;

        public PublisherLogicController(IPublisherLogic logic) { this.logic = logic; }

        [HttpGet] public IEnumerable<Publisher> Series() => logic.GetSeriesPublishers();
        [HttpGet] public IEnumerable<Publisher> OnlySeries() => logic.GetOnlySeriesPublishers();
        [HttpGet] public KeyValuePair<double?, Publisher> HighestRated() => logic.GetHighestRatedPublisher();
        [HttpGet] public KeyValuePair<double?, Publisher> LowestRated() => logic.GetLowestRatedPublisher();
        [HttpPost] public double? Rating([FromBody] Publisher publisher) => logic.GetRatingOfPublisher(logic.Read(publisher.PublisherID));
        [HttpPost] public IEnumerable<Author> Authors([FromBody] Publisher publisher) => logic.GetAuthorsOfPublisher(logic.Read(publisher.PublisherID));
        [HttpGet] public IEnumerable<Author> PermanentAuthors() => logic.GetPermanentAuthors();
        [HttpPost] public IEnumerable<Author> PermanentAuthors([FromBody] Publisher publisher) => logic.GetPermanentAuthorsOfPublisher(logic.Read(publisher.PublisherID));
    }
}
