using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023241.Models.Models;
using QGXUN0_HFT_2023242.WPFClient.Logics.Interfaces;
using QGXUN0_HFT_2023242.WPFClient.Services;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace QGXUN0_HFT_2023242.WPFClient.ViewModels
{
    public class PublisherManagerViewModel : ObservableRecipient
    {
        private Publisher selectedItem;

        public WebList<Publisher> Items { get; private set; }
        public List<CommandButton> CrudButtons { get; set; }
        public List<CommandButton> NonCrudButtons { get; set; }
        public Publisher SelectedItem
        {
            get => selectedItem; set
            {
                SetProperty(ref selectedItem, value);
                NotifyChanges();
            }
        }

        public PublisherManagerViewModel() : this(
            (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue
            ? null
            : Ioc.Default.GetService<IPublisherWindowLogic>())
        { }
        public PublisherManagerViewModel(IPublisherWindowLogic logic)
        {
            Items = new PublisherWebList();
            Items.CollectionChanged += NotifyChanges;
            logic.Setup(Items);

            CrudButtons = new List<CommandButton>()
            {
                new("Create new publisher",
                    () => logic.Create(),
                    () => Items != null
                ),
                new("View the publisher",
                    () => logic.Read(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View all publishers",
                    () => logic.ReadAll(),
                    () => Items != null && Items.Count > 0
                ),
                new("Update the publisher",
                    () => logic.Update(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Delete the publisher",
                    () => logic.Delete(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
            };

            NonCrudButtons = new List<CommandButton>()
            {
                new("View series publishers",
                    () => logic.Series(),
                    () => Items != null && Items.Count > 0
                ),
                new("View only series publishers",
                    () => logic.OnlySeries(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the highest rated publisher",
                    () => logic.HighestRated(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the lowest rated publisher",
                    () => logic.LowestRated(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the rating of the publisher",
                    () => logic.Rating(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View the authors of the publisher",
                    () => logic.Authors(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && SelectedItem.Books.SelectMany(t => t.Authors).Any()
                ),
                new("View the permanent authors",
                    () => logic.PermanentAuthors(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the permanent authors of the publisher",
                    () => logic.PermanentAuthorsOfPublisher(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && SelectedItem.Books.SelectMany(t => t.Authors).Any()
                ),
            };

            Messenger.Register<PublisherManagerViewModel, string, string>(this, "PublisherModification", (_, _) => { });
        }


        private void NotifyChanges()
        {
            foreach (var item in CrudButtons)
                (item.Command as RelayCommand)?.NotifyCanExecuteChanged();

            foreach (var item in NonCrudButtons)
                (item.Command as RelayCommand)?.NotifyCanExecuteChanged();
        }
        private void NotifyChanges(object? sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyChanges();
        }
    }
}
