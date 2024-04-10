using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using QGXUN0_HFT_2023242.WPFClient.Logics;
using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection()

                .AddSingleton<IMainMenuLogic, MainMenuLogic>()

                .AddSingleton<IAuthorManagerLogic, AuthorManagerLogic>()
                .AddSingleton<IBookManagerLogic, BookManagerLogic>()
                .AddSingleton<ICollectionManagerLogic, CollectionManagerLogic>()
                .AddSingleton<IPublisherManagerLogic, PublisherManagerLogic>()

                .AddSingleton<AuthorManager>()
                .AddSingleton<BookManager>()
                .AddSingleton<CollectionManager>()
                .AddSingleton<PublisherManager>()

                .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                
                .BuildServiceProvider());
        }
    }
}
