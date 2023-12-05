using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IPublisherLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        int? Create(Publisher publisher);
        bool Delete(Publisher publisher);
        IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher);
        KeyValuePair<double?, Publisher> GetHighestRatedPublisher();
        KeyValuePair<double?, Publisher> GetLowestRatedPublisher();
        IEnumerable<Publisher> GetOnlySeriesPublishers();
        IEnumerable<Author> GetPermanentAuthors();
        IEnumerable<Author> GetPermanentAuthorsOfPublisher(Publisher publisher);
        double? GetRatingOfPublisher(Publisher publisher);
        IEnumerable<Publisher> GetSeriesPublishers();
        Publisher Read(int publisherID);
        IQueryable<Publisher> ReadAll();
        bool Update(Publisher publisher);
    }
}