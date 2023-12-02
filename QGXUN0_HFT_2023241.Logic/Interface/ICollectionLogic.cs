using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface ICollectionLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool AddBooksToCollection(Collection collection, IEnumerable<Book> books);
        bool AddBooksToCollection(Collection collection, params Book[] books);
        ExtendedCollection ConvertCollectionToExtendedCollection(Collection collection);
        int? Create(Collection collection);
        int? Create(Collection collection, IEnumerable<Book> books);
        int? Create(Collection collection, params Book[] books);
        bool Delete(Collection collection);
        IEnumerable<ExtendedCollection> GetAllAsExtendedCollection();
        IEnumerable<Collection> GetAllNonSeries();
        IEnumerable<Collection> GetAllSeries();
        IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Collection> GetCollectionsInYear(int year);
        double? GetPriceOfCollection(Collection collection);
        double? GetRatingOfCollection(Collection collection);
        Collection Read(int collectionID);
        IQueryable<Collection> ReadAll();
        bool RemoveAllBookFromCollection(Collection collection);
        bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books);
        bool RemoveBooksFromCollection(Collection collection, params Book[] books);
        Book SelectBookFromCollection(Collection collection, BookFilter bookFilter);
        KeyValuePair<double, Collection> SelectCollection(BookFilter bookFilter, CollectionFilter collectionFilter = CollectionFilter.Collection);
        bool Update(Collection collection);
    }
}