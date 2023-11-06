using QGXUN0_HFT_2023241.Models;
using QGXUN0_HFT_2023241.Repository.Template;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic
{
    /// <summary>
    /// Implements all CRUD and non-crud methods for the <see cref="Publisher"/> model
    /// </summary>
    public class PublisherLogic
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
    }
}