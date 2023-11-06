using QGXUN0_HFT_2023241.Logic.Interface;
using QGXUN0_HFT_2023241.Models;
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
            // if the publisher attributes are not valid (through ValidationAttribute), then returns
            if (!publisher.IsValid())
                return null;

            // if the publisher already exists, then returns
            // else if the ID already exists, gives a new ID to the publisher
            var read = Read(publisher.PublisherID);
            if (read == publisher)
                return publisher.PublisherID;
            else if (read != null)
                publisher.PublisherID = ReadAll().Max(t => t.PublisherID) + 1;

            // creates the publisher, then returns the ID
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
        /// Deletes a <see cref="Publisher"/> with the same <see cref="Publisher.PublisherID"/>
        /// </summary>
        /// <param name="publisherID"><see cref="Publisher.PublisherID"/> of the <see cref="Publisher"/></param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(int publisherID)
        {
            try { publisherRepository.Delete(publisherID); }
            catch (InvalidOperationException) { return false; }
            return Read(publisherID) == null;
        }
        /// <summary>
        /// Deletes a <see cref="Publisher"/> with the same <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher"><see cref="Publisher"/> instance</param>
        /// <returns><see langword="true"/> if the deleting was successful, otherwise <see langword="false"/></returns>
        public bool Delete(Publisher publisher)
        {
            if (publisher == null) return false;
            return Delete(publisher.PublisherID);
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
        /// Reads a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publisherIDs"><see cref="Publisher.PublisherID"/> values of the publishers</param>
        /// <returns><see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadRange(IEnumerable<int> publisherIDs)
        {
            return ReadAll().Where(t => publisherIDs.Any(u => u == t.PublisherID));
        }
        /// <summary>
        /// Reads a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publishers"><see cref="Publisher"/> instances</param>
        /// <returns><see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadRange(IEnumerable<Publisher> publishers)
        {
            return ReadRange(publishers.Select(t => t.PublisherID));
        }
        /// <summary>
        /// Reads a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publisherIDs"><see cref="Publisher.PublisherID"/> values of the publishers</param>
        /// <returns><see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadRange(params int[] publisherIDs)
        {
            return ReadRange(publisherIDs.AsEnumerable());
        }
        /// <summary>
        /// Reads a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publishers"><see cref="Publisher"/> instances</param>
        /// <returns><see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadRange(params Publisher[] publishers)
        {
            return ReadRange(publishers.AsEnumerable());
        }
        /// <summary>
        /// Reads a range of <see cref="Publisher"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Publisher.PublisherID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Publisher.PublisherID"/></param>
        /// <returns><see cref="Publisher"/> instances as <c><see cref="IQueryable"/></c></returns>
        public IQueryable<Publisher> ReadBetween(int minimumID, int maximumID)
        {
            return ReadAll().Where(t => t.PublisherID >= minimumID && t.PublisherID <= maximumID);
        }

        /// <summary>
        /// Updates a range of <paramref name="publishers"/> with the same <see cref="Publisher.PublisherID"/> values
        /// </summary>
        /// <remarks>The <see cref="Publisher.PublisherID"/> values of the <paramref name="publishers"/> must be the same as the ones intended to update</remarks>
        /// <param name="publishers">updated publishers</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(IEnumerable<Publisher> publishers)
        {
            bool successful = true;

            foreach (var item in publishers)
                if (!Update(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Updates a range of <paramref name="publishers"/> with the same <see cref="Publisher.PublisherID"/> values
        /// </summary>
        /// <remarks>The <see cref="Publisher.PublisherID"/> values of the <paramref name="publishers"/> must be the same as the ones intended to update</remarks>
        /// <param name="publishers">updated publishers</param>
        /// <returns><see langword="true"/> if every update was successful, otherwise <see langword="false"/></returns>
        public bool UpdateRange(params Publisher[] publishers)
        {
            return UpdateRange(publishers.AsEnumerable());
        }

        /// <summary>
        /// Deletes a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publisherIDs"><see cref="Publisher.PublisherID"/> values of the <see cref="Publisher"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<int> publisherIDs)
        {
            bool successful = true;

            foreach (var item in publisherIDs)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publishers"><see cref="Publisher"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(IEnumerable<Publisher> publishers)
        {
            bool successful = true;

            foreach (var item in publishers)
                if (!Delete(item) && successful)
                    successful = false;

            return successful;
        }
        /// <summary>
        /// Deletes a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publisherIDs"><see cref="Publisher.PublisherID"/> values of the <see cref="Publisher"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params int[] publisherIDs)
        {
            return DeleteRange(publisherIDs.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Publisher"/> instances
        /// </summary>
        /// <param name="publishers"><see cref="Publisher"/> instances</param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteRange(params Publisher[] publishers)
        {
            return DeleteRange(publishers.AsEnumerable());
        }
        /// <summary>
        /// Deletes a range of <see cref="Publisher"/> instances between the given <paramref name="minimumID"/> and <paramref name="maximumID"/>
        /// </summary>
        /// <param name="minimumID">minimum value of the <see cref="Publisher.PublisherID"/></param>
        /// <param name="maximumID">maximum value of the <see cref="Publisher.PublisherID"/></param>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteBetween(int minimumID, int maximumID)
        {
            return DeleteRange(ReadBetween(minimumID, maximumID));
        }
        /// <summary>
        /// Deletes every <see cref="Publisher"/> instances
        /// </summary>
        /// <returns><see langword="true"/> if every deleting was successful, otherwise <see langword="false"/></returns>
        public bool DeleteAll()
        {
            return DeleteRange(ReadAll());
        }


        /// <summary>
        /// Determines whether the <see cref="Publisher"/> instances contains the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">searched publisher</param>
        /// <returns><see langword="true"/> if the <paramref name="publisher"/> was found, otherwise <see langword="false"/></returns>
        public bool Contains(Publisher publisher)
        {
            return ReadAll().Contains(publisher);
        }
        /// <summary>
        /// Determines whether the <see cref="Publisher"/> instances contains any of the <paramref name="publishers"/>
        /// </summary>
        /// <param name="publishers">searched publishers</param>
        /// <returns><see langword="true"/> if any of the <paramref name="publishers"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(IEnumerable<Publisher> publishers)
        {
            return publishers.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Publisher"/> instances contains any of the <paramref name="publishers"/>
        /// </summary>
        /// <param name="publishers">searched publishers</param>
        /// <returns><see langword="true"/> if any of the <paramref name="publishers"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAny(params Publisher[] publishers)
        {
            return publishers.Any(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Publisher"/> instances contains all the <paramref name="publishers"/>
        /// </summary>
        /// <param name="publishers">searched publishers</param>
        /// <returns><see langword="true"/> if all the <paramref name="publishers"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(IEnumerable<Publisher> publishers)
        {
            return publishers.All(t => Contains(t));
        }
        /// <summary>
        /// Determines whether the <see cref="Publisher"/> instances contains all the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">searched publishers</param>
        /// <returns><see langword="true"/> if all the <paramref name="publisher"/> was found, otherwise <see langword="false"/></returns>
        public bool ContainsAll(params Publisher[] publisher)
        {
            return publisher.All(t => Contains(t));
        }


        /// <summary>
        /// Returns all <see cref="Publisher"/> who released at least one series
        /// </summary>
        /// <returns>series publishers</returns>
        public IEnumerable<Publisher> GetSeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.Any(u => u.Collections.Any(v => v.IsSeries.HasValue && v.IsSeries.Value == true))).ToList();
        }
        /// <summary>
        /// Returns all <see cref="Publisher"/> who only released series
        /// </summary>
        /// <returns>only series publishers</returns>
        public IEnumerable<Publisher> GetOnlySeriesPublishers()
        {
            return ReadAll().Where(t => t.Books.All(u => u.Collections.Any(v => v.IsSeries.HasValue && v.IsSeries.Value == true))).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who published at least one <see cref="Book"/> at the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <returns>authors of the publisher</returns>
        public IEnumerable<Author> GetAuthorsOfPublisher(Publisher publisher)
        {
            if (publisher == null) return Enumerable.Empty<Author>();
            return publisher.Books.SelectMany(t => t.Authors, (t, u) => u).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who only published <see cref="Book"/> at the <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <returns>permanent authors of the publisher</returns>
        public IEnumerable<Author> GetPermanentAuthorsOfPublisher(Publisher publisher)
        {
            if (publisher == null) return Enumerable.Empty<Author>();
            return publisher.Books.SelectMany(book => book.Authors, (book, author) => author).Distinct().Where(author => author.Books.All(book => book.Publisher == publisher)).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Author"/> instances who only published at one <see cref="Publisher"/>
        /// </summary>
        /// <returns>permanent authors</returns>
        public IEnumerable<Author> GetPermanentAuthors()
        {
            // "flattens" the publishers' books, then "flattens" the books' authors, then selects the authors who only published at one publisher
            return ReadAll()
                .SelectMany(publisher => publisher.Books, (publisher, book) => new { publisher, book })
                .SelectMany(pb => pb.book.Authors, (pb, author) => new { pb.publisher, author })
                .Where(pa => pa.author.Books.All(book => book.Publisher != null && book.Publisher == pa.publisher))
                .Select(pa => pa.author)
                .Distinct().ToList();

            // note: I know it would have been way easier if I would have passed through the AuthorRepository in the constructor, but why take the easy way?
        }

        /// <summary>
        /// Returns all <see cref="Book"/> with the given <paramref name="year"/> from the given <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <param name="year">value of the <see cref="Book.Year"/></param>
        /// <returns>all book in the given <paramref name="year"/> from the <paramref name="publisher"/></returns>
        public IEnumerable<Book> GetBooksFromPublisherInYear(Publisher publisher, int year)
        {
            if (publisher == null) return Enumerable.Empty<Book>();
            return publisher.Books.Where(t => t.Year == year).ToList();
        }

        /// <summary>
        /// Returns all <see cref="Book"/> between the <paramref name="minimumYear"/> and <paramref name="maximumYear"/> from the given <paramref name="publisher"/>
        /// </summary>
        /// <param name="publisher">publisher</param>
        /// <param name="minimumYear">minimum value of the <see cref="Book.Year"/></param>
        /// <param name="maximumYear">maximum value of the <see cref="Book.Year"/></param>
        /// <returns>all book in the given interval from the <paramref name="publisher"/></returns>
        public IEnumerable<Book> GetBooksFromPublisherBetweenYears(Publisher publisher, int minimumYear, int maximumYear)
        {
            if (publisher == null) return Enumerable.Empty<Book>();
            return publisher.Books.Where(t => t.Year >= minimumYear && t.Year <= maximumYear).ToList();
        }

        /// <summary>
        /// Returns the <see cref="Publisher"/> instances grouped by their number of <see cref="Publisher.Books"/>
        /// </summary>
        /// <returns>grouped publishers</returns>
        public IEnumerable<IGrouping<int, Publisher>> GroupByNumberOfBooks()
        {
            return ReadAll().OrderBy(t => t.Books.Count).ToList().GroupBy(u => u.Books.Count);
        }
    }
}