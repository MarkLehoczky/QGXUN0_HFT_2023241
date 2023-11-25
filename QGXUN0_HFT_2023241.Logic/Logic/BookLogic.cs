using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections;
using System.Collections.Generic;
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
            // if the book attributes are not valid (through ValidationAttribute), then returns
            if (!book.IsValid())
                return null;

            // if the book already exists, then returns
            // else if the ID already exists, gives a new ID to the book
            var read = Read(book.BookID);
            if (read == book)
                return book.BookID;
            else if (read != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            // creates the book, then returns the ID
            bookRepository.Create(book);
            return book.BookID;
        }
        /// <summary>
        /// Creates a <paramref name="book"/> with authors
        /// </summary>
        /// <remarks><para>The <see cref="Book.BookID"/> may be changed if another <see cref="Book"/> instance has the same <see langword="key"/></para><para>The authors must be in the database</para></remarks>
        /// <param name="book">new book</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the authors of the book</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> if the book and authors are valid, otherwise <see langword="null"/></returns>
        public int? Create(Book book, IEnumerable<int> authorIDs)
        {
            // if the book does not exist, then returns
            if (book == null)
                return null;

            // if not all author addition was successful, resets the authors
            var prevAuthors = book.Authors?.ToList();
            if (!AddNewAuthorsToBook(book, authorIDs))
                book.Authors = prevAuthors;

            // if the book attributes are not valid (through ValidationAttribute), then returns
            if (!book.IsValid())
                return null;

            // if the book already exists, then returns
            // else if the ID already exists, gives a new ID to the book
            var read = Read(book.BookID);
            if (read == book)
                return book.BookID;
            else if (read != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            // creates the book, then returns the ID
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
            // if the book does not exist, then returns
            if (book == null)
                return null;

            // if not all author addition was successful, resets the authors
            var prevAuthors = book.Authors?.ToList();
            if (!AddNewAuthorsToBook(book, authors))
                book.Authors = prevAuthors;

            // if the book attributes are not valid (through ValidationAttribute), then returns
            if (!book.IsValid())
                return null;

            // if the book already exists, then returns
            // else if the ID already exists, gives a new ID to the book
            var read = Read(book.BookID);
            if (read == book)
                return book.BookID;
            else if (read != null)
                book.BookID = ReadAll().Max(t => t.BookID) + 1;

            // creates the book, then returns the ID
            bookRepository.Create(book);
            return book.BookID;
        }
        /// <summary>
        /// Creates a <paramref name="book"/> with authors
        /// </summary>
        /// <remarks><para>The <see cref="Book.BookID"/> may be changed if another <see cref="Book"/> instance has the same <see langword="key"/></para><para>The authors must be in the database</para></remarks>
        /// <param name="book">new book</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the authors in the book</param>
        /// <returns><see cref="Book.BookID"/> of the <paramref name="book"/> if the book is valid, otherwise <see langword="null"/></returns>
        public int? Create(Book book, params int[] authorIDs)
        {
            return Create(book, authorIDs.AsEnumerable());
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
        /// Deletes a <see cref="Book"/> with the same <see cref="Book.BookID"/>
        /// </summary>
        /// <param name="bookID"><see cref="Book.BookID"/> of the <see cref="Book"/></param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(int bookID)
        {
            try { bookRepository.Delete(bookID); }
            catch (InvalidOperationException) { return false; }
            return Read(bookID) == null;
        }
        /// <summary>
        /// Deletes a <see cref="Book"/> with the same <paramref name="book"/>
        /// </summary>
        /// <param name="book"><see cref="Book"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Book book)
        {
            if (book == null) return false;
            return Delete(book.BookID);
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
        /// Reads a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="bookIDs"><see cref="Book.BookID"/> values of the books</param>
        /// <returns><see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadRange(IEnumerable<int> bookIDs)
        {
            return ReadAll().Where(t => bookIDs.Any(u => u == t.BookID));
        }
        /// <summary>
        /// Reads a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="books"><see cref="Book"/> instances</param>
        /// <returns><see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadRange(IEnumerable<Book> books)
        {
            return ReadAll().Where(t => books.Any(u => u == t));
        }
        /// <summary>
        /// Reads a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="bookIDs"><see cref="Book.BookID"/> values of the books</param>
        /// <returns><see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadRange(params int[] bookIDs)
        {
            return ReadAll().Where(t => bookIDs.Any(u => u == t.BookID));
        }
        /// <summary>
        /// Reads a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="books"><see cref="Book"/> instances</param>
        /// <returns><see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadRange(params Book[] books)
        {
            return ReadAll().Where(t => books.Any(u => u == t));
        }
        /// <summary>
        /// Reads a range of <see cref="Book"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Book.BookID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Book.BookID"/></param>
        /// <returns><see cref="Book"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Book> ReadBetween(int minimumID, int maximumID)
        {
            return ReadAll().Where(t => t.BookID >= minimumID && t.BookID <= maximumID);
        }

        /// <summary>
        /// Updates a range of <paramref name="books"/> with the same <see cref="Book.BookID"/> values
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> values of the <paramref name="books"/> must be the same as the ones intended to update</remarks>
        /// <param name="books">updated books</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(IEnumerable<Book> books)
        {
            bool successful = true;

            foreach (var item in books)
                if (!Update(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Updates a range of <paramref name="books"/> with the same <see cref="Book.BookID"/> values
        /// </summary>
        /// <remarks>The <see cref="Book.BookID"/> values of the <paramref name="books"/> must be the same as the ones intended to update</remarks>
        /// <param name="books">updated books</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(params Book[] books)
        {
            return UpdateRange(books.AsEnumerable());
        }

        /// <summary>
        /// Deletes a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="bookIDs"><see cref="Book.BookID"/> values of the <see cref="Book"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<int> bookIDs)
        {
            bool successful = true;

            foreach (var item in bookIDs)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="books"><see cref="Book"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<Book> books)
        {
            bool successful = true;

            foreach (var item in books)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="bookIDs"><see cref="Book.BookID"/> values of the <see cref="Book"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params int[] bookIDs)
        {
            return DeleteRange(bookIDs.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Book"/> instances
        /// </summary>
        /// <param name="books"><see cref="Book"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params Book[] books)
        {
            return DeleteRange(books.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Book"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Book.BookID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Book.BookID"/></param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteBetween(int minimumID, int maximumID)
        {
            return DeleteRange(ReadBetween(minimumID, maximumID));
        }
        /// <summary>
        /// Deletes every <see cref="Book"/> instances
        /// </summary>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteAll()
        {
            return DeleteRange(ReadAll());
        }


        /// <summary>
        /// Determines whether the <see cref="Book"/> instances contains the <paramref name="book"/>
        /// </summary>
        /// <param name="book">searched book</param>
        /// <returns><see langword="true"/> if the <paramref name="book"/> was found, otherwise <see langword="false"/></returns>
        public bool Contains(Book book)
        {
            return ReadAll().Contains(book);
        }
        /// <summary>
        /// Determines whether the <see cref="Book"/> instances contains any of the <paramref name="books"/>
        /// </summary>
        /// <param name="books">searched books</param>
        /// <returns><see langword="true"/> if any of the <paramref name="books"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(IEnumerable<Book> books)
        {
            return books.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Book"/> instances contains any of the <paramref name="books"/>
        /// </summary>
        /// <param name="books">searched books</param>
        /// <returns><see langword="true"/> if any of the <paramref name="books"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(params Book[] books)
        {
            return books.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Book"/> instances contains all the <paramref name="books"/>
        /// </summary>
        /// <param name="books">searched books</param>
        /// <returns><see langword="true"/> if all the <paramref name="books"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(IEnumerable<Book> books)
        {
            return books.All(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Book"/> instances contains all the <paramref name="book"/>
        /// </summary>
        /// <param name="book">searched books</param>
        /// <returns><see langword="true"/> if all the <paramref name="book"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(params Book[] book)
        {
            return book.All(t => Contains(t));
        }


        /// <summary>
        /// Adds authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks>The authors must be in the database</remarks>
        /// <param name="book">book for the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddAuthorsToBook(Book book, IEnumerable<int> authorIDs)
        {
            if (book == null || authorIDs.Count() == 0)
                return false;

            book.Authors ??= new List<Author>();
            int count = book.Authors.Count;

            foreach (var item in authorRepository.ReadAll().Where(t => authorIDs.Contains(t.AuthorID)))
                book.Authors.Add(item);

            connectorRepository.SaveChanges();
            return authorIDs.All(t => book.Authors.Any(u => u.AuthorID == t));
        }
        /// <summary>
        /// Adds authors to a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddAuthorsToBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null || authors.Count() == 0 || authors.Any(t => t == null))
                return false;

            book.Authors ??= new List<Author>();
            int count = book.Authors.Count;

            foreach (var item in authors)
                book.Authors.Add(item);

            connectorRepository.SaveChanges();
            return authors.All(t => book.Authors.Contains(t));
        }
        /// <summary>
        /// Adds authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks>The authors must be in the database</remarks>
        /// <param name="book">book of the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddAuthorsToBook(Book book, params int[] authorIDs)
        {
            return AddAuthorsToBook(book, authorIDs.AsEnumerable());
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
        /// Adds new authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks><para>The authors must be in the database</para><para>Removes all previous authors</para></remarks>
        /// <param name="book">book for the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddNewAuthorsToBook(Book book, IEnumerable<int> authorIDs)
        {
            if (book != null) book.Authors = null;
            return AddAuthorsToBook(book, authorIDs);
        }
        /// <summary>
        /// Adds new authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks>Removes all previous authors</remarks>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddNewAuthorsToBook(Book book, IEnumerable<Author> authors)
        {
            if (book != null) book.Authors = null;
            return AddAuthorsToBook(book, authors);
        }
        /// <summary>
        /// Adds new authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks><para>The authors must be in the database</para><para>Removes all previous authors</para></remarks>
        /// <param name="book">book of the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddNewAuthorsToBook(Book book, params int[] authorIDs)
        {
            return AddNewAuthorsToBook(book, authorIDs.AsEnumerable());
        }
        /// <summary>
        /// Adds new authors to a <paramref name="book"/>
        /// </summary>
        /// <remarks>Removes all previous authors</remarks>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">addable authors</param>
        /// <returns><see langword="true"/> if all the addition was successful, otherwise <see langword="false"/></returns>
        public bool AddNewAuthorsToBook(Book book, params Author[] authors)
        {
            return AddNewAuthorsToBook(book, authors.AsEnumerable());
        }

        /// <summary>
        /// Removes authors from a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the removable authors</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAuthorsFromBook(Book book, IEnumerable<int> authorIDs)
        {
            if (book == null)
                return false;

            int count = book.Authors.Count;

            foreach (var item in authorRepository.ReadAll().Where(t => authorIDs.Contains(t.AuthorID)))
                book.Authors.Remove(item);

            connectorRepository.SaveChanges();
            return !book.Authors.Any(t => authorIDs.Contains(t.AuthorID));
        }
        /// <summary>
        /// Removes authors from a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authors">removable authors</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors)
        {
            if (book == null)
                return false;

            foreach (var item in authors)
                book.Authors.Remove(item);

            connectorRepository.SaveChanges();
            return !book.Authors.Any(t => authors.Contains(t));
        }
        /// <summary>
        /// Removes authors from a <paramref name="book"/>
        /// </summary>
        /// <param name="book">book of the authors</param>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> of the removable authors</param>
        /// <returns><see langword="true"/> if all the removal was successful, otherwise <see langword="false"/></returns>
        public bool RemoveAuthorsFromBook(Book book, params int[] authorIDs)
        {
            return RemoveAuthorsFromBook(book, authorIDs.AsEnumerable());
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
        /// Returns the most expensive book
        /// </summary>
        /// <returns>most expensive book</returns>
        public Book GetMostExpensiveBook()
        {
            return ReadAll().OrderByDescending(t => t.Price).FirstOrDefault();
        }

        /// <summary>
        /// Returns the least expensive book
        /// </summary>
        /// <returns>least expensive book</returns>
        public Book GetLeastExpensiveBook()
        {
            return ReadAll().OrderBy(t => t.Price).FirstOrDefault();
        }

        /// <summary>
        /// Returns the highest rated book
        /// </summary>
        /// <returns>highest rated book</returns>
        public Book GetHighestRatedBook()
        {
            return ReadAll().OrderByDescending(t => t.Rating).FirstOrDefault();
        }

        /// <summary>
        /// Returns the lowest rated book
        /// </summary>
        /// <returns>lowest rated book</returns>
        public Book GetLowestRatedBook()
        {
            return ReadAll().OrderBy(t => t.Rating).FirstOrDefault();
        }

        /// <summary>
        /// Returns the <see cref="Book"/> instances which <see cref="Book.Title"/> contains the given <paramref name="text"/>
        /// </summary>
        /// <param name="text">text in the title</param>
        /// <returns>books with the given <paramref name="text"/> in the title</returns>
        public IEnumerable<Book> GetBooksWithTitle(string text)
        {
            if (text == null) return Enumerable.Empty<Book>();
            return ReadAll().Where(t => t.Title.ToLower().Contains(text)).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Book"/> instances which <see cref="Book.Title"/> contains the given <paramref name="texts"/>
        /// </summary>
        /// <param name="texts">texts in the title</param>
        /// <returns>books and texts as an <c><see cref="IDictionary"/></c></returns>
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
        /// <returns>books and texts as an <c><see cref="IDictionary"/></c></returns>
        public IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts)
        {
            return GetBooksWithTitles(texts.AsEnumerable());
        }

        /// <summary>
        /// Returns all <see cref="Book"/> from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>all book from the <paramref name="author"/></returns>
        public IEnumerable<Book> GetBooksFromAuthor(Author author)
        {
            return ReadAll().Where(t => t.Authors.Any(u => u == author)).ToList();
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

        /// <summary>
        /// Returns the <see cref="Book"/> instances grouped by their number of <see cref="Book.Authors"/>
        /// </summary>
        /// <returns>grouped books</returns>
        public IEnumerable<IGrouping<int, Book>> GroupByNumberOfAuthors()
        {
            return ReadAll().Where(t => t.Authors.Count != 0).OrderBy(t => t.Authors.Count).ToList().GroupBy(u => u.Authors.Count);
        }
    }
}