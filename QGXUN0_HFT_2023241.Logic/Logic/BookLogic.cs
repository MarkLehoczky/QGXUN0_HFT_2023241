using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Attributes;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Logic
{
    /// <summary>
    /// Implements all CRUD and non-crud methods for the <see cref="Book"/> model
    /// </summary>
    public class BookLogic : IBookLogic
    {
        /// <summary>
        /// Repository for the <see cref="Book"/> database context
        /// </summary>
        private readonly IRepository<Book> bookRepository;
        /// <summary>
        /// Repository for the <see cref="Author"/> database context
        /// </summary>
        private readonly IRepository<Author> authorRepository;
        /// <summary>
        /// Repository for the <see cref="BookAuthorConnector"/> database context
        /// </summary>
        private readonly IRepository<BookAuthorConnector> connectorRepository;

        /// <summary>
        /// Counts the number of <see cref="Book"/> instances
        /// </summary>
        /// <value>number of <see cref="Book"/> instances</value>
        public int Count { get => ReadAll().Count(); }
        /// <summary>
        /// Determines whether there are <see cref="Book"/> instances in the database
        /// </summary>
        /// <value><see langword="true"/> if there are <see cref="Book"/> instances in the database, otherwise <see langword="false"/></value>
        public bool IsEmpty { get => Count == 0; }


        /// <summary>
        /// Constructor with the database repositories
        /// </summary>
        /// <param name="bookRepository"><see cref="Book"/> repository</param>
        /// <param name="authorRepository"><see cref="Author"/> repository</param>
        /// <param name="connectorRepository"><see cref="BookAuthorConnector"/> repository</param>
        public BookLogic(IRepository<Book> bookRepository, IRepository<Author> authorRepository, IRepository<BookAuthorConnector> connectorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.connectorRepository = connectorRepository;
        }


        /// <summary>
        /// Creates a <paramref name="book"/> where the authors are already added
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> may be changed if another <see cref="Book"/> instance has the same <see langword="key"/></remarks>
        /// <param name="book">new book</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> if the book and authors are valid, otherwise <see langword="null"/></returns>
        public int? Create(Book book)
        {
            if (!book.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(book))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == book)?.BookID;

            if (Read(book.BookID) != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            bookRepository.Create(book);
            return book.BookID;
        }
        /// <summary>
        /// Creates a <paramref name="book"/> with authors
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> may be changed if another <see cref="Book"/> instance has the same <see langword="key"/></remarks>
        /// <param name="book">new book</param>
        /// <param name="authors">authors in the book</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> if the book is valid, otherwise <see langword="null"/></returns>
        public int? Create(Book book, IEnumerable<Author> authors)
        {
            if (book == null || !book.IsValid(typeof(RequiredCollectionAttribute)))
                return null;

            var prevAuthors = book.Authors;
            book.Authors = null;
            if (!AddAuthorsToBook(book, authors))
            {
                book.Authors = prevAuthors;
                return null;
            }

            if (!book.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(book))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == book)?.BookID;

            if (Read(book.BookID) != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            bookRepository.Create(book);
            return book.BookID;
        }
        /// <summary>
        /// Creates a <paramref name="book"/> with authors
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> may be changed if another <see cref="Book"/> instance has the same <see langword="key"/></remarks>
        /// <param name="book">new book</param>
        /// <param name="authors">authors in the book</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> if the book is valid, otherwise <see langword="null"/></returns>
        public int? Create(Book book, params Author[] authors)
        {
            return Create(book, authors.AsEnumerable());
        }

        /// <summary>
        /// Reads a <see cref="Book"/> with the same <paramref name="bookID"/> value
        /// </summary>
        /// <param name="bookID"><see cref="Book.BookID"/> value of the book</param>
        /// <returns><see cref="Book"/> if the book exists, otherwise <see langword="null"/></returns>
        public Book Read(int bookID)
        {
            try { return bookRepository.Read(bookID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <summary>
        /// Updates a <paramref name="book"/> with the same <see cref="Book.BookID"/> value
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> value of the <paramref name="book"/> must be the same as the one intended to update</remarks>
        /// <param name="book">updated book</param>
        /// <returns><see langword="true"/> if the update was successful, otherwise <see langword="false"/></returns>
        public bool Update(Book book)
        {
            if (!book.IsValid() || Read(book.BookID) == null)
                return false;
            bookRepository.Update(book);

            return true;
        }

        /// <summary>
        /// Deletes a <see cref="Book"/> with the same <paramref name="book"/>
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Book book)
        {
            if (book == null) return false;
            try { bookRepository.Delete(book.BookID); return true; }
            catch { return false; }
        }

        /// <summary>
        /// Reads all <see cref="Book"/>
        /// </summary>
        /// <returns>all <see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadAll()
        {
            return bookRepository.ReadAll();
        }


        /// <summary>
        /// Adds authors to a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddAuthorsToBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null || Read(book.BookID) == null) return false;

            foreach (var item in authorRepository.ReadAll().Where(t => authors.Contains(t)).Intersect(authorRepository.ReadAll().Where(t => !book.Authors.Contains(t))))
                connectorRepository.Create(new BookAuthorConnector(
                    connectorRepository.ReadAll().Max(t => t.BookAuthorConnectorID) + 1,
                    book.BookID,
                    item.AuthorID
                ));

            return authors.All(t => book.Authors.Contains(t));
        }
        /// <summary>
        /// Adds authors to a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddAuthorsToBook(Book book, params Author[] authors)
        {
            return AddAuthorsToBook(book, authors.AsEnumerable());
        }

        /// <summary>
        /// Removes authors from a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">removable authors</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null || Read(book.BookID) == null) return false;

            foreach (var item in connectorRepository.ReadAll().Where(t => t.Book == book && authors.Contains(t.Author)))
                connectorRepository.Delete(item.BookAuthorConnectorID);

            return authors.All(t => authorRepository.ReadAll().Contains(t)) && !authors.Any(t => book.Authors.Contains(t));
        }
        /// <summary>
        /// Removes authors from a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">removable authors</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAuthorsFromBook(Book book, params Author[] authors)
        {
            return RemoveAuthorsFromBook(book, authors.AsEnumerable());
        }


        /// <summary>
        /// Selects a book based on the given <paramref name="bookFilter"/>
        /// </summary>
        /// <param name="bookFilter">book filter</param>
        /// <returns>selected book</returns>
        public Book SelectBook(BookFilter bookFilter)
        {
            switch (bookFilter)
            {
                case BookFilter.MostExpensive: return ReadAll().Where(t => t.Price != null).OrderByDescending(t => t.Price).FirstOrDefault();
                case BookFilter.HighestRated: return ReadAll().Where(t => t.Rating != null).OrderByDescending(t => t.Rating).FirstOrDefault();
                case BookFilter.LeastExpensive: return ReadAll().Where(t => t.Price != null).OrderBy(t => t.Price).FirstOrDefault();
                case BookFilter.LowestRated: return ReadAll().Where(t => t.Rating != null).OrderBy(t => t.Rating).FirstOrDefault();
                default: return null;
            }
        }

        /// <summary>
        /// Returns the <see cref="Book"/> instances which <see cref="Book.Title"/> contains the given <paramref name="text"/>
        /// </summary>
        /// <param name="text">text in the title</param>
        /// <returns>books with the given <paramref name="text"/> in the title</returns>
        public IEnumerable<Book> GetBooksWithTitle(string text)
        {
            if (text == null) return Enumerable.Empty<Book>();
            return ReadAll().Where(t => t.Title.ToLower().Contains(text.ToLower())).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Book"/> instances which <see cref="Book.Title"/> contains the given <paramref name="texts"/>
        /// </summary>
        /// <param name="texts">texts in the title</param>
        /// <returns>books and texts, where the <see langword="Key"/> is the text and the <see langword="Value"/> is the <see cref="Book"/> instances</returns>
        public IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(IEnumerable<string> texts)
        {
            IDictionary<string, IEnumerable<Book>> dict = new Dictionary<string, IEnumerable<Book>>();
            texts.Where(t => t != null).ToList().ForEach(t => dict.Add(t, GetBooksWithTitle(t)));
            return dict;
        }

        /// <summary>
        /// Returns the <see cref="Book"/> instances which <see cref="Book.Title"/> contains the given <paramref name="texts"/>
        /// </summary>
        /// <param name="texts">texts in the title</param>
        /// <returns>books and texts, where the <see langword="Key"/> is the text and the <see langword="Value"/> is the <see cref="Book"/> instances</returns>
        public IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts)
        {
            return GetBooksWithTitles(texts.AsEnumerable());
        }

        /// <summary>
        /// Returns all <see cref="Book"/> with the given <paramref name="year"/>
        /// </summary>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all book in the given <paramref name="year"/></returns>
        public IEnumerable<Book> GetBooksInYear(int year)
        {
            return ReadAll().Where(t => t.Year == year).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Book"/> between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/>
        /// </summary>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all book in the given interval</returns>
        public IEnumerable<Book> GetBooksBetweenYears(int minimumYear, int maximumYear)
        {
            return ReadAll().Where(t => t.Year >= minimumYear && t.Year <= maximumYear).ToList();
        }
    }
}