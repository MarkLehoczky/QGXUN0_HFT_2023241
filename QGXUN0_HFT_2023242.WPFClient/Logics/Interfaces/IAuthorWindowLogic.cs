using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;

namespace QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces
{
    public interface IAuthorWindowLogic
    {
        void Create();
        void Delete(Author author);
        void HighestRated();
        void LowestRated();
        void Read(Author author);
        void ReadAll();
        void SelectBook(Author author);
        void Series(Author author);
        void Setup(WebList<Author> webList);
        void Update(Author author);
    }
}