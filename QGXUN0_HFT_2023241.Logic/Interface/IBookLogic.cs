using QGXUN0_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IBookLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool AddAuthorsToBook(Book book, IEnumerable<Author> authors);
        bool AddAuthorsToBook(Book book, IEnumerable<int> authorIDs);
        bool AddAuthorsToBook(Book book, params Author[] authors);
        bool AddAuthorsToBook(Book book, params int[] authorIDs);
        bool AddNewAuthorsToBook(Book book, IEnumerable<Author> authors);
        bool AddNewAuthorsToBook(Book book, IEnumerable<int> authorIDs);
        bool AddNewAuthorsToBook(Book book, params Author[] authors);
        bool AddNewAuthorsToBook(Book book, params int[] authorIDs);
        bool Contains(Book book);
        bool ContainsAll(IEnumerable<Book> books);
        bool ContainsAll(params Book[] book);
        bool ContainsAny(IEnumerable<Book> books);
        bool ContainsAny(params Book[] books);
        int? Create(Book book);
        int? Create(Book book, IEnumerable<Author> authors);
        int? Create(Book book, IEnumerable<int> authorIDs);
        int? Create(Book book, params Author[] authors);
        int? Create(Book book, params int[] authorIDs);
        bool Delete(Book book);
        bool Delete(int bookID);
        bool DeleteAll();
        bool DeleteBetween(int minimumID, int maximumID);
        bool DeleteRange(IEnumerable<Book> books);
        bool DeleteRange(IEnumerable<int> bookIDs);
        bool DeleteRange(params Book[] books);
        bool DeleteRange(params int[] bookIDs);
        IEnumerable<Book> GetBooksBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Book> GetBooksFromAuthor(Author author);
        IEnumerable<Book> GetBooksInYear(int year);
        IEnumerable<Book> GetBooksWithTitle(string text);
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(IEnumerable<string> texts);
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts);
        Book GetHighestRatedBook();
        Book GetLeastExpensiveBook();
        Book GetLowestRatedBook();
        Book GetMostExpensiveBook();
        IEnumerable<IGrouping<int, Book>> GroupByNumberOfAuthors();
        Book Read(int bookID);
        IQueryable<Book> ReadAll();
        IQueryable<Book> ReadBetween(int minimumID, int maximumID);
        IQueryable<Book> ReadRange(IEnumerable<Book> books);
        IQueryable<Book> ReadRange(IEnumerable<int> bookIDs);
        IQueryable<Book> ReadRange(params Book[] books);
        IQueryable<Book> ReadRange(params int[] bookIDs);
        bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors);
        bool RemoveAuthorsFromBook(Book book, IEnumerable<int> authorIDs);
        bool RemoveAuthorsFromBook(Book book, params Author[] authors);
        bool RemoveAuthorsFromBook(Book book, params int[] authorIDs);
        bool Update(Book book);
        bool UpdateRange(IEnumerable<Book> books);
        bool UpdateRange(params Book[] books);
    }
}