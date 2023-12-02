using QGXUN0_HFT_2023241.Logic.Interface;
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
    public class CollectionLogic : ICollectionLogic
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
            if (!collection.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(collection))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == collection)?.CollectionID;

            if (Read(collection.CollectionID) != null)
                collection.CollectionID = ReadAll().Max(t => t.CollectionID) + 1;

            collectionRepository.Create(collection);
            return collection.CollectionID;
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
            var created = Create(collection);
            if (created != null)
                AddBooksToCollection(collection, books);

            return created;
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
            return Create(collection, books.AsEnumerable());
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
        /// Deletes a <see cref="Collection"/> with the same <paramref name="collection"/>
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Collection collection)
        {
            if (collection == null) return false;
            try { collectionRepository.Delete(collection.CollectionID); return true; }
            catch { return false; }
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

            foreach (var item in bookRepository.ReadAll().Where(t => books.Contains(t)).Intersect(bookRepository.ReadAll().Where(t => !collection.Books.Contains(t))))
                connectorRepository.Create(new BookCollectionConnector(
                    connectorRepository.ReadAll().Max(t => t.BookCollectionConnectorID) + 1,
                    item.BookID,
                    collection.CollectionID
                ));

            return books.All(t => collection.Books.Contains(t));
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
        /// <param name="books">removable books</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books)
        {
            if (collection == null || Read(collection.CollectionID) == null)
                return false;

            foreach (var item in connectorRepository.ReadAll().Where(t => t.Collection == collection && books.Contains(t.Book)))
                connectorRepository.Delete(item.BookCollectionConnectorID);

            return books.All(t => bookRepository.ReadAll().Contains(t)) && !books.Any(t => collection.Books.Contains(t));
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
        /// Returns all collection as <see cref="ExtendedCollection"/>
        /// </summary>
        /// <returns>all collection as <see cref="ExtendedCollection"/></returns>
        public IEnumerable<ExtendedCollection> GetAllAsExtendedCollection()
        {
            return ReadAll().ToList().Select(t => ConvertCollectionToExtendedCollection(t));
        }

        /// <summary>
        /// Converts a collection to a <see cref="ExtendedCollection"/>
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>collection as <see cref="ExtendedCollection"/></returns>
        public ExtendedCollection ConvertCollectionToExtendedCollection(Collection collection)
        {
            if (collection == null) return null;

            return new ExtendedCollection(collection,
                collection.Books == null ? Enumerable.Empty<Author>() : collection.Books.SelectMany(u => u.Authors, (u, authors) => authors).Distinct(),
                GetPriceOfCollection(collection),
                GetRatingOfCollection(collection));
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
        /// Select the collections based on the given <paramref name="bookFilter"/> and <paramref name="collectionFilter"/>
        /// </summary>
        /// <param name="bookFilter">book filter</param>
        /// <param name="collectionFilter">collection filter</param>
        /// <returns>selected collection, where the <see langword="Key"/> is the <paramref name="bookFilter"/> option's value and the <see langword="Value"/> is the <see cref="Collection"/></returns>
        public KeyValuePair<double, Collection> SelectCollection(BookFilter bookFilter, CollectionFilter collectionFilter = CollectionFilter.Collection)
        {
            IEnumerable<Collection> filtered = null;

            switch (collectionFilter)
            {
                case CollectionFilter.Series: filtered = ReadAll().Where(t => t.IsSeries.HasValue == true && t.IsSeries == true); break;
                case CollectionFilter.NonSeries: filtered = ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries == false); break;
                case CollectionFilter.Collection: filtered = ReadAll(); break;
                default: filtered = Enumerable.Empty<Collection>(); break;
            }

            switch (bookFilter)
            {
                case BookFilter.MostExpensive:
                    return filtered.Where(t => t.Books.Any(u => u.Price != null)).OrderByDescending(t => t.Books.Sum(u => u.Price))
                    .Select(v => new KeyValuePair<double, Collection>((double)v.Books.Sum(u => u.Price), v))
                    .FirstOrDefault();

                case BookFilter.LeastExpensive:
                    return filtered.Where(t => t.Books.Any(u => u.Price != null)).OrderBy(t => t.Books.Sum(u => u.Price))
                    .Select(v => new KeyValuePair<double, Collection>((double)v.Books.Sum(u => u.Price), v))
                    .FirstOrDefault();

                case BookFilter.HighestRated:
                    return filtered.Where(t => t.Books.Any(u => u.Rating != null)).OrderByDescending(t => t.Books.Average(u => u.Rating))
                    .Select(v => new KeyValuePair<double, Collection>((double)v.Books.Average(u => u.Rating), v))
                    .FirstOrDefault();

                case BookFilter.LowestRated:
                    return filtered.Where(t => t.Books.Any(u => u.Rating != null)).OrderBy(t => t.Books.Average(u => u.Rating))
                    .Select(v => new KeyValuePair<double, Collection>((double)v.Books.Average(u => u.Rating), v))
                    .FirstOrDefault();

                default: return new KeyValuePair<double, Collection>(0, null);
            }
        }

        /// <summary>
        /// Selects a book based on the given <paramref name="bookFilter"/> from an <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection</param>
        /// <param name="bookFilter">book filter</param>
        /// <returns>selected book</returns>
        public Book SelectBookFromCollection(Collection collection, BookFilter bookFilter)
        {
            if (collection == null) return null;

            switch (bookFilter)
            {
                case BookFilter.MostExpensive: return collection.Books.Where(t => t.Price != null).OrderByDescending(t => t.Price).FirstOrDefault();
                case BookFilter.HighestRated: return collection.Books.Where(t => t.Rating != null).OrderByDescending(t => t.Rating).FirstOrDefault();
                case BookFilter.LeastExpensive: return collection.Books.Where(t => t.Price != null).OrderBy(t => t.Price).FirstOrDefault();
                case BookFilter.LowestRated: return collection.Books.Where(t => t.Rating != null).OrderBy(t => t.Rating).FirstOrDefault();
                default: return null;
            }
        }

        /// <summary>
        /// Returns the summarized price of all the books of the <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection</param>
        /// <returns>price of the collection</returns>
        public double? GetPriceOfCollection(Collection collection)
        {
            if (collection == null || !collection.Books.Any()) return null;
            return collection.Books.Sum(t => t.Price);
        }

        /// <summary>
        /// Returns the average rating of all the books of the <paramref name="collection"/>
        /// </summary>
        /// <param name="collection">collection</param>
        /// <returns>average rating of the collection</returns>
        public double? GetRatingOfCollection(Collection collection)
        {
            if (collection == null || !collection.Books.Any()) return null;
            return collection.Books.Average(t => t.Rating);
        }

        /// <summary>
        /// Returns all <see cref="Collection"/> with at least one <see cref="Book"/> released in the given <paramref name="year"/>
        /// </summary>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all collection in the given <paramref name="year"/></returns>
        public IEnumerable<Collection> GetCollectionsInYear(int year)
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.Any(u => u.Year == year)).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Collection"/> between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>
        /// </summary>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all collection in the given interval</returns>
        public IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.Max(u => u.Year) >= minimumYear && t.Books.Min(u => u.Year) <= maximumYear).ToList();
        }
    }
}