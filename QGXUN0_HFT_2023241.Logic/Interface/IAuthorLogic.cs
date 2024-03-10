using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    /// <summary>
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Author"/> <see langword="class"/>
    /// </summary>
    public interface IAuthorLogic
    {
        /// <summary>
        /// Gets the number of <see cref="Author"/> instances.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether there are <see cref="Author"/> instances.
        /// </summary>
        /// <value><see langword="true"/> if the are <see cref="Author"/> instances; otherwise, <see langword="false"/></value>
        bool IsEmpty { get; }


        /// <summary>
        /// Creates an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="author">New <see cref="Author"/> instance</param>
        /// <returns><see cref="Author.AuthorID"/> of the <paramref name="author"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Author author);

        /// <summary>
        /// Reads an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="authorID"><see cref="Author.AuthorID"/> of the read <see cref="Author"/> instance</param>
        /// <returns><see cref="Author"/> instance</returns>
        Author Read(int authorID);

        /// <summary>
        /// Updates an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="author">Updated <see cref="Author"/> instance</param>
        /// <returns><see langword="true"/> if the updating was successful; otherwise, <see langword="false"/></returns>
        bool Update(Author author);

        /// <summary>
        /// Deletes an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="author"><see cref="Author"/> instance for deleting</param>
        /// <returns><see langword="true"/> if the deleting was successful; otherwise, <see langword="false"/></returns>
        bool Delete(Author author);

        /// <summary>
        /// Reads all <see cref="Author"/> instances.
        /// </summary>
        /// <returns>Every <see cref="Author"/> instances</returns>
        IQueryable<Author> ReadAll();


        /// <summary>
        /// Gets the <see cref="Author"/> instance which has the highest <see langword="average"/> <see cref="Book.Rating"/> from the <see cref="Author.Books"/>.
        /// </summary>
        /// <returns>lowest rated <see cref="Author"/>, where the <see cref="KeyValuePair"/> <see langword="Key"/> is the average <see cref="Book.Rating"/>, and the <see cref="KeyValuePair"/> <see langword="Value"/> is the <see cref="Author"/> instance</returns>
        KeyValuePair<double?, Author> GetHighestRatedAuthor();

        /// <summary>
        /// Gets the <see cref="Author"/> instance which has the lowest <see langword="average"/> <see cref="Book.Rating"/> from the <see cref="Author.Books"/>.
        /// </summary>
        /// <returns>lowest rated <see cref="Author"/>, where the <see cref="KeyValuePair"/> <see langword="Key"/> is the average <see cref="Book.Rating"/>, and the <see cref="KeyValuePair"/> <see langword="Value"/> is the <see cref="Author"/> instance</returns>
        KeyValuePair<double?, Author> GetLowestRatedAuthor();

        /// <summary>
        /// Gets the series <see cref="Collection"/> instances from the <see cref="Author.Books"/> of an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="author"><see cref="Author"/> instance</param>
        /// <returns>Series <see cref="Collection"/> instances of the <see cref="Author"/> instance</returns>
        IEnumerable<Collection> GetSeriesOfAuthor(Author author);

        /// <summary>
        /// Gets a <see cref="Book"/> instance from an <see cref="Author"/> instance by the <paramref name="bookFilter"/>.
        /// </summary>
        /// <param name="author"><see cref="Author"/> instance</param>
        /// <param name="bookFilter">Filter option for the <see cref="Book"/> instances</param>
        /// <returns><see cref="Book"/> instance of the <see cref="Author"/> instance by the <paramref name="bookFilter"/></returns>
        Book SelectBookFromAuthor(Author author, BookFilter bookFilter);
    }
}