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
    public class PublisherLogic : IPublisherLogic
    {
        /// <inheritdoc/>
        public int Count { get => ReadAll().Count(); }

        /// <inheritdoc/>
        public bool IsEmpty { get => Count == 0; }

        /// <summary>
        /// Specifies an instance of the <see cref="Repository{Publisher}"/>
        /// </summary>
        private readonly IRepository<Publisher> _publisherRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> <see langword="class"/> by the <see cref="Repository{Author}"/> instance.
        /// </summary>
        /// <param name="publisherRepository"><see cref="Publisher"/> repository instance</param>
        public PublisherLogic(IRepository<Publisher> publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }


        /// <summary>
        /// Creates an <see cref="Publisher"/> instance.
        /// </summary>
        /// <remarks>The <see cref="Publisher.PublisherID"/> of the <see cref="Publisher"/> instance may be changed</remarks>
        /// <param name="publisher">New <see cref="Publisher"/> instance</param>
        /// <returns><see cref="Publisher.PublisherID"/> of the <paramref name="publisher"/> instance if the creating was successful; otherwise, <see langword="null"/></returns>
        public int? Create(Publisher publisher)
        {
            if (!publisher.IsValid())
                return null;

            if (ReadAll().AsEnumerable().Contains(publisher))
                return ReadAll().AsEnumerable().FirstOrDefault(t => t == publisher)?.PublisherID;

            if (Read(publisher.PublisherID) != null)
                publisher.PublisherID = ReadAll().Max(t => t.PublisherID) + 1;

            _publisherRepository.Create(publisher);
            return publisher.PublisherID;
        }

        /// <summary>
        /// Reads an <see cref="Publisher"/> instance.
        /// </summary>
        /// <param name="publisherID"><see cref="Publisher.PublisherID"/> of the read <see cref="Publisher"/> instance</param>
        /// <returns><see cref="Publisher"/> instance if the instance is found; otherwise, <see langword="null"/></returns>
        public Publisher Read(int publisherID)
        {
            try { return _publisherRepository.Read(publisherID); }
            catch (InvalidOperationException) { return null; }
        }

        /// <inheritdoc/>
        public bool Update(Publisher publisher)
        {
            if (!publisher.IsValid() || Read(publisher.PublisherID) == null)
                return false;
            _publisherRepository.Update(publisher);

            return true;
        }

        /// <inheritdoc/>
        public bool Delete(Publisher publisher)
        {
            if (publisher == null) return false;
            try { _publisherRepository.Delete(publisher.PublisherID); return true; }
            catch { return false; }
        }

        /// <inheritdoc/>
        public IQueryable<Publisher> ReadAll()
        {
            return _publisherRepository.ReadAll();
        }


        /// <inheritdoc/>
        public IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher)
        {
            if (publisher == null) return Enumerable.Empty<Author>();
            return publisher.Books.SelectMany(t => t.Authors, (t, u) => u).Distinct().ToList();
        }

        /// <inheritdoc/>
        public KeyValuePair<double?, Publisher> GetHighestRatedPublisher()
        {
            var temp = ReadAll().Where(t => t.Books.Any(u => u.Rating != null)).OrderByDescending(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
            return new KeyValuePair<double?, Publisher>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <inheritdoc/>
        public KeyValuePair<double?, Publisher> GetLowestRatedPublisher()
        {
            var temp = ReadAll().Where(t => t.Books.Any(u => u.Rating != null)).OrderBy(t => t.Books.Average(u => u.Rating)).FirstOrDefault();
            return new KeyValuePair<double?, Publisher>(temp?.Books.Average(t => t.Rating), temp);
        }

        /// <inheritdoc/>
        public IEnumerable<Publisher> GetOnlySeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.Any() && t.Books.All(u => u.Collections.Any(v => v.IsSeries.HasValue == true && v.IsSeries == true))).ToList();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public double? GetRatingOfPublisher(Publisher publisher)
        {
            if (publisher == null) return null;
            return publisher.Books.Average(t => t.Rating);
        }

        /// <inheritdoc/>
        public IEnumerable<Publisher> GetSeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.Any(u => u.Collections.Any(v => v.IsSeries.HasValue == true && v.IsSeries == true))).ToList();
        }
    }
}