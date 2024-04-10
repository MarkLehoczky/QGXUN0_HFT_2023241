using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.ViewModels
{
    public class MainMenuViewModel : ObservableRecipient
    {
        public List<CommandButton> Buttons { get; set; }

        public MainMenuViewModel() : this(
            (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue
            ? null
            : Ioc.Default.GetService<IMainMenuLogic>()) { }
        public MainMenuViewModel(IMainMenuLogic logic)
        {
            Buttons = new List<CommandButton>()
            {
                new CommandButton("Author Manager", new RelayCommand(() => logic.OpenAuthorManager())),
                new CommandButton("Book Manager", new RelayCommand(() => logic.OpenBookManager())),
                new CommandButton("Collection Manager", new RelayCommand(() => logic.OpenCollectionManager())),
                new CommandButton("Publisher Manager", new RelayCommand(() => logic.OpenPublisherManager()))
            };
        }
    }
}
