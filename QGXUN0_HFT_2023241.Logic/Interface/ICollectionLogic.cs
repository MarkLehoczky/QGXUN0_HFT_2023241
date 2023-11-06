using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface ICollectionLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool AddBooksToCollection(Collection collection, IEnumerable<Book> books);
        bool AddBooksToCollection(Collection collection, IEnumerable<int> bookIDs);
        bool AddBooksToCollection(Collection collection, params Book[] books);
        bool AddBooksToCollection(Collection collection, params int[] bookIDs);
        bool Contains(Collection collection);
        bool ContainsAll(IEnumerable<Collection> collections);
        bool ContainsAll(params Collection[] collection);
        bool ContainsAny(IEnumerable<Collection> collections);
        bool ContainsAny(params Collection[] collections);
        int? Create(Collection collection);
        int? Create(Collection collection, IEnumerable<Book> books);
        int? Create(Collection collection, IEnumerable<int> bookIDs);
        int? Create(Collection collection, params Book[] books);
        int? Create(Collection collection, params int[] bookIDs);
        void CreatePublisherCollections();
        bool Delete(Collection collection);
        bool Delete(int collectionID);
        bool DeleteAll();
        bool DeleteBetween(int minimumID, int maximumID);
        bool DeleteRange(IEnumerable<Collection> collections);
        bool DeleteRange(IEnumerable<int> collectionIDs);
        bool DeleteRange(params Collection[] collections);
        bool DeleteRange(params int[] collectionIDs);
        IEnumerable<Collection> GetAllNonSeries();
        IEnumerable<Collection> GetAllSeries();
        IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Collection> GetCollectionsInYear(int year);
        IEnumerable<Collection> GetNonSeriesBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Collection> GetNonSeriesInYear(int year);
        IEnumerable<Collection> GetSeriesBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Collection> GetSeriesInYear(int year);
        IEnumerable<IGrouping<int, Collection>> GroupByNumberOfBooks();
        Collection Read(int collectionID);
        IQueryable<Collection> ReadAll();
        IQueryable<Collection> ReadBetween(int minimumID, int maximumID);
        IQueryable<Collection> ReadRange(IEnumerable<Collection> collections);
        IQueryable<Collection> ReadRange(IEnumerable<int> collectionIDs);
        IQueryable<Collection> ReadRange(params Collection[] collections);
        IQueryable<Collection> ReadRange(params int[] collectionIDs);
        bool RemoveAllBookFromCollection(Collection collection);
        bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books);
        bool RemoveBooksFromCollection(Collection collection, IEnumerable<int> bookIDs);
        bool RemoveBooksFromCollection(Collection collection, params Book[] books);
        bool RemoveBooksFromCollection(Collection collection, params int[] bookIDs);
        bool Update(Collection collection);
        bool UpdateRange(IEnumerable<Collection> collections);
        bool UpdateRange(params Collection[] collections);
    }
}