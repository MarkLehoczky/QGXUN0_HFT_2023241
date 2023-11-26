using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IPublisherLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        ExtendedPublisher ConvertPublisherToExtendedPublisher(Publisher publisher);
        int? Create(Publisher publisher);
        bool Delete(Publisher publisher);
        IEnumerable<ExtendedPublisher> GetAllAsExtendedPublisher();
        IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher);
        KeyValuePair<double, Publisher> GetHighestRatedPublisher();
        KeyValuePair<double, Publisher> GetLowestRatedPublisher();
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