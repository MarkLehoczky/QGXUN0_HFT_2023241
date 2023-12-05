using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models.Attributes;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Logic
{
    /// <summary>
    /// Implements all CRUD and non-crud methods for the <see cref="Publisher"/> model
    /// </summary>
    public class PublisherLogic : IPublisherLogic
    {
        /// <summary>
        /// Repository for the <see cref="Publisher"/> database context
        /// </summary>
        private readonly IRepository<Publisher> publisherRepository;

        /// <summary>
        /// Counts the number of <see cref="Publisher"/> instances
        /// </summary>
        /// <value>number of <see cref="Publisher"/> instances</value>
        public int Count { get => ReadAll().Count(); }
        /// <summary>
        /// Determines whether there are <see cref="Publisher"/> instances in the database
        /// </summary>
        /// <value><see langword="true"/> if there are <see cref="Publisher"/> instances in the database, otherwise <see langword="false"/></value>
        public bool IsEmpty { get => Count == 0; }


        /// <summary>
        /// Constructor with the database repositories
        /// </summary>
        /// <param name="publisherRepository"><see cref="Publisher"/> repository</param>
        public PublisherLogic(IRepository<Publisher> publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }


        /// <summary>
        /// Creates a <paramref name="publisher"/>
        /// </summary>
        /// <remarks>The <see cref="Publisher.PublisherID"/> may be changed if another <see cref="Publisher"/> instance has the same <see langword="key"/></remarks>
        /// <param name="publisher">new publisher</param>
        /// <returns><see cref="Publisher.PublisherID"/> of the <paramref name="publisher"/> if the publisher is valid, otherwise <see langword="null"/></returns>
        public int? Create(Publisher publisher)
        {
            if (!publisher.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(publisher))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == publisher)?.PublisherID;

            if (Read(publisher.PublisherID) != null)
                publisher.PublisherID = ReadAll().Max(t => t.PublisherID) + 1;

            publisherRepository.Create(publisher);
            return publisher.PublisherID;
        }

        /// <summary>
        /// Reads a <see cref="Publisher"/> with the same <paramref name="publisherID"/> value
        /// </summary>
        /// <param name="publisherID"><see cref="Publisher.PublisherID"/> value of the publisher</param>
        /// <returns><see cref="Publisher"/> if the publisher exists, otherwise <see langword="null"/></returns>
        public Publisher Read(int publisherID)
        {
            try { return publisherRepository.Read(publisherID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <summary>
        /// Updates a <paramref name="publisher"/> with the same <see cref="Publisher.PublisherID"/> value
        /// </summary>
        /// <remarks>The <see cref="Publisher.PublisherID"/> value of the <paramref name="publisher"/> must be the same as the one intended to update</remarks>
        /// <param name="publisher">updated publisher</param>
        /// <returns><see langword="true"/> if the update was successful, otherwise <see langword="false"/></returns>
        public bool Update(Publisher publisher)
        {
            if (!publisher.IsValid() || Read(publisher.PublisherID) == null)
                return false;
            publisherRepository.Update(publisher);

            return true;
        }

        /// <summary>
        /// Deletes a <see cref="Publisher"/> with the same <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Publisher publisher)
        {
            if (publisher == null) return false;
            try { publisherRepository.Delete(publisher.PublisherID); return true; }
            catch { return false; }
        }

        /// <summary>
        /// Reads all <see cref="Publisher"/>
        /// </summary>
        /// <returns>all <see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadAll()
        {
            return publisherRepository.ReadAll();
        }


        /// <summary>
        /// Returns all <see cref="Publisher"/> who released at least one series
        /// </summary>
        /// <returns>series publishers</returns>
        public IEnumerable<Publisher> GetSeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.Any(u => u.Collections.Any(v => v.IsSeries.HasValue == true && v.IsSeries == true))).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Publisher"/> who only released series
        /// </summary>
        /// <returns>only series publishers</returns>
        public IEnumerable<Publisher> GetOnlySeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.All(u => u.Collections.Any(v => v.IsSeries.HasValue == true && v.IsSeries == true))).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Publisher"/> which has the highest average <see cref="Publisher.Books"/> rating
        /// </summary>
        /// <returns>highest rated publisher, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Publisher"/></returns>
        public KeyValuePair<double?, Publisher> GetHighestRatedPublisher()
        {
            var temp = ReadAll().Where(t => t.Books.Any(u => u.Rating != null)).OrderByDescending(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
            return new KeyValuePair<double?, Publisher>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <summary>
        /// Returns the <see cref="Publisher"/> which has the lowest average <see cref="Publisher.Books"/> rating
        /// </summary>
        /// <returns>lowest rated publisher, where the <see langword="Key"/> is the average rating and the <see langword="Value"/> is the <see cref="Publisher"/></returns>
        public KeyValuePair<double?, Publisher> GetLowestRatedPublisher()
        {
            var temp = ReadAll().Where(t => t.Books.Any(u => u.Rating != null)).OrderBy(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
            return new KeyValuePair<double?, Publisher>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <summary>
        /// Returns the average rating of all the books of the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <returns>average rating of the publisher</returns>
        public double? GetRatingOfPublisher(Publisher publisher)
        {
            if (publisher == null) return null;
            return publisher.Books.Average(t => t.Rating);
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who published at least one <see cref="Book"/> at the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <returns>authors of the publisher</returns>
        public IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher)
        {
            if (publisher == null) return Enumerable.Empty<Author>();
            return publisher.Books.SelectMany(t => t.Authors, (t, u) => u).Distinct().ToList();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who only published <see cref="Book"/> at the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <returns>permanent authors of the publisher</returns>
        public IEnumerable<Author> GetPermanentAuthorsOfPublisher(Publisher publisher)
        {
            if (publisher == null) return Enumerable.Empty<Author>();
            return publisher.Books
                .SelectMany(book => book.Authors, (book, author) => author)
                .Distinct()
                .Where(author => author.Books.All(book => book.Publisher == publisher))
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who only published at one <see cref="Publisher"/>
        /// </summary>
        /// <returns>permanent authors</returns>
        public IEnumerable<Author> GetPermanentAuthors()
        {
            return ReadAll()
                .SelectMany(publisher => publisher.Books, (publisher, book) => new { publisher, book })
                .SelectMany(pb => pb.book.Authors, (pb, author) => new { pb.publisher, author })
                .Where(pa => pa.author.Books.All(book => book.Publisher != null && book.Publisher == pa.publisher))
                .Select(pa => pa.author)
                .Distinct()
                .ToList();

            // note: I know it would have been way easier if I would have passed through the AuthorRepository in the constructor, but why take the easy way?
        }
    }
}