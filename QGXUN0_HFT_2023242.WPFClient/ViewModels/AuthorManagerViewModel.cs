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
    public class AuthorManagerViewModel : ObservableRecipient
    {
        private Author selectedItem;

        public WebList<Author> Items { get; private set; }
        public List<CommandButton> CrudButtons { get; set; }
        public List<CommandButton> NonCrudButtons { get; set; }
        public Author SelectedItem
        {
            get => selectedItem; set
            {
                SetProperty(ref selectedItem, value);
                NotifyChanges();
            }
        }

        public AuthorManagerViewModel() : this(
            (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue
        ? null
            : Ioc.Default.GetService<IAuthorWindowLogic>())
        { }
        public AuthorManagerViewModel(IAuthorWindowLogic logic)
        {
            Items = new AuthorWebList();
            Items.CollectionChanged += NotifyChanges;
            logic.Setup(Items);

            CrudButtons = new List<CommandButton>()
            {
                new("Create new author",
                    () => logic.Create(),
                    () => Items != null
                ),
                new("View the author",
                    () => logic.Read(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View all authors",
                    () => logic.ReadAll(),
                    () => Items != null && Items.Count > 0
                ),
                new("Update the author",
                    () => logic.Update(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Delete the author",
                    () => logic.Delete(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
            };

            NonCrudButtons = new List<CommandButton>()
            {
                new("View the highest rated author",
                    () => logic.HighestRated(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the lowest rated author",
                    () => logic.LowestRated(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the series of the author",
                    () => logic.Series(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View a book by filter from the author",
                    () => logic.SelectBook(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && SelectedItem.Books.Any()
                ),
            };

            Messenger.Register<AuthorManagerViewModel, string, string>(this, "AuthorModification", (_, _) => { });
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
