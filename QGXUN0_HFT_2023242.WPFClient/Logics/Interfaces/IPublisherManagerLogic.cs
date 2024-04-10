using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Services;

namespace QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces
{
    public interface IPublisherManagerLogic
    {
        void Authors(Publisher publisher);
        void Create();
        void Delete(Publisher publisher);
        void HighestRated();
        void LowestRated();
        void OnlySeries();
        void PermanentAuthors();
        void PermanentAuthorsOfPublisher(Publisher publisher);
        void Rating(Publisher publisher);
        void Read(Publisher publisher);
        void ReadAll();
        void Series();
        void Setup(WebList<Publisher> webList);
        void Update(Publisher publisher);
    }
}