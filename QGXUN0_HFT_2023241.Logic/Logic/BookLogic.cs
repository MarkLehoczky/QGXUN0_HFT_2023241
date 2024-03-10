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
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Book"/> <see langword="class"/>
    /// </summary>
    public class BookLogic : IBookLogic
    {
        /// <inheritdoc/>
        public int Count { get => ReadAll().Count(); }

        /// <inheritdoc/>
        public bool IsEmpty { get => Count == 0; }

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Book}"/>
        /// </summary>
        private readonly IRepository<Book> _bookRepository;

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Author}"/>
        /// </summary>
        private readonly IRepository<Author> _authorRepository;

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{BookAuthorConnector}"/>
        /// </summary>
        private readonly IRepository<BookAuthorConnector> _connectorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> <see langword="class"/> by the <see cref="Repository{Book}"/>, <see cref="Repository{Author}"/> and <see cref="Repository{BookAuthorConnector}"/> instances.
        /// </summary>
        /// <param name="bookRepository"><see cref="Book"/> repository instance</param>
        /// <param name="authorRepository"><see cref="Author"/> repository instance</param>
        /// <param name="connectorRepository"><see cref="BookAuthorConnector"/> repository instance</param>
        public BookLogic(IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<BookAuthorConnector> connectorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _connectorRepository = connectorRepository;
        }


        /// <summary>
        /// Creates an <see cref="Book"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> of the <see cref="Book"/> instance may be changed</remarks>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Book book)
        {
            if (!book.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(book))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == book)?.BookID;

            if (Read(book.BookID) != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            _bookRepository.Create(book);
            return book.BookID;
        }
        /// <summary>
        /// Creates an <see cref="Book"/> instance, then adds <see cref="Author"/> instances to the <see cref="Book"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> of the <see cref="Book"/> instance may be changed</remarks>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Book book, IEnumerable<Author> authors)
        {
            var created = Create(book);
            if (created != null)
                AddAuthorsToBook(book, authors);

            return created;
        }
        /// <summary>
        /// Creates an <see cref="Book"/> instance, then adds <see cref="Author"/> instances to the <see cref="Book"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> of the <see cref="Book"/> instance may be changed</remarks>
        /// <param name="book">New <see cref="Book"/> instance</param>
        /// <param name="authors"><see cref="Author"/> instances for the <see cref="Book"/> instance</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Book book, params Author[] authors)
        {
            return Create(book, authors.AsEnumerable());
        }

        /// <summary>
        /// Reads an <see cref="Book"/> instance.
        /// </summary>
        /// <param name="bookID"><see cref="Book.BookID"/> of the read <see cref="Book"/> instance</param>
        /// <returns><see cref="Book"/> instance if the instance is found; otherwise, <see langword="null"/></returns>
        public Book Read(int bookID)
        {
            try { return _bookRepository.Read(bookID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <inheritdoc/>
        public bool Update(Book book)
        {
            if (!book.IsValid() || Read(book.BookID) == null)
                return false;
            _bookRepository.Update(book);

            return true;
        }

        /// <inheritdoc/>
        public bool Delete(Book book)
        {
            if (book == null) return false;
            try { _bookRepository.Delete(book.BookID); return true; }
            catch { return false; }
        }

        /// <inheritdoc/>
        public IQueryable<Book> ReadAll()
        {
            return _bookRepository.ReadAll();
        }


        /// <inheritdoc/>
        public bool AddAuthorsToBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null || Read(book.BookID) == null) return false;

            foreach (var item in _authorRepository.ReadAll().Where(t => authors.Contains(t)).Except(book.Authors))
                _connectorRepository.Create(new BookAuthorConnector(
                    _connectorRepository.ReadAll().Max(t => t.BookAuthorConnectorID) + 1,
                    book.BookID,
                    item.AuthorID
                ));

            return authors.All(t => book.Authors.Contains(t));
        }
        /// <inheritdoc/>
        public bool AddAuthorsToBook(Book book, params Author[] authors)
        {
            return AddAuthorsToBook(book, authors.AsEnumerable());
        }

        /// <inheritdoc/>
        public bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null || Read(book.BookID) == null) return false;

            foreach (var item in _connectorRepository.ReadAll().Where(t => t.Book == book && authors.Contains(t.Author)))
                _connectorRepository.Delete(item.BookAuthorConnectorID);

            return authors.All(t => _authorRepository.ReadAll().Contains(t)) && !authors.Any(t => book.Authors.Contains(t));
        }
        /// <inheritdoc/>
        public bool RemoveAuthorsFromBook(Book book, params Author[] authors)
        {
            return RemoveAuthorsFromBook(book, authors.AsEnumerable());
        }


        /// <inheritdoc/>
        public IEnumerable<Book> GetBooksBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.Year >= minimumYear && t.Year <= maximumYear).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Book> GetBooksInYear(int year)
        {
            return ReadAll().Where(t => t.Year == year).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Book> GetBooksWithTitle(string text)
        {
            return ReadAll().Where(t => t.Title.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(IEnumerable<string> texts)
        {
            return new Dictionary<string, IEnumerable<Book>>(texts.Select(t => new KeyValuePair<string, IEnumerable<Book>>(t, GetBooksWithTitle(t))));
        }

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts)
        {
            return GetBooksWithTitles(texts.AsEnumerable());
        }

        /// <inheritdoc/>
        public Book SelectBook(BookFilter bookFilter)
        {
            return bookFilter switch
            {
                BookFilter.MostExpensive => ReadAll().Where(t => t.Price != null).OrderByDescending(t => t.Price).FirstOrDefault(),
                BookFilter.HighestRated => ReadAll().Where(t => t.Rating != null).OrderByDescending(t => t.Rating).FirstOrDefault(),
                BookFilter.LeastExpensive => ReadAll().Where(t => t.Price != null).OrderBy(t => t.Price).FirstOrDefault(),
                BookFilter.LowestRated => ReadAll().Where(t => t.Rating != null).OrderBy(t => t.Rating).FirstOrDefault(),
                _ => null,
            };
        }
    }
}