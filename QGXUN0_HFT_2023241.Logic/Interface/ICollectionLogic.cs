using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    /// <summary>
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Collection"/> <see langword="class"/>
    /// </summary>
    public interface ICollectionLogic
    {
        /// <summary>
        /// Gets the number of <see cref="Collection"/> instances.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether there are <see cref="Collection"/> instances.
        /// </summary>
        /// <value><see langword="true"/> if the are <see cref="Collection"/> instances; otherwise, <see langword="false"/></value>
        bool IsEmpty { get; }


        /// <summary>
        /// Creates an <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Collection collection);
        /// <summary>
        /// Creates an <see cref="Collection"/> instance, then adds <see cref="Book"/> instances to the <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Collection collection, IEnumerable<Book> books);
        /// <summary>
        /// Creates an <see cref="Collection"/> instance, then adds <see cref="Book"/> instances to the <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection">New <see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection.CollectionID"/> of the <paramref name="collection"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Collection collection, params Book[] books);

        /// <summary>
        /// Reads an <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collectionID"><see cref="Collection.CollectionID"/> of the read <see cref="Collection"/> instance</param>
        /// <returns><see cref="Collection"/> instance</returns>
        Collection Read(int collectionID);

        /// <summary>
        /// Updates an <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection">Updated <see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if the updating was successful; otherwise, <see langword="false"/></returns>
        bool Update(Collection collection);

        /// <summary>
        /// Deletes an <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance for deleting</param>
        /// <returns><see langword="true"/> if the deleting was successful; otherwise, <see langword="false"/></returns>
        bool Delete(Collection collection);

        /// <summary>
        /// Reads all <see cref="Collection"/> instances.
        /// </summary>
        /// <returns>Every <see cref="Collection"/> instances</returns>
        IQueryable<Collection> ReadAll();


        /// <summary>
        /// Adds <see cref="Book"/> instances to the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if every adding was successful; otherwise, <see langword="false"/></returns>
        bool AddBooksToCollection(Collection collection, IEnumerable<Book> books);
        /// <summary>
        /// Adds <see cref="Book"/> instances to the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances for the <see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if every adding was successful; otherwise, <see langword="false"/></returns>
        bool AddBooksToCollection(Collection collection, params Book[] books);

        /// <summary>
        /// Removes <see cref="Book"/> instances from the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances from the <see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if every removing was successful; otherwise, <see langword="false"/></returns>
        bool RemoveBooksFromCollection(Collection collection, IEnumerable<Book> books);
        /// <summary>
        /// Removes <see cref="Book"/> instances from the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <param name="books"><see cref="Book"/> instances from the <see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if every removing was successful; otherwise, <see langword="false"/></returns>
        bool RemoveBooksFromCollection(Collection collection, params Book[] books);
        /// <summary>
        /// Removes all <see cref="Book"/> instances from the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <returns><see langword="true"/> if every removing was successful; otherwise, <see langword="false"/></returns>
        bool RemoveAllBookFromCollection(Collection collection);


        /// <summary>
        /// Gets all <see cref="Collection"/> instances where the <see cref="Collection.IsSeries"/> of <see cref="Collection"/> instance has value and the value is true.
        /// </summary>
        /// <returns>all <see cref="Collection"/> instances which are not series</returns>
        IEnumerable<Collection> GetAllNonSeries();

        /// <summary>
        /// Gets all <see cref="Collection"/> instances where the <see cref="Collection.IsSeries"/> of <see cref="Collection"/> instance does not have value or the value is false.
        /// </summary>
        /// <returns>all <see cref="Collection"/> instances which are series</returns>
        IEnumerable<Collection> GetAllSeries();

        /// <summary>
        /// Gets all the <see cref="Collection"/> instances where any of the <see cref="Book.Year"/> of the <see cref="Collection.Books"/> is between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>.
        /// </summary>
        /// <param name="minimumYear">Minimum <see cref="Book.Year"/></param>
        /// <param name="maximumYear">Maximum <see cref="Book.Year"/></param>
        /// <returns><see cref="Collection"/> instances with <see cref="Book.Year"/> between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/></returns>
        IEnumerable<Collection> GetCollectionsBetweenYears(int minimumYear, int maximumYear);

        /// <summary>
        /// Gets all the <see cref="Collection"/> instances where any of the <see cref="Book.Year"/> of the <see cref="Collection.Books"/> matches the <paramref name="year"/>.
        /// </summary>
        /// <param name="year"><see cref="Book.Year"/></param>
        /// <returns><see cref="Collection"/> instances with <see cref="Book.Year"/> matching the <paramref name="year"/></returns>
        IEnumerable<Collection> GetCollectionsInYear(int year);

        /// <summary>
        /// Gets the <see langword="summarized"/> <see cref="Book.Price"/> of the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <returns>price of the <see cref="Collection"/> instance</returns>
        double? GetPriceOfCollection(Collection collection);

        /// <summary>
        /// Gets the <see langword="average"/> <see cref="Book.Rating"/> of the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <returns>rating of the <see cref="Collection"/> instance</returns>
        double? GetRatingOfCollection(Collection collection);

        /// <summary>
        /// Gets a <see cref="Book"/> instance from the <see cref="Collection.Books"/> of a <see cref="Collection"/> instance by the <paramref name="bookFilter"/>.
        /// </summary>
        /// <param name="collection"><see cref="Collection"/> instance</param>
        /// <param name="bookFilter">Filter option for the <see cref="Book"/> instances</param>
        /// <returns><see cref="Book"/> instance of the <see cref="Collection"/> instance by the <paramref name="bookFilter"/></returns>
        Book SelectBookFromCollection(Collection collection, BookFilter bookFilter);

        /// <summary>
        /// Gets the <see cref="Collection"/> instance by the <paramref name="bookFilter"/> and <paramref name="collectionFilter"/>
        /// </summary>
        /// <param name="bookFilter">Filter option for the <see cref="Book"/> instances</param>
        /// <param name="collectionFilter">Filter option for the <see cref="Collection"/> instances</param>
        /// <returns>selected <see cref="Collection"/>, where the <see cref="KeyValuePair"/> <see langword="Key"/> is the <paramref name="bookFilter"/> option's value, and the <see cref="KeyValuePair"/> <see langword="Value"/> is the <see cref="Collection"/> instance</returns>
        KeyValuePair<double?, Collection> SelectCollection(BookFilter bookFilter, CollectionFilter collectionFilter = CollectionFilter.Collection);
    }
}