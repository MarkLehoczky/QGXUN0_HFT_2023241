using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023241.WPFClient.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient.ViewModels
{
    public class MenuViewModel : ObservableRecipient
    {
        public ActiveMenu ActiveMenu { get; set; }
        public List<CommandButton> MainMenuActions { get; set; }
        public string SubMenuTitle { get; set; }
        public ObservableCollection<CommandButton> CrudActions { get; set; }
        public ObservableCollection<CommandButton> NonCrudActions { get; set; }
        public ICommand ReturnCommand { get; set; }


        public MenuViewModel()
        {
            ActiveMenu = ActiveMenu.MAINMENU;
            MainMenuActions = new List<CommandButton>();
            SubMenuTitle = string.Empty;
            CrudActions = new ObservableCollection<CommandButton>();
            NonCrudActions = new ObservableCollection<CommandButton>();


            var authorMenu = new CommandButton("AUTHOR MANAGER", new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "AUTHOR MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));


                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new author", AuthorCommand.Create));
                CrudActions.Add(new CommandButton("List all authors", AuthorCommand.ReadAll));
                CrudActions.Add(new CommandButton("Read author", AuthorCommand.Read));
                CrudActions.Add(new CommandButton("Update author", AuthorCommand.Update));
                CrudActions.Add(new CommandButton("Delete author", AuthorCommand.Delete));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("Highest rated author", AuthorCommand.HighestRated));
                NonCrudActions.Add(new CommandButton("Lowest rated author", AuthorCommand.LowestRated));
                NonCrudActions.Add(new CommandButton("Series from an author", AuthorCommand.Series));
                NonCrudActions.Add(new CommandButton("Select filtered book from an author", AuthorCommand.SelectBook));
            }));

            var bookMenu = new CommandButton("BOOK MANAGER", new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "BOOK MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new book", BookCommand.Create));
                CrudActions.Add(new CommandButton("List all books", BookCommand.ReadAll));
                CrudActions.Add(new CommandButton("Read book", BookCommand.Read));
                CrudActions.Add(new CommandButton("Update book", BookCommand.Update));
                CrudActions.Add(new CommandButton("Delete book", BookCommand.Delete));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("Add authors to a book", BookCommand.AddAuthors));
                NonCrudActions.Add(new CommandButton("Remove authors from a book", BookCommand.RemoveAuthors));
                NonCrudActions.Add(new CommandButton("List books in year", BookCommand.InYear));
                NonCrudActions.Add(new CommandButton("List books between years", BookCommand.BetweenYears));
                NonCrudActions.Add(new CommandButton("List books where the title has texts", BookCommand.TitleContains));
                NonCrudActions.Add(new CommandButton("Select filtered book", BookCommand.Select));
            }));

            var collectionMenu = new CommandButton("COLLECTION MANAGER", new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "COLLECTION MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new collection", CollectionCommand.Create));
                CrudActions.Add(new CommandButton("List all collections", CollectionCommand.ReadAll));
                CrudActions.Add(new CommandButton("Read collection", CollectionCommand.Read));
                CrudActions.Add(new CommandButton("Update collection", CollectionCommand.Update));
                CrudActions.Add(new CommandButton("Delete collection", CollectionCommand.Delete));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("Add books to a collection", CollectionCommand.AddBooks));
                NonCrudActions.Add(new CommandButton("Remove books from a collection", CollectionCommand.RemoveBooks));
                NonCrudActions.Add(new CommandButton("List series collections", CollectionCommand.Series));
                NonCrudActions.Add(new CommandButton("List non-series collections", CollectionCommand.NonSeries));
                NonCrudActions.Add(new CommandButton("List collections in year", CollectionCommand.InYear));
                NonCrudActions.Add(new CommandButton("List collections between years", CollectionCommand.BetweenYears));
                NonCrudActions.Add(new CommandButton("Summarized price of a collection", CollectionCommand.Price));
                NonCrudActions.Add(new CommandButton("Average rating of a collection", CollectionCommand.Rating));
                NonCrudActions.Add(new CommandButton("Select filtered collection", CollectionCommand.Select));
                NonCrudActions.Add(new CommandButton("Select filtered book from a collection", CollectionCommand.SelectBook));
            }));

            var publisherMenu = new CommandButton("PUBLISHER MANAGER", new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "PUBLISHER MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new publisher", PublisherCommand.Create));
                CrudActions.Add(new CommandButton("List all publishers", PublisherCommand.ReadAll));
                CrudActions.Add(new CommandButton("Read publisher", PublisherCommand.Read));
                CrudActions.Add(new CommandButton("Update publisher", PublisherCommand.Update));
                CrudActions.Add(new CommandButton("Delete publisher", PublisherCommand.Delete));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("List series publishers", PublisherCommand.Series));
                NonCrudActions.Add(new CommandButton("List only series publishers", PublisherCommand.OnlySeries));
                NonCrudActions.Add(new CommandButton("Highest rated publisher", PublisherCommand.HighestRated));
                NonCrudActions.Add(new CommandButton("Lowest rated publisher", PublisherCommand.LowestRated));
                NonCrudActions.Add(new CommandButton("Average rating of a publisher", PublisherCommand.Rating));
                NonCrudActions.Add(new CommandButton("Authors", PublisherCommand.Authors));
                NonCrudActions.Add(new CommandButton("Permanent authors", PublisherCommand.PermanentAuthors));
                NonCrudActions.Add(new CommandButton("Permanent authors of a publisher", PublisherCommand.PermanentAuthorsOfPublisher));
            }));

            ReturnCommand = new RelayCommand(
                () => { ActiveMenu = ActiveMenu.MAINMENU; OnPropertyChanged(nameof(ActiveMenu)); },
                () => true);

            MainMenuActions.Add(authorMenu);
            MainMenuActions.Add(bookMenu);
            MainMenuActions.Add(collectionMenu);
            MainMenuActions.Add(publisherMenu);
        }
    }

    public class CommandButton
    {
        public string Name { get; set; }
        public ICommand Command { get; set; }

        public CommandButton()
        {
            Name = string.Empty;
            Command = new RelayCommand(() => { }, () => true);
        }
        public CommandButton(string name)
        {
            Name = name;
            Command = new RelayCommand(() => { }, () => true);
        }
        public CommandButton(string name, ICommand command)
        {
            Name = name;
            Command = command;
        }
    }

    public enum ActiveMenu { MAINMENU, SUBMENU }
}
