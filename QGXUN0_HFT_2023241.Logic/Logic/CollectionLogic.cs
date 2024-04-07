using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
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
        /// <inheritdoc/>
        public int Count { get => ReadAll().Count(); }

        /// <inheritdoc/>
        public bool IsEmpty { get => Count == 0; }

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Collection}"/>
        /// </summary>
        private readonly IRepository<Collection> _collectionRepository;

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Book}"/>
        /// </summary>
        private readonly IRepository<Book> _bookRepository;

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{BookCollectionConnector}"/>
        /// </summary>
        private readonly IRepository<BookCollectionConnector> _connectorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> <see langword="class"/> by the <see cref="Repository{Book}"/>, <see cref="Repository{Author}"/> and <see cref="Repository{BookAuthorConnector}"/> instances.
        /// </summary>
        /// <param name="collectionRepository"><see cref="Collection"/> repository instance</param>
        /// <param name="bookRepository"><see cref="Book"/> repository instance</param>
        /// <param name="connectorRepository"><see cref="BookCollectionConnector"/> repository instance</param>
        public CollectionLogic(IRepository<Collection> collectionRepository, IRepository<Book> bookRepository, IRepository<BookCollectionConnector> connectorRepository)
        {
            _collectionRepository = collectionRepository;
            _bookRepository = bookRepository;
            _connectorRepository = connectorRepository;
        }


        /// <summary>
        /// Creates an <see cref="Collection"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> of the <see cref="Collection"/> instance may be changed</remarks>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Collection collection)
        {
            if (!collection.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(collection))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == collection)?.CollectionID;

            if (Read(collection.CollectionID) != null)
                collection.CollectionID = ReadAll().Max(t => t.CollectionID) + 1;

            _collectionRepository.Create(collection);
            return collection.CollectionID;
        }
        /// <summary>
        /// Creates an <see cref="Collection"/> instance, then adds <see cref="Book"/> instances to the <see cref="Collection"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> of the <see cref="Collection"/> instance may be changed</remarks>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Collection collection, IEnumerable<Book> books)
        {
            var created = Create(collection);
            if (created != null)
                AddBooksToCollection(collection, books);

            return created;
        }
        /// <summary>
        /// Creates an <see cref="Collection"/> instance, then adds <see cref="Book"/> instances to the <see cref="Collection"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Collection.CollectionID"/> of the <see cref="Collection"/> instance may be changed</remarks>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Collection collection, params Book[] books)
        {
            return Create(collection, books.AsEnumerable());
        }

        /// <summary>
        /// Reads an <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collectionID"><see cref="Collection.CollectionID"/> of the read <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection"/> instance if the instance is found; otherwise, <see langword="null"/></returns>
        public Collection Read(int collectionID)
        {
            try { return _collectionRepository.Read(collectionID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <inheritdoc/>
        public bool Update(Collection collection)
        {
            if (!collection.IsValid() || Read(collection.CollectionID) == null)
                return false;

            _collectionRepository.Update(collection);
            return true;
        }

        /// <inheritdoc/>
        public bool Delete(Collection collection)
        {
            if (collection == null) return false;
            try { _collectionRepository.Delete(collection.CollectionID); return true; }
            catch { return false; }
        }

        /// <inheritdoc/>
        public IQueryable<Collection> ReadAll()
        {
            return _collectionRepository.ReadAll();
        }


        /// <inheritdoc/>
        public bool AddBooksToCollection(Collection collection, IEnumerable<Book> books)
        {
            if (collection == null || Read(collection.CollectionID) == null)
                return false;

            foreach (var item in _bookRepository.ReadAll().Where(t => books.Contains(t)).AsEnumerable().Except(collection.Books))
                _connectorRepository.Create(new BookCollectionConnector(
                    _connectorRepository.ReadAll().Max(t => t.BookCollectionConnectorID) + 1,
                    item.BookID,
                    collection.CollectionID
                ));

            return books.All(t => collection.Books.Contains(t));
        }
        /// <inheritdoc/>
        public bool AddBooksToCollection(Collection collection, params Book[] books)
        {
            return AddBooksToCollection(collection, books.AsEnumerable());
        }

        /// <inheritdoc/>
        public bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books)
        {
            if (collection == null || Read(collection.CollectionID) == null)
                return false;

            foreach (var item in _connectorRepository.ReadAll().Where(t => t.Collection == collection && books.Contains(t.Book)))
                _connectorRepository.Delete(item.BookCollectionConnectorID);

            return books.All(t => _bookRepository.ReadAll().Contains(t)) && !books.Any(t => collection.Books.Contains(t));
        }
        /// <inheritdoc/>
        public bool RemoveBooksFromCollection(Collection collection, params Book[] books)
        {
            return RemoveBooksFromCollection(collection, books.AsEnumerable());
        }
        /// <inheritdoc/>
        public bool RemoveAllBookFromCollection(Collection collection)
        {
            return RemoveBooksFromCollection(collection, collection.Books);
        }


        /// <inheritdoc/>
        public IEnumerable<Collection> GetAllNonSeries()
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries.Value == false).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Collection> GetAllSeries()
        {
            return ReadAll().Where(t => t.IsSeries.HasValue == true && t.IsSeries.Value == true).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.Max(u => u.Year) >= minimumYear && t.Books.Min(u => u.Year) <= maximumYear).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Collection> GetCollectionsInYear(int year)
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.Any(u => u.Year == year)).ToList();
        }

        /// <inheritdoc/>
        public double? GetPriceOfCollection(Collection collection)
        {
            if (collection == null || !collection.Books.Any()) return null;
            return collection.Books.Sum(t => t.Price);
        }

        /// <inheritdoc/>
        public double? GetRatingOfCollection(Collection collection)
        {
            if (collection == null || !collection.Books.Any()) return null;
            return collection.Books.Average(t => t.Rating);
        }

        /// <inheritdoc/>
        public Book SelectBookFromCollection(Collection collection, BookFilter bookFilter)
        {
            if (collection == null) return null;

            return bookFilter switch
            {
                BookFilter.MostExpensive => collection.Books.Where(t => t.Price != null).OrderByDescending(t => t.Price).FirstOrDefault(),
                BookFilter.HighestRated => collection.Books.Where(t => t.Rating != null).OrderByDescending(t => t.Rating).FirstOrDefault(),
                BookFilter.LeastExpensive => collection.Books.Where(t => t.Price != null).OrderBy(t => t.Price).FirstOrDefault(),
                BookFilter.LowestRated => collection.Books.Where(t => t.Rating != null).OrderBy(t => t.Rating).FirstOrDefault(),
                _ => null,
            };
        }

        /// <inheritdoc/>
        public KeyValuePair<double?, Collection> SelectCollection(BookFilter bookFilter, CollectionFilter collectionFilter = CollectionFilter.Collection)
        {
            IEnumerable<Collection> filtered = null;

            filtered = collectionFilter switch
            {
                CollectionFilter.Series => ReadAll().Where(t => t.IsSeries.HasValue == true && t.IsSeries == true),
                CollectionFilter.NonSeries => ReadAll().Where(t => t.IsSeries.HasValue == false || t.IsSeries == false),
                CollectionFilter.Collection => ReadAll(),
                _ => Enumerable.Empty<Collection>(),
            };

            Collection temp;

            switch (bookFilter)
            {
                case BookFilter.MostExpensive:
                    temp = filtered.Where(t => t.Books.Any(u => u.Price != null)).OrderByDescending(t => t.Books.Sum(u => u.Price)).FirstOrDefault();
                    return new KeyValuePair<double?, Collection>(temp?.Books.Sum(t => t.Price), temp);

                case BookFilter.LeastExpensive:
                    temp = filtered.Where(t => t.Books.Any(u => u.Price != null)).OrderBy(t => t.Books.Sum(u => u.Price)).FirstOrDefault();
                    return new KeyValuePair<double?, Collection>(temp?.Books.Sum(t => t.Price), temp);

                case BookFilter.HighestRated:
                    temp = filtered.Where(t => t.Books.Any(u => u.Rating != null)).OrderByDescending(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
                    return new KeyValuePair<double?, Collection>(temp?.Books.Average(t => t.Rating), temp);

                case BookFilter.LowestRated:
                    temp = filtered.Where(t => t.Books.Any(u => u.Rating != null)).OrderBy(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
                    return new KeyValuePair<double?, Collection>(temp?.Books.Average(t => t.Rating), temp);

                default: return new KeyValuePair<double?, Collection>(null, null);
            }
        }
    }
}