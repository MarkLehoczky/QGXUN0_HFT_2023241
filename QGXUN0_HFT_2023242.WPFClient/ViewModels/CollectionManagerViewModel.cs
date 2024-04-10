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
    public class CollectionManagerViewModel : ObservableRecipient
    {
        private Collection selectedItem;

        public WebList<Collection> Items { get; private set; }
        public List<CommandButton> CrudButtons { get; set; }
        public List<CommandButton> NonCrudButtons { get; set; }
        public Collection SelectedItem
        {
            get => selectedItem; set
            {
                SetProperty(ref selectedItem, value);
                NotifyChanges();
            }
        }

        public CollectionManagerViewModel() : this(
            (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue
        ? null
            : Ioc.Default.GetService<ICollectionWindowLogic>())
        { }
        public CollectionManagerViewModel(ICollectionWindowLogic logic)
        {
            Items = new CollectionWebList();
            Items.CollectionChanged += NotifyChanges;
            logic.Setup(Items);

            CrudButtons = new List<CommandButton>()
            {
                new("Create new collection",
                    () => logic.Create(),
                    () => Items != null
                ),
                new("View the collection",
                    () => logic.Read(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View all collections",
                    () => logic.ReadAll(),
                    () => Items != null && Items.Count > 0
                ),
                new("Update the collection",
                    () => logic.Update(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Delete the collection",
                    () => logic.Delete(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
            };

            NonCrudButtons = new List<CommandButton>()
            {
                new("Add books to the collection",
                    () => logic.AddBooks(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Remove books from the collection",
                    () => logic.RemoveBooks(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && selectedItem.Books.Any()
                ),
                new("Remove all books from the collection",
                    () => logic.ClearBooks(SelectedItem),
                    () => Items != null && Items.Count > 0 && selectedItem.Books.Any()
                ),
                new("View series collections",
                    () => logic.Series(),
                    () => Items != null && Items.Count > 0
                ),
                new("View non-series collections",
                    () => logic.NonSeries(),
                    () => Items != null && Items.Count > 0
                ),
                new("View collections in a year",
                    () => logic.InYear(),
                    () => Items != null && Items.Count > 0
                ),
                new("View collections between years",
                    () => logic.BetweenYears(),
                    () => Items != null && Items.Count > 0
                ),
                new("View the price of the collection",
                    () => logic.Price(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && selectedItem.Books.Any()
                    ),
                new("View the rating of the collection",
                    () => logic.Rating(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && selectedItem.Books.Any()
                ),
                new("View a collection by filter",
                    () => logic.Select(),
                    () => Items != null && Items.Count > 0
                ),
                new("View a book by filter from the collection",
                    () => logic.SelectBook(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && selectedItem.Books.Any()
                )
            };

            Messenger.Register<CollectionManagerViewModel, string, string>(this, "CollectionModification", (_, _) => { });
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
