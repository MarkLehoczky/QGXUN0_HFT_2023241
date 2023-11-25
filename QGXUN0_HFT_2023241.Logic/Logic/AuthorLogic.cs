using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Logic
{
    public class AuthorLogic : IAuthorLogic
    {
        /// <summary>
        /// Repository for the <see cref="Author"/> database context
        /// </summary>
        private readonly IRepository<Author> authorRepository;

        /// <summary>
        /// Counts the number of <see cref="Author"/> instances
        /// </summary>
        /// <value>number of <see cref="Author"/> instances</value>
        public int Count { get => ReadAll().Count(); }
        /// <summary>
        /// Determines whether there are <see cref="Author"/> instances in the database
        /// </summary>
        /// <value><see langword="true"/> if there are <see cref="Author"/> instances in the database, otherwise <see langword="false"/></value>
        public bool IsEmpty { get => Count == 0; }


        /// <summary>
        /// Constructor with the database repositories
        /// </summary>
        /// <param name="authorRepository"><see cref="Author"/> repository</param>
        public AuthorLogic(IRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }


        /// <summary>
        /// Creates a <paramref name="author"/>
        /// </summary>
        /// <remarks>The <see cref="Author.AuthorID"/> may be changed if another <see cref="Author"/> instance has the same <see langword="key"/></remarks>
        /// <param name="author">new author</param>
        /// <returns><see cref="Author.AuthorID"/> of the <paramref name="author"/> if the author is valid, otherwise <see langword="null"/></returns>
        public int? Create(Author author)
        {
            // if the author attributes are not valid (through ValidationAttribute), then returns
            if (!author.IsValid())
                return null;

            // if the author already exists, then returns
            // else if the ID already exists, gives a new ID to the author
            var read = Read(author.AuthorID);
            if (read == author)
                return author.AuthorID;
            else if (read != null)
                author.AuthorID = ReadAll().Max(t => t.AuthorID) + 1;

            // creates the author, then returns the ID
            authorRepository.Create(author);
            return author.AuthorID;
        }

        /// <summary>
        /// Reads a <see cref="Author"/> with the same <paramref name="authorID"/> value
        /// </summary>
        /// <param name="authorID"><see cref="Author.AuthorID"/> value of the author</param>
        /// <returns><see cref="Author"/> if the author exists, otherwise <see langword="null"/></returns>
        public Author Read(int authorID)
        {
            try { return authorRepository.Read(authorID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <summary>
        /// Updates a <paramref name="author"/> with the same <see cref="Author.AuthorID"/> value
        /// </summary>
        /// <remarks>The <see cref="Author.AuthorID"/> value of the <paramref name="author"/> must be the same as the one intended to update</remarks>
        /// <param name="author">updated author</param>
        /// <returns><see langword="true"/> if the update was successful, otherwise <see langword="false"/></returns>
        public bool Update(Author author)
        {
            if (!author.IsValid() || Read(author.AuthorID) == null)
                return false;
            authorRepository.Update(author);

            return true;
        }

        /// <summary>
        /// Deletes a <see cref="Author"/> with the same <see cref="Author.AuthorID"/>
        /// </summary>
        /// <param name="authorID"><see cref="Author.AuthorID"/> of the <see cref="Author"/></param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(int authorID)
        {
            try { authorRepository.Delete(authorID); }
            catch (InvalidOperationException) { return false; }
            return Read(authorID) == null;
        }
        /// <summary>
        /// Deletes a <see cref="Author"/> with the same <paramref name="author"/>
        /// </summary>
        /// <param name="author"><see cref="Author"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Author author)
        {
            if (author == null) return false;
            return Delete(author.AuthorID);
        }

        /// <summary>
        /// Reads all <see cref="Author"/>
        /// </summary>
        /// <returns>all <see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadAll()
        {
            return authorRepository.ReadAll();
        }


        /// <summary>
        /// Reads a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> values of the authors</param>
        /// <returns><see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadRange(IEnumerable<int> authorIDs)
        {
            return ReadAll().Where(t => authorIDs.Any(u => u == t.AuthorID));
        }
        /// <summary>
        /// Reads a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authors"><see cref="Author"/> instances</param>
        /// <returns><see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadRange(IEnumerable<Author> authors)
        {
            return ReadRange(authors.Select(t => t.AuthorID));
        }
        /// <summary>
        /// Reads a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> values of the authors</param>
        /// <returns><see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadRange(params int[] authorIDs)
        {
            return ReadRange(authorIDs.AsEnumerable());
        }
        /// <summary>
        /// Reads a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authors"><see cref="Author"/> instances</param>
        /// <returns><see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadRange(params Author[] authors)
        {
            return ReadRange(authors.AsEnumerable());
        }
        /// <summary>
        /// Reads a range of <see cref="Author"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Author.AuthorID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Author.AuthorID"/></param>
        /// <returns><see cref="Author"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Author> ReadBetween(int minimumID, int maximumID)
        {
            return ReadAll().Where(t => t.AuthorID >= minimumID && t.AuthorID <= maximumID);
        }

        /// <summary>
        /// Updates a range of <paramref name="authors"/> with the same <see cref="Author.AuthorID"/> values
        /// </summary>
        /// <remarks>The <see cref="Author.AuthorID"/> values of the <paramref name="authors"/> must be the same as the ones intended to update</remarks>
        /// <param name="authors">updated authors</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(IEnumerable<Author> authors)
        {
            bool successful = true;

            foreach (var item in authors)
                if (!Update(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Updates a range of <paramref name="authors"/> with the same <see cref="Author.AuthorID"/> values
        /// </summary>
        /// <remarks>The <see cref="Author.AuthorID"/> values of the <paramref name="authors"/> must be the same as the ones intended to update</remarks>
        /// <param name="authors">updated authors</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(params Author[] authors)
        {
            return UpdateRange(authors.AsEnumerable());
        }

        /// <summary>
        /// Deletes a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> values of the <see cref="Author"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<int> authorIDs)
        {
            bool successful = true;

            foreach (var item in authorIDs)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authors"><see cref="Author"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<Author> authors)
        {
            bool successful = true;

            foreach (var item in authors)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authorIDs"><see cref="Author.AuthorID"/> values of the <see cref="Author"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params int[] authorIDs)
        {
            return DeleteRange(authorIDs.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Author"/> instances
        /// </summary>
        /// <param name="authors"><see cref="Author"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params Author[] authors)
        {
            return DeleteRange(authors.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Author"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Author.AuthorID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Author.AuthorID"/></param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteBetween(int minimumID, int maximumID)
        {
            return DeleteRange(ReadBetween(minimumID, maximumID));
        }
        /// <summary>
        /// Deletes every <see cref="Author"/> instances
        /// </summary>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteAll()
        {
            return DeleteRange(ReadAll());
        }


        /// <summary>
        /// Determines whether the <see cref="Author"/> instances contains the <paramref name="author"/>
        /// </summary>
        /// <param name="author">searched author</param>
        /// <returns><see langword="true"/> if the <paramref name="author"/> was found, otherwise <see langword="false"/></returns>
        public bool Contains(Author author)
        {
            return ReadAll().Contains(author);
        }
        /// <summary>
        /// Determines whether the <see cref="Author"/> instances contains any of the <paramref name="authors"/>
        /// </summary>
        /// <param name="authors">searched authors</param>
        /// <returns><see langword="true"/> if any of the <paramref name="authors"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(IEnumerable<Author> authors)
        {
            return authors.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Author"/> instances contains any of the <paramref name="authors"/>
        /// </summary>
        /// <param name="authors">searched authors</param>
        /// <returns><see langword="true"/> if any of the <paramref name="authors"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(params Author[] authors)
        {
            return authors.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Author"/> instances contains all the <paramref name="authors"/>
        /// </summary>
        /// <param name="authors">searched authors</param>
        /// <returns><see langword="true"/> if all the <paramref name="authors"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(IEnumerable<Author> authors)
        {
            return authors.All(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Author"/> instances contains all the <paramref name="author"/>
        /// </summary>
        /// <param name="author">searched authors</param>
        /// <returns><see langword="true"/> if all the <paramref name="author"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(params Author[] author)
        {
            return author.All(t => Contains(t));
        }


        /// <summary>
        /// Returns the >most expensive book from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>most expensive book from the <paramref name="author"/></returns>
        public Book GetMostExpensiveBookFromAuthor(Author author)
        {
            if (author == null || author.Books == null) return null;
            return author.Books.OrderByDescending(t => t.Price).FirstOrDefault();
        }

        /// <summary>
        /// Returns the least expensive book from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>least expensive book from the <paramref name="author"/></returns>
        public Book GetLeastExpensiveBookFromAuthor(Author author)
        {
            if (author == null || author.Books == null) return null;
            return author.Books.OrderBy(t => t.Price).FirstOrDefault();
        }

        /// <summary>
        /// Returns the highest rated book from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>highest rated book from the <paramref name="author"/></returns>
        public Book GetHighestRatedBookFromAuthor(Author author)
        {
            if (author == null || author.Books == null) return null;
            return author.Books.OrderByDescending(t => t.Rating).FirstOrDefault();
        }

        /// <summary>
        /// Returns the lowest rated book from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>lowest rated book from the <paramref name="author"/></returns>
        public Book GetLowestRatedBookFromAuthor(Author author)
        {
            if (author == null || author.Books == null) return null;
            return author.Books.OrderBy(t => t.Rating).FirstOrDefault();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> which has the highest average <see cref="Author.Books"/> rating
        /// </summary>
        /// <returns>highest rated author, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Author"/></returns>
        public KeyValuePair<double, Author> GetHighestRatedAuthor()
        {
            return ReadAll().Where(t => t.Books != null).OrderByDescending(t => t.Books.Average(u => u.Rating))
                .Select(v => new KeyValuePair<double, Author>((double)v.Books.Average(u => u.Rating), v)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> which has the lowest average <see cref="Author.Books"/> rating
        /// </summary>
        /// <returns>lowest rated author, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Author"/></returns>
        public KeyValuePair<double, Author> GetLowestRatedAuthor()
        {
            return ReadAll().Where(t => t.Books != null).OrderBy(t => t.Books.Average(u => u.Rating))
                .Select(v => new KeyValuePair<double, Author>((double)v.Books.Average(u => u.Rating), v)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the series of an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>series of an <paramref name="author"/></returns>
        public IEnumerable<Collection> GetSeriesOfAuthor(Author author)
        {
            if (author == null || author.Books == null) return Enumerable.Empty<Collection>();
            return author.Books.SelectMany(t => t.Collections, (t, collections) => collections).Where(t => t.IsSeries == true).Distinct();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances grouped by their number of <see cref="Author.Books"/>
        /// </summary>
        /// <returns>grouped authors</returns>
        public IEnumerable<IGrouping<int, Author>> GroupByNumberOfBooks()
        {
            return ReadAll().OrderBy(t => t.Books.Count).ToList().GroupBy(u => u.Books.Count);
        }
    }
}
