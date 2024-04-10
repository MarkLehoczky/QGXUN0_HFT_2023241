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
    public class BookManagerViewModel : ObservableRecipient
    {
        private Book selectedItem;

        public WebList<Book> Items { get; private set; }
        public List<CommandButton> CrudButtons { get; set; }
        public List<CommandButton> NonCrudButtons { get; set; }
        public Book SelectedItem
        {
            get => selectedItem; set
            {
                SetProperty(ref selectedItem, value);
                NotifyChanges();
            }
        }

        public BookManagerViewModel() : this(
            (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue
            ? null
            : Ioc.Default.GetService<IBookWindowLogic>())
        { }
        public BookManagerViewModel(IBookWindowLogic logic)
        {
            Items = new BookWebList();
            Items.CollectionChanged += NotifyChanges;
            logic.Setup(Items);

            CrudButtons = new List<CommandButton>()
            {
                new("Create new book",
                    () => logic.Create(),
                    () => Items != null
                ),
                new("View the book",
                    () => logic.Read(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("View all books",
                    () => logic.ReadAll(),
                    () => Items != null && Items.Count > 0
                ),
                new("Update the book",
                    () => logic.Update(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Delete the book",
                    () => logic.Delete(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
            };

            NonCrudButtons = new List<CommandButton>()
            {
                new("Add authors to the book",
                    () => logic.AddAuthors(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null
                ),
                new("Remove authors from the book",
                    () => logic.RemoveAuthors(SelectedItem),
                    () => Items != null && Items.Count > 0 && SelectedItem != null && SelectedItem.Authors.Any()
                ),
                new("View books in a year",
                    () => logic.InYear(),
                    () => Items != null && Items.Count > 0
                ),
                new("View books between years",
                    () => logic.BetweenYears(),
                    () => Items != null && Items.Count > 0
                ),
                new("View a book by filter",
                    () => logic.Select(),
                    () => Items != null && Items.Count > 0
                ),
                new("View books by titles",
                    () => logic.TitleContains(),
                    () => Items != null && Items.Count > 0
                ),
            };

            Messenger.Register<BookManagerViewModel, string, string>(this, "BookModification", (_, _) => { });
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
