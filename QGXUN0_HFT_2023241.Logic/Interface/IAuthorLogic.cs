using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IAuthorLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool Contains(Author author);
        bool ContainsAll(IEnumerable<Author> authors);
        bool ContainsAll(params Author[] author);
        bool ContainsAny(IEnumerable<Author> authors);
        bool ContainsAny(params Author[] authors);
        int? Create(Author author);
        bool Delete(Author author);
        bool Delete(int authorID);
        bool DeleteAll();
        bool DeleteBetween(int minimumID, int maximumID);
        bool DeleteRange(IEnumerable<Author> authors);
        bool DeleteRange(IEnumerable<int> authorIDs);
        bool DeleteRange(params Author[] authors);
        bool DeleteRange(params int[] authorIDs);
        KeyValuePair<double, Author> GetHighestRatedAuthor();
        Book GetHighestRatedBookFromAuthor(Author author);
        Book GetLeastExpensiveBookFromAuthor(Author author);
        KeyValuePair<double, Author> GetLowestRatedAuthor();
        Book GetLowestRatedBookFromAuthor(Author author);
        Book GetMostExpensiveBookFromAuthor(Author author);
        IEnumerable<Collection> GetSeriesOfAuthor(Author author);
        IEnumerable<IGrouping<int, Author>> GroupByNumberOfBooks();
        Author Read(int authorID);
        IQueryable<Author> ReadAll();
        IQueryable<Author> ReadBetween(int minimumID, int maximumID);
        IQueryable<Author> ReadRange(IEnumerable<Author> authors);
        IQueryable<Author> ReadRange(IEnumerable<int> authorIDs);
        IQueryable<Author> ReadRange(params Author[] authors);
        IQueryable<Author> ReadRange(params int[] authorIDs);
        bool Update(Author author);
        bool UpdateRange(IEnumerable<Author> authors);
        bool UpdateRange(params Author[] authors);
    }
}