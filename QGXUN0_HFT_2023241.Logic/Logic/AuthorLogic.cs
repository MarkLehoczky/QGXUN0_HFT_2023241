using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!author.IsValid(typeof(RequiredAttribute)))
                return null;

            if (ReadAll().Contains(author))
                return ReadAll().FirstOrDefault(t => t == author)?.AuthorID;

            if (Read(author.AuthorID) != null)
                author.AuthorID = ReadAll().Max(t => t.AuthorID) + 1;

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
        /// Deletes a <see cref="Author"/> with the same <paramref name="author"/>
        /// </summary>
        /// <param name="author"><see cref="Author"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Author author)
        {
            if (author == null) return false;
            try { authorRepository.Delete(author.AuthorID); return true; }
            catch { return false; }
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
        /// Selects a book based on the given <paramref name="bookFilter"/> from an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <param name="bookFilter">book filter</param>
        /// <returns>selected book</returns>
        public Book SelectBookFromAuthor(Author author, BookFilter bookFilter)
        {
            if (author == null || author.Books == null) return null;

            switch (bookFilter)
            {
                case BookFilter.MostExpensive: return author.Books.OrderByDescending(t => t.Price).FirstOrDefault();
                case BookFilter.HighestRated: return author.Books.OrderByDescending(t => t.Rating).FirstOrDefault();
                case BookFilter.LeastExpensive: return author.Books.OrderBy(t => t.Price).FirstOrDefault();
                case BookFilter.LowestRated: return author.Books.OrderBy(t => t.Rating).FirstOrDefault();
                default: return null;
            }
        }

        /// <summary>
        /// Returns the <see cref="Author"/> which has the highest average <see cref="Author.Books"/> rating
        /// </summary>
        /// <returns>highest rated author, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Author"/></returns>
        public KeyValuePair<double, Author> GetHighestRatedAuthor()
        {
            return ReadAll()
                .Where(t => t.Books != null && t.Books.Any(t => t.Rating != null))
                .OrderByDescending(t => t.Books.Average(u => u.Rating))
                .Select(v => new KeyValuePair<double, Author>((double)v.Books.Average(u => u.Rating), v))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> which has the lowest average <see cref="Author.Books"/> rating
        /// </summary>
        /// <returns>lowest rated author, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Author"/></returns>
        public KeyValuePair<double, Author> GetLowestRatedAuthor()
        {
            return ReadAll()
                .Where(t => t.Books != null && t.Books.Any(t => t.Rating != null))
                .OrderBy(t => t.Books.Average(u => u.Rating))
                .Select(v => new KeyValuePair<double, Author>((double)v.Books.Average(u => u.Rating), v))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the series of an <paramref name="author"/>
        /// </summary>
        /// <param name="author">author</param>
        /// <returns>series of an <paramref name="author"/></returns>
        public IEnumerable<Collection> GetSeriesOfAuthor(Author author)
        {
            if (author == null || author.Books == null) return Enumerable.Empty<Collection>();
            return author.Books
                .Where(t => t.Collections != null)
                .SelectMany(t => t.Collections, (t, collections) => collections)
                .Where(t => t.IsSeries.HasValue == true && t.IsSeries == true)
                .Distinct();
        }
    }
}
