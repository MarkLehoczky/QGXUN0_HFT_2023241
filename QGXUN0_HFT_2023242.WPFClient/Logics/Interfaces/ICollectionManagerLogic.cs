using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;

namespace QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces
{
    public interface ICollectionManagerLogic
    {
        void AddBooks(Collection collection);
        void BetweenYears();
        void ClearBooks(Collection collection);
        void Create();
        void Delete(Collection collection);
        void InYear();
        void NonSeries();
        void Price(Collection collection);
        void Rating(Collection collection);
        void Read(Collection collection);
        void ReadAll();
        void RemoveBooks(Collection collection);
        void Select();
        void SelectBook(Collection collection);
        void Series();
        void Setup(WebList<Collection> webList);
        void Update(Collection collection);
    }
}