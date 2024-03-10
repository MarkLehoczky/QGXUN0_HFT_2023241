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
    /// Specifies the <see langword="CRUD"/> and <see langword="Non-CRUD"/> methods of the <see cref="Author"/> <see langword="class"/>
    /// </summary>
    public class AuthorLogic : IAuthorLogic
    {
        /// <inheritdoc/>
        public int Count { get => ReadAll().Count(); }

        /// <inheritdoc/>
        public bool IsEmpty { get => Count == 0; }

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Author}"/>
        /// </summary>
        private readonly IRepository<Author> _authorRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="Author"/> <see langword="class"/> by the <see cref="Repository{Author}"/> instance.
        /// </summary>
        /// <param name="authorRepository"><see cref="Author"/> repository instance</param>
        public AuthorLogic(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }


        /// <summary>
        /// Creates an <see cref="Author"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Author.AuthorID"/> of the <see cref="Author"/> instance may be changed</remarks>
        /// <param name="author">New <see cref="Author"/> instance</param>
        /// <returns><see cref="Author.AuthorID"/> of the <paramref name="author"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Author author)
        {
            if (!author.IsValid())
                return null;

            if (ReadAll().Contains(author))
                return ReadAll().FirstOrDefault(t => t == author)?.AuthorID;

            if (Read(author.AuthorID) != null)
                author.AuthorID = ReadAll().Max(t => t.AuthorID) + 1;

            _authorRepository.Create(author);
            return author.AuthorID;
        }

        /// <summary>
        /// Reads an <see cref="Author"/> instance.
        /// </summary>
        /// <param name="authorID"><see cref="Author.AuthorID"/> of the read <see cref="Author"/> instance</param>
        /// <returns><see cref="Author"/> instance if the instance is found; otherwise, <see langword="null"/></returns>
        public Author Read(int authorID)
        {
            try { return _authorRepository.Read(authorID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <inheritdoc/>
        public bool Update(Author author)
        {
            if (!author.IsValid() || Read(author.AuthorID) == null)
                return false;
            _authorRepository.Update(author);

            return true;
        }

        /// <inheritdoc/>
        public bool Delete(Author author)
        {
            if (author == null) return false;
            try { _authorRepository.Delete(author.AuthorID); return true; }
            catch { return false; }
        }

        /// <inheritdoc/>
        public IQueryable<Author> ReadAll()
        {
            return _authorRepository.ReadAll();
        }


        /// <inheritdoc/>
        public KeyValuePair<double?, Author> GetHighestRatedAuthor()
        {
            var temp = ReadAll()
                .Where(t => t.Books.Any(t => t.Rating != null))
                .OrderByDescending(t => t.Books.Average(u => u.Rating))
                .FirstOrDefault();
            return new KeyValuePair<double?, Author>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <inheritdoc/>
        public KeyValuePair<double?, Author> GetLowestRatedAuthor()
        {
            var temp = ReadAll()
                .Where(t => t.Books.Any(t => t.Rating != null))
                .OrderBy(t => t.Books.Average(u => u.Rating))
                .FirstOrDefault();
            return new KeyValuePair<double?, Author>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <inheritdoc/>
        public IEnumerable<Collection> GetSeriesOfAuthor(Author author)
        {
            if (author == null) return Enumerable.Empty<Collection>();
            return author.Books
                .SelectMany(t => t.Collections, (t, collections) => collections)
                .Where(t => t.IsSeries.HasValue == true && t.IsSeries == true)
                .Distinct()
                .ToList();
        }

        /// <inheritdoc/>
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
    }
}
