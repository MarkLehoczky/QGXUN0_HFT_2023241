using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Logic
{
    /// <summary>
    /// Implements all CRUD and non-crud methods for the <see cref="Collection"/> model
    /// </summary>
    public class CollectionLogic
    {
        /// <summary>
        /// Repository for the <see cref="Collection"/> database context
        /// </summary>
        private readonly IRepository<Collection> collectionRepository;
        /// <summary>
        /// Repository for the <see cref="Book"/> database context
        /// </summary>
        private readonly IRepository<Book> bookRepository;
        /// <summary>
        /// Repository for the <see cref="BookCollectionConnector"/> database context
        /// </summary>
        private readonly IRepository<BookCollectionConnector> connectorRepository;

        /// <summary>
        /// Counts the number of <see cref="Collection"/> instances
        /// </summary>
        /// <value>number of <see cref="Collection"/> instances</value>
        public int Count { get => ReadAll().Count(); }
        /// <summary>
        /// Determines whether there are <see cref="Collection"/> instances in the database
        /// </summary>
        /// <value><see langword="true"/> if there are <see cref="Collection"/> instances in the database, otherwise <see langword="false"/></value>
        public bool IsEmpty { get => Count == 0; }


        /// <summary>
        /// Constructor with the database repositories
        /// </summary>
        /// <param name="collectionRepository"><see cref="Collection"/> repository</param>
        /// <param name="bookRepository"><see cref="Book"/> repository</param>
        /// <param name="connectorRepository"><see cref="BookCollectionConnector"/> repository</param>
        public CollectionLogic(IRepository<Collection> collectionRepository, IRepository<Book> bookRepository, IRepository<BookCollectionConnector> connectorRepository)
        {
            this.collectionRepository = collectionRepository;
            this.bookRepository = bookRepository;
            this.connectorRepository = connectorRepository;
        }


        /// <summary>
        /// Creates an empty <paramref name="collection"/>
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> may be changed if another <see cref="Collection"/> instance has the same <see langword="key"/></remarks>
        /// <param name="collection">new collection</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> if the collection is valid, otherwise <see langword="null"/></returns>
        public int? Create(Collection collection)
        {
            // if the collection attributes are not valid (through ValidationAttribute), then returns
            if (!collection.IsValid())
                return null;

            // if the collection already exists, then returns
            // else if the ID already exists, gives a new ID to the collection
            var read = Read(collection.CollectionID);
            if (read == collection)
                return collection.CollectionID;
            else if (read != null)
                collection.CollectionID = ReadAll().Max(t => t.CollectionID) + 1;

            // creates the collection, then returns the ID
            collectionRepository.Create(collection);
            return collection.CollectionID;
        }
        /// <summary>
        /// Creates a <paramref name="collection"/> with books
        /// </summary>
        /// <remarks><para>The <see cref="Collection.CollectionID"/> may be changed if another <see cref="Collection"/> instance has the same <see langword="key"/></para><para>The books must be in the database</para></remarks>
        /// <param name="collection">new collection</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the books in the collection</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> if the collection is valid, otherwise <see langword="null"/></returns>
        public int? Create(Collection collection, IEnumerable<int> bookIDs)
        {
            var created = Create(collection);
            if (created != null)
                AddBooksToCollection(collection, bookIDs);

            return created;
        }
        /// <summary>
        /// Creates a <paramref name="collection"/> with books
        /// </summary>
        /// <remarks><para>The <see cref="Collection.CollectionID"/> may be changed if another <see cref="Collection"/> instance has the same <see langword="key"/></para><para>The books must be in the database</para></remarks>
        /// <param name="collection">new collection</param>
        /// <param name="books">books in the collection</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> if the collection is valid, otherwise <see langword="null"/></returns>
        public int? Create(Collection collection, IEnumerable<Book> books)
        {
            return Create(collection, books.Select(t => t.BookID));
        }
        /// <summary>
        /// Creates a <paramref name="collection"/> with books
        /// </summary>
        /// <remarks><para>The <see cref="Collection.CollectionID"/> may be changed if another <see cref="Collection"/> instance has the same <see langword="key"/></para><para>The books must be in the database</para></remarks>
        /// <param name="collection">new collection</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the books in the collection</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> if the collection is valid, otherwise <see langword="null"/></returns>
        public int? Create(Collection collection, params int[] bookIDs)
        {
            return Create(collection, bookIDs.AsEnumerable());
        }
        /// <summary>
        /// Creates a <paramref name="collection"/> with books
        /// </summary>
        /// <remarks><para>The <see cref="Collection.CollectionID"/> may be changed if another <see cref="Collection"/> instance has the same <see langword="key"/></para><para>The books must be in the database</para></remarks>
        /// <param name="collection">new collection</param>
        /// <param name="books">books in the collection</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> if the collection is valid, otherwise <see langword="null"/></returns>
        public int? Create(Collection collection, params Book[] books)
        {
            return Create(collection, books.Select(t => t.BookID));
        }

        /// <summary>
        /// Reads a <see cref="Collection"/> with the same <paramref name="collectionID"/> value
        /// </summary>
        /// <param name="collectionID"><see cref="Collection.CollectionID"/> value of the collection</param>
        /// <returns><see cref="Collection"/> if the collection exists, otherwise <see langword="null"/></returns>
        public Collection Read(int collectionID)
        {
            try { return collectionRepository.Read(collectionID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <summary>
        /// Updates a <paramref name="collection"/> with the same <see cref="Collection.CollectionID"/> value
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> value of the <paramref name="collection"/> must be the same as the one intended to update</remarks>
        /// <param name="collection">updated collection</param>
        /// <returns><see langword="true"/> if the update was successful, otherwise <see langword="false"/></returns>
        public bool Update(Collection collection)
        {
            if (!collection.IsValid() || Read(collection.CollectionID) == null)
                return false;

            collectionRepository.Update(collection);
            return true;
        }

        /// <summary>
        /// Deletes a <see cref="Collection"/> with the same <see cref="Collection.CollectionID"/>
        /// </summary>
        /// <param name="collectionID"><see cref="Collection.CollectionID"/> of the <see cref="Collection"/></param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(int collectionID)
        {
            try { collectionRepository.Delete(collectionID); }
            catch (InvalidOperationException) { return false; }
            return Read(collectionID) == null;
        }
        /// <summary>
        /// Deletes a <see cref="Collection"/> with the same <paramref name="collection"/>
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Collection collection)
        {
            if (collection == null) return false;
            return Delete(collection.CollectionID);
        }

        /// <summary>
        /// Reads all <see cref="Collection"/>
        /// </summary>
        /// <returns>all <see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadAll()
        {
            return collectionRepository.ReadAll();
        }


        /// <summary>
        /// Reads a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collectionIDs"><see cref="Collection.CollectionID"/> values of the collections</param>
        /// <returns><see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadRange(IEnumerable<int> collectionIDs)
        {
            return ReadAll().Where(t => collectionIDs.Any(u => u == t.CollectionID));
        }
        /// <summary>
        /// Reads a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collections"><see cref="Collection"/> instances</param>
        /// <returns><see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadRange(IEnumerable<Collection> collections)
        {
            return ReadAll().Where(t => collections.Any(u => u == t));
        }
        /// <summary>
        /// Reads a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collectionIDs"><see cref="Collection.CollectionID"/> values of the collections</param>
        /// <returns><see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadRange(params int[] collectionIDs)
        {
            return ReadAll().Where(t => collectionIDs.Any(u => u == t.CollectionID));
        }
        /// <summary>
        /// Reads a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collections"><see cref="Collection"/> instances</param>
        /// <returns><see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadRange(params Collection[] collections)
        {
            return ReadAll().Where(t => collections.Any(u => u == t));
        }
        /// <summary>
        /// Reads a range of <see cref="Collection"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Collection.CollectionID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Collection.CollectionID"/></param>
        /// <returns><see cref="Collection"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Collection> ReadBetween(int minimumID, int maximumID)
        {
            return ReadAll().Where(t => t.CollectionID >= minimumID && t.CollectionID <= maximumID);
        }

        /// <summary>
        /// Updates a range of <paramref name="collections"/> with the same <see cref="Collection.CollectionID"/> values
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> values of the <paramref name="collections"/> must be the same as the ones intended to update</remarks>
        /// <param name="collections">updated collections</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(IEnumerable<Collection> collections)
        {
            bool successful = true;

            foreach (var item in collections)
                if (!Update(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Updates a range of <paramref name="collections"/> with the same <see cref="Collection.CollectionID"/> values
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> values of the <paramref name="collections"/> must be the same as the ones intended to update</remarks>
        /// <param name="collections">updated collections</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(params Collection[] collections)
        {
            return UpdateRange(collections.AsEnumerable());
        }

        /// <summary>
        /// Deletes a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collectionIDs"><see cref="Collection.CollectionID"/> values of the <see cref="Collection"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<int> collectionIDs)
        {
            bool successful = true;

            foreach (var item in collectionIDs)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collections"><see cref="Collection"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<Collection> collections)
        {
            bool successful = true;

            foreach (var item in collections)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collectionIDs"><see cref="Collection.CollectionID"/> values of the <see cref="Collection"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params int[] collectionIDs)
        {
            return DeleteRange(collectionIDs.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Collection"/> instances
        /// </summary>
        /// <param name="collections"><see cref="Collection"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params Collection[] collections)
        {
            return DeleteRange(collections.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Collection"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Collection.CollectionID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Collection.CollectionID"/></param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteBetween(int minimumID, int maximumID)
        {
            return DeleteRange(ReadBetween(minimumID, maximumID));
        }
        /// <summary>
        /// Deletes every <see cref="Collection"/> instances
        /// </summary>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteAll()
        {
            return DeleteRange(ReadAll());
        }


        /// <summary>
        /// Determines whether the <see cref="Collection"/> instances contains the <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">searched collection</param>
        /// <returns><see langword="true"/> if the <paramref name="collection"/> was found, otherwise <see langword="false"/></returns>
        public bool Contains(Collection collection)
        {
            return ReadAll().Contains(collection);
        }
        /// <summary>
        /// Determines whether the <see cref="Collection"/> instances contains any of the <paramref name="collections"/>
        /// </summary>
        /// <param name="collections">searched collections</param>
        /// <returns><see langword="true"/> if any of the <paramref name="collections"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(IEnumerable<Collection> collections)
        {
            return collections.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Collection"/> instances contains any of the <paramref name="collections"/>
        /// </summary>
        /// <param name="collections">searched collections</param>
        /// <returns><see langword="true"/> if any of the <paramref name="collections"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(params Collection[] collections)
        {
            return collections.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Collection"/> instances contains all the <paramref name="collections"/>
        /// </summary>
        /// <param name="collections">searched collections</param>
        /// <returns><see langword="true"/> if all the <paramref name="collections"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(IEnumerable<Collection> collections)
        {
            return collections.All(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Collection"/> instances contains all the <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">searched collections</param>
        /// <returns><see langword="true"/> if all the <paramref name="collection"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(params Collection[] collection)
        {
            return collection.All(t => Contains(t));
        }


        /// <summary>
        /// Adds books to a <paramref name="collection"/>
        /// </summary>
        /// <remarks>The books must be in the database</remarks>
        /// <param name="collection">collection of the books</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the addable books</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddBooksToCollection(Collection collection, IEnumerable<int> bookIDs)
        {
            if (collection == null || Read(collection.CollectionID) == null)
                return false;

            collection.Books ??= new List<Book>();
            int count = collection.Books.Count;

            foreach (var item in bookRepository.ReadAll().Where(t => bookIDs.Contains(t.BookID)))
                collection.Books.Add(item);

            connectorRepository.SaveChanges();
            return bookIDs.All(t => collection.Books.Any(u => u.BookID == t));
        }
        /// <summary>
        /// Adds books to a <paramref name="collection"/>
        /// </summary>
        /// <remarks>The books must be in the database</remarks>
        /// <param name="collection">collection of the books</param>
        /// <param name="books">addable books</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddBooksToCollection(Collection collection, IEnumerable<Book> books)
        {
            if (collection == null || Read(collection.CollectionID) == null)
                return false;

            collection.Books ??= new List<Book>();
            int count = collection.Books.Count;

            foreach (var item in bookRepository.ReadAll().Where(t => books.Contains(t)))
                collection.Books.Add(item);

            connectorRepository.SaveChanges();
            return books.All(t => collection.Books.Contains(t));
        }
        /// <summary>
        /// Adds books to a <paramref name="collection"/>
        /// </summary>
        /// <remarks>The books must be in the database</remarks>
        /// <param name="collection">collection of the books</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the addable books</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddBooksToCollection(Collection collection, params int[] bookIDs)
        {
            return AddBooksToCollection(collection, bookIDs.AsEnumerable());
        }
        /// <summary>
        /// Adds books to a <paramref name="collection"/>
        /// </summary>
        /// <remarks>The books must be in the database</remarks>
        /// <param name="collection">collection of the books</param>
        /// <param name="books">addable books</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddBooksToCollection(Collection collection, params Book[] books)
        {
            return AddBooksToCollection(collection, books.AsEnumerable());
        }

        /// <summary>
        /// Removes books from a <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection of the books</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the removable books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveBooksFromCollection(Collection collection, IEnumerable<int> bookIDs)
        {
            if (collection == null || collection.Books == null) return false;

            foreach (var item in bookRepository.ReadAll().Where(t => bookIDs.Contains(t.BookID)))
                collection.Books.Remove(item);

            connectorRepository.SaveChanges();
            return !collection.Books.Any(t => bookIDs.Contains(t.BookID));
        }
        /// <summary>
        /// Removes books from a <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection of the books</param>
        /// <param name="books">removable books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books)
        {
            if (collection == null || collection.Books == null) return false;

            foreach (var item in bookRepository.ReadAll().Where(t => books.Contains(t)))
                collection.Books.Remove(item);

            connectorRepository.SaveChanges();
            return !collection.Books.Any(t => books.Contains(t));
        }
        /// <summary>
        /// Removes books from a <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection of the books</param>
        /// <param name="bookIDs"><see cref="Book.BookID"/> of the removable books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveBooksFromCollection(Collection collection, params int[] bookIDs)
        {
            return RemoveBooksFromCollection(collection, bookIDs.AsEnumerable());
        }
        /// <summary>
        /// Removes books from a <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection of the books</param>
        /// <param name="books">removable books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveBooksFromCollection(Collection collection, params Book[] books)
        {
            return RemoveBooksFromCollection(collection, books.AsEnumerable());
        }
        /// <summary>
        /// Removes all books from a <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection of the books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAllBookFromCollection(Collection collection)
        {
            return RemoveBooksFromCollection(collection, collection.Books);
        }


        /// <summary>
        /// Returns all <see cref="Collection"/> which is a series
        /// </summary>
        /// <returns>all series collections</returns>
        public IEnumerable<Collection> GetAllSeries()
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == true && t.IsSeries.Value == true).ToList();
        }
        /// <summary>
        /// Returns all <see cref="Collection"/> which is not a series
        /// </summary>
        /// <returns>all non-series collections</returns>
        public IEnumerable<Collection> GetAllNonSeries()
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries.Value == false).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Collection"/> which is a series with at least one <see cref="Book"/> released in the given <paramref name="year"/>
        /// </summary>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all series in the given <paramref name="year"/></returns>
        public IEnumerable<Collection> GetSeriesInYear(int year)
        {
            return ReadAll().Where(t => t.IsSeries.HasValue && t.IsSeries.Value == true)
                .Intersect(ReadAll().Where(t => t.Books.Any(u => u.Year == year)))
                .ToList();
        }
        /// <summary>
        /// Returns all <see cref="Collection"/> which is not a series with at least one <see cref="Book"/> released in the given <paramref name="year"/>
        /// </summary>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all series in the given <paramref name="year"/></returns>
        public IEnumerable<Collection> GetNonSeriesInYear(int year)
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries.Value == false)
                .Intersect(ReadAll().Where(t => t.Books.Any(u => u.Year == year)))
                .ToList();
        }
        /// <summary>
        /// Returns all <see cref="Collection"/> with at least one <see cref="Book"/> released in the given <paramref name="year"/>
        /// </summary>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all collection in the given <paramref name="year"/></returns>
        public IEnumerable<Collection> GetCollectionsInYear(int year)
        {
            return ReadAll().Where(t => t.Books.Count != 0 && t.Books.Any(u => u.Year == year)).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Collection"/> which is a series between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>
        /// </summary>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all series in the given interval</returns>
        public IEnumerable<Collection> GetSeriesBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.IsSeries.HasValue && t.IsSeries.Value == true)
                .Intersect(ReadAll().Where(t => t.Books.Count != 0 && t.Books.Max(u => u.Year) >= minimumYear && t.Books.Min(u => u.Year) <= maximumYear))
                .ToList();
        }
        /// <summary>
        /// Returns all <see cref="Collection"/> which is not a series between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>
        /// </summary>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all series in the given interval</returns>
        public IEnumerable<Collection> GetNonSeriesBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries.Value == false)
                .Intersect(ReadAll().Where(t => t.Books.Count != 0 && t.Books.Max(u => u.Year) >= minimumYear && t.Books.Min(u => u.Year) <= maximumYear))
                .ToList();
        }
        /// <summary>
        /// Returns all <see cref="Collection"/> between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>
        /// </summary>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all collection in the given interval</returns>
        public IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.Books.Count != 0 && t.Books.Max(u => u.Year) >= minimumYear && t.Books.Min(u => u.Year) <= maximumYear).ToList();
        }

        /// <summary>
        /// Creates <see cref="Collection"/> instances from the <see cref="Publisher"/> values
        /// </summary>
        public void CreatePublisherCollections()
        {
            bookRepository.ReadAll().Select(t => t.Publisher).Where(u => u != null).Distinct().ToList().ForEach(v => Create(new Collection(1, v.PublisherName), v.Books));
        }

        /// <summary>
        /// Returns the <see cref="Collection"/> instances grouped by their number of <see cref="Collection.Books"/>
        /// </summary>
        /// <returns>grouped collections</returns>
        public IEnumerable<IGrouping<int, Collection>> GroupByNumberOfBooks()
        {
            return ReadAll().Where(t => t.Books.Count != 0).OrderBy(t => t.Books.Count).ToList().GroupBy(u => u.Books.Count);
        }
    }
}