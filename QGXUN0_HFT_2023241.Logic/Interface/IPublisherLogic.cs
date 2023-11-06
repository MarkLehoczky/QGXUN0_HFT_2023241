using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IPublisherLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool Contains(Publisher publisher);
        bool ContainsAll(IEnumerable<Publisher> publishers);
        bool ContainsAll(params Publisher[] publisher);
        bool ContainsAny(IEnumerable<Publisher> publishers);
        bool ContainsAny(params Publisher[] publishers);
        int? Create(Publisher publisher);
        bool Delete(int publisherID);
        bool Delete(Publisher publisher);
        bool DeleteAll();
        bool DeleteBetween(int minimumID, int maximumID);
        bool DeleteRange(IEnumerable<int> publisherIDs);
        bool DeleteRange(IEnumerable<Publisher> publishers);
        bool DeleteRange(params int[] publisherIDs);
        bool DeleteRange(params Publisher[] publishers);
        IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher);
        IEnumerable<Book> GetBooksFromPublisherBetweenYears(Publisher publisher, int minimumYear, int maximumYear);
        IEnumerable<Book> GetBooksFromPublisherInYear(Publisher publisher, int year);
        IEnumerable<Publisher> GetOnlySeriesPublishers();
        IEnumerable<Author> GetPermanentAuthors();
        IEnumerable<Author> GetPermanentAuthorsOfPublisher(Publisher publisher);
        IEnumerable<Publisher> GetSeriesPublishers();
        IEnumerable<IGrouping<int, Publisher>> GroupByNumberOfBooks();
        Publisher Read(int publisherID);
        IQueryable<Publisher> ReadAll();
        IQueryable<Publisher> ReadBetween(int minimumID, int maximumID);
        IQueryable<Publisher> ReadRange(IEnumerable<int> publisherIDs);
        IQueryable<Publisher> ReadRange(IEnumerable<Publisher> publishers);
        IQueryable<Publisher> ReadRange(params int[] publisherIDs);
        IQueryable<Publisher> ReadRange(params Publisher[] publishers);
        bool Update(Publisher publisher);
        bool UpdateRange(IEnumerable<Publisher> publishers);
        bool UpdateRange(params Publisher[] publishers);
    }
}