using QGXUN0_HFT_2023241.Models.Extensions;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IBookLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        bool AddAuthorsToBook(Book book, IEnumerable<Author> authors);
        bool AddAuthorsToBook(Book book, params Author[] authors);
        int? Create(Book book);
        int? Create(Book book, IEnumerable<Author> authors);
        int? Create(Book book, params Author[] authors);
        bool Delete(Book book);
        IEnumerable<Book> GetBooksBetweenYears(int minimumYear, int maximumYear);
        IEnumerable<Book> GetBooksInYear(int year);
        IEnumerable<Book> GetBooksWithTitle(string text);
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(IEnumerable<string> texts);
        IDictionary<string, IEnumerable<Book>> GetBooksWithTitles(params string[] texts);
        Book Read(int bookID);
        IQueryable<Book> ReadAll();
        bool RemoveAuthorsFromBook(Book book, IEnumerable<Author> authors);
        bool RemoveAuthorsFromBook(Book book, params Author[] authors);
        Book SelectBook(BookFilter bookFilter);
        bool Update(Book book);
    }
}