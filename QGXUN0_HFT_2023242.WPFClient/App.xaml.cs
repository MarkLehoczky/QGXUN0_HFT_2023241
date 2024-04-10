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

                .AddSingleton<IAuthorWindowLogic, AuthorWindowLogic>()
                .AddSingleton<IBookWindowLogic, BookWindowLogic>()
                .AddSingleton<ICollectionWindowLogic, CollectionWindowLogic>()
                .AddSingleton<IPublisherWindowLogic, PublisherWindowLogic>()

                .AddSingleton<AuthorManagerWindow>()
                .AddSingleton<BookManagerWindow>()
                .AddSingleton<CollectionManagerWindow>()
                .AddSingleton<PublisherManagerWindow>()

                .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
                
                .BuildServiceProvider());
        }
    }
}
