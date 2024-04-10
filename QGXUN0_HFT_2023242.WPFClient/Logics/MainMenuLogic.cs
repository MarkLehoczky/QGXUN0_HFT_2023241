using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class MainMenuLogic : IMainMenuLogic
    {
        private readonly AuthorManager authorManager;
        private readonly BookManager bookManager;
        private readonly CollectionManager collectionManager;
        private readonly PublisherManager publisherManager;


        public MainMenuLogic(AuthorManager authorManager, BookManager bookManager, CollectionManager collectionManager, PublisherManager publisherManager)
        {
            this.authorManager = authorManager;
            this.bookManager = bookManager;
            this.collectionManager = collectionManager;
            this.publisherManager = publisherManager;
        }


        public void OpenAuthorManager() => authorManager.Open();
        public void OpenBookManager() => bookManager.Open();
        public void OpenCollectionManager() => collectionManager.Open();
        public void OpenPublisherManager() => publisherManager.Open();
    }
}
