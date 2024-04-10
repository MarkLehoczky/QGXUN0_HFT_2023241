using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;

namespace QGXUN0_HFT_2023242.WPFClient.Logics
{
    public class MainMenuLogic : IMainMenuLogic
    {
        private readonly AuthorManagerWindow authorManager;
        private readonly BookManagerWindow bookManager;
        private readonly CollectionManagerWindow collectionManager;
        private readonly PublisherManagerWindow publisherManager;


        public MainMenuLogic(AuthorManagerWindow authorManager, BookManagerWindow bookManager, CollectionManagerWindow collectionManager, PublisherManagerWindow publisherManager)
        {
            this.authorManager = authorManager;
            this.bookManager = bookManager;
            this.collectionManager = collectionManager;
            this.publisherManager = publisherManager;
        }


        public void OpenAuthorManager() => authorManager.Show();
        public void OpenBookManager() => bookManager.Show();
        public void OpenCollectionManager() => collectionManager.Show();
        public void OpenPublisherManager() => publisherManager.Show();
    }
}
