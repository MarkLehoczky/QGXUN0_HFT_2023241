using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;

namespace QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces
{
    public interface IBookManagerLogic
    {
        void AddAuthors(Book book);
        void BetweenYears();
        void Create();
        void Delete(Book book);
        void InYear();
        void Read(Book book);
        void ReadAll();
        void RemoveAuthors(Book book);
        void Select();
        void Setup(WebList<Book> webList);
        void TitleContains();
        void Update(Book book);
    }
}