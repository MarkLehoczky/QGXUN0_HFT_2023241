using QGXUN0_HFT_2023241.Client.Actions;

namespace QGXUN0_HFT_2023241.Client
{
    class ModelAction
    {
        private static WebService web;

        static ModelAction()
        {
            web = new WebService("http://localhost:43016/");

            AuthorAction.web = web;
            BookAction.web = web;
            CollectionAction.web = web;
            PublisherAction.web = web;
        }

        public static AuthorAction Author { get => AuthorAction.Static; }
        public static BookAction Book { get => BookAction.Static; }
        public static CollectionAction Collection { get => CollectionAction.Static; }
        public static PublisherAction Publisher { get => PublisherAction.Static; }
    }
}
