using QGXUN0_HFT_2023241.Logic.Logic;
using QGXUN0_HFT_2023241.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace QGXUN0_HFT_2023241.Logic.Interface
{
    public interface IAuthorLogic
    {
        int Count { get; }
        bool IsEmpty { get; }

        int? Create(Author author);
        bool Delete(Author author);
        KeyValuePair<double?, Author> GetHighestRatedAuthor();
        KeyValuePair<double?, Author> GetLowestRatedAuthor();
        IEnumerable<Collection> GetSeriesOfAuthor(Author author);
        Author Read(int authorID);
        IQueryable<Author> ReadAll();
        Book SelectBookFromAuthor(Author author, BookFilter bookFilter);
        bool Update(Author author);
    }
}