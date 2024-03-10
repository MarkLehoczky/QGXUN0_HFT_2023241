using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    /// <summary>
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Book"/> <see langword="class"/>
    /// </summary>
    public interface IBookLogic
    {
        /// <summary>
        /// Gets the number of <see cref="Book"/> instances.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether there are <see cref="Book"/> instances.
        /// </summary>
        /// <value><see langword="true"/> if the are <see cref="Book"/> instances; otherwise, <see langword="false"/></value>
        bool IsEmpty { get; }


        /// <summary>
        /// Creates an <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Book book);
        /// <summary>
        /// Creates an <see cref="Book"/> instance, then adds <see cref="Author"/> instances to the <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Book book, IEnumerable<Author> authors);
        /// <summary>
        /// Creates an <see cref="Book"/> instance, then adds <see cref="Author"/> instances to the <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        int? Create(Book book, params Author[] authors);

        /// <summary>
        /// Reads an <see cref="Book"/> instance.
        /// </summary>
        /// <param name="bookID"><see cref="Book.BookID"/> of the read <see cref="Book"/> instance</param>
        /// <returns><see cref="Book"/> instance</returns>
        Book Read(int bookID);

        /// <summary>
        /// Updates an <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book">Updated <see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if the updating was successful; otherwise, <see langword="false"/></returns>
        bool Update(Book book);

        /// <summary>
        /// Deletes an <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance for deleting</param>
        /// <returns><see langword="true"/> if the deleting was successful; otherwise, <see langword="false"/></returns>
        bool Delete(Book book);

        /// <summary>
        /// Reads all <see cref="Book"/> instances.
        /// </summary>
        /// <returns>Every <see cref="Book"/> instances</returns>
        IQueryable<Book> ReadAll();


        /// <summary>
        /// Adds <see cref="Author"/> instances to the <see cref="Book.Authors"/> of a <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if every adding was successful; otherwise, <see langword="false"/></returns>
        bool AddAuthorsToBook(Book book, IEnumerable<Author> authors);
        /// <summary>
        /// Adds <see cref="Author"/> instances to the <see cref="Book.Authors"/> of a <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if every adding was successful; otherwise, <see langword="false"/></returns>
        bool AddAuthorsToBook(Book book, params Author[] authors);

        /// <summary>
        /// Removes <see cref="Author"/> instances from the <see cref="Book.Authors"/> of a <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances from the <see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if every removing was successful; otherwise, <see langword="false"/></returns>
        bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors);
        /// <summary>
        /// Removes <see cref="Author"/> instances from the <see cref="Book.Authors"/> of a <see cref="Book"/> instance.
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances from the <see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if every removing was successful; otherwise, <see langword="false"/></returns>
        bool RemoveAuthorsFromBook(Book book, params Author[] authors);


        /// <summary>
        /// Gets all the <see cref="Book"/> instances where the <see cref="Book.Year"/> is between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>.
        /// </summary>
        /// <param name="minimumYear">Minimum <see cref="Book.Year"/></param>
        /// <param name="maximumYear">Maximum <see cref="Book.Year"/></param>
        /// <returns><see cref="Book"/> instances between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/></returns>
        IEnumerable<Book> GetBooksBetweenYears(int minimumYear, int maximumYear);

        /// <summary>
        /// Gets all the <see cref="Book"/> instances where the <see cref="Book.Year"/> matches the <paramref name="year"/>.
        /// </summary>
        /// <param name="year"><see cref="Book.Year"/></param>
        /// <returns><see cref="Book"/> instances with the <paramref name="year"/></returns>
        IEnumerable<Book> GetBooksInYear(int year);

        /// <summary>
        /// Gets all <see cref="Book"/> instances where the <see cref="Book.Title"/> contains the <paramref name="text"/>
        /// </summary>
        /// <param name="text"><see cref="Book.Title"/> subtext</param>
        /// <returns><see cref="Book"/> instances with the <paramref name="text"/></returns>
        IEnumerable<Book> GetBooksWithTitle(string text);
        /// <summary>
        /// Gets all <see cref="Book"/> instances where the <see cref="Book.Title"/> contains the <paramref name="texts"/>
        /// </summary>
        /// <param name="texts"><see cref="Book.Title"/> subtexts</param>
        /// <returns><see cref="Book"/> instances, where the <see cref="IDictionary{TKey, TValue}.Keys"/> are the <paramref name="texts"/> and the <see cref="IDictionary{TKey, TValue}.Values"/> are the <see cref="Book"/> instances</returns>
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(IEnumerable<string> texts);
        /// <summary>
        /// Gets all <see cref="Book"/> instances where the <see cref="Book.Title"/> contains the <paramref name="texts"/>
        /// </summary>
        /// <param name="texts"><see cref="Book.Title"/> subtexts</param>
        /// <returns><see cref="Book"/> instances, where the <see cref="IDictionary{TKey, TValue}.Keys"/> are the <paramref name="texts"/> and the <see cref="IDictionary{TKey, TValue}.Values"/> are the <see cref="Book"/> instances</returns>
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts);

        /// <summary>
        /// Gets a <see cref="Book"/> instance by the <paramref name="bookFilter"/>.
        /// </summary>
        /// <param name="bookFilter">Filter option for the <see cref="Book"/> instances</param>
        /// <returns><see cref="Book"/> instance by the <paramref name="bookFilter"/></returns>
        Book SelectBook(BookFilter bookFilter);
    }
}