using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    /// <summary>
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Publisher"/> <see langword="class"/>
    /// </summary>
    public interface IPublisherLogic
    {
        /// <summary>
        /// Gets the number of <see cref="Publisher"/> instances.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether there are <see cref="Publisher"/> instances.
        /// </summary>
        /// <value><see langword="true"/> if the are <see cref="Publisher"/> instances; otherwise, <see langword="false"/></value>
        bool IsEmpty { get; }


        /// <summary>
        /// Creates an <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher">New <see cref="Publisher"/> instance</param>
        /// <returns><see cref="Publisher.PublisherID"/> of the <paramref name="publisher"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Publisher publisher);

        /// <summary>
        /// Reads an <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisherID"><see cref="Publisher.PublisherID"/> of the read <see cref="Publisher"/> instance</param>
        /// <returns><see cref="Publisher"/> instance</returns>
        Publisher Read(int publisherID);

        /// <summary>
        /// Updates an <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher">Updated <see cref="Publisher"/> instance</param>
        /// <returns><see langword="true"/> if the updating was successful; otherwise, <see langword="false"/></returns>
        bool Update(Publisher publisher);

        /// <summary>
        /// Deletes an <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance for deleting</param>
        /// <returns><see langword="true"/> if the deleting was successful; otherwise, <see langword="false"/></returns>
        bool Delete(Publisher publisher);

        /// <summary>
        /// Reads all <see cref="Publisher"/> instances.
        /// </summary>
        /// <returns>Every <see cref="Publisher"/> instances</returns>
        IQueryable<Publisher> ReadAll();


        /// <summary>
        /// Gets the <see cref="Author"/> instances from the <see cref="Book.Authors"/> from the <see cref="Publisher.Books"/> of a <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance</param>
        /// <returns><see cref="Author"/> instances of the <see cref="Publisher"/> instance</returns>
        IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher);

        /// <summary>
        /// Gets the <see cref="Publisher"/> instance which has the highest <see langword="average"/> <see cref="Book.Rating"/> from the <see cref="Publisher.Books"/>.
        /// </summary>
        /// <returns>lowest rated <see cref="Publisher"/>, where the <see cref="KeyValuePair"/> <see langword="Key"/> is the average <see cref="Book.Rating"/>, and the <see cref="KeyValuePair"/> <see langword="Value"/> is the <see cref="Publisher"/> instance</returns>
        KeyValuePair<double?, Publisher> GetHighestRatedPublisher();

        /// <summary>
        /// Gets the <see cref="Publisher"/> instance which has the lowest <see langword="average"/> <see cref="Book.Rating"/> from the <see cref="Publisher.Books"/>.
        /// </summary>
        /// <returns>lowest rated <see cref="Publisher"/>, where the <see cref="KeyValuePair"/> <see langword="Key"/> is the average <see cref="Book.Rating"/>, and the <see cref="KeyValuePair"/> <see langword="Value"/> is the <see cref="Publisher"/> instance</returns>
        KeyValuePair<double?, Publisher> GetLowestRatedPublisher();

        /// <summary>
        /// Gets the <see cref="Publisher"/> instances where the any of the <see cref="Book.Collections"/> is series for all <see cref="Publisher.Books"/>.
        /// </summary>
        /// <returns>only series publisher <see cref="Publisher"/> instances</returns>
        IEnumerable<Publisher> GetOnlySeriesPublishers();

        /// <summary>
        /// Gets the <see cref="Author"/> instances from the <see cref="Book.Authors"/> of the <see cref="Publisher.Books"/> where the <see cref="Author"/> has only published at one <see cref="Publisher"/>.
        /// </summary>
        /// <returns>permanent <see cref="Author"/> instances</returns>
        IEnumerable<Author> GetPermanentAuthors();

        /// <summary>
        /// Gets the <see cref="Author"/> instances from the <see cref="Book.Authors"/> of the <see cref="Publisher.Books"/> where the <see cref="Author"/> has only published at one <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance</param>
        /// <returns>permanent <see cref="Author"/> instances of the <see cref="Publisher"/> instance</returns>
        IEnumerable<Author> GetPermanentAuthorsOfPublisher(Publisher publisher);

        /// <summary>
        /// Gets the <see langword="average"/> <see cref="Book.Rating"/> of the <see cref="Publisher.Books"/> of a <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance</param>
        double? GetRatingOfPublisher(Publisher publisher);

        /// <summary>
        /// Gets the <see cref="Publisher"/> instances where the any of the <see cref="Book.Collections"/> is series for any <see cref="Publisher.Books"/>
        /// </summary>
        /// <returns>series publisher <see cref="Publisher"/> instances</returns>
        IEnumerable<Publisher> GetSeriesPublishers();
    }
}