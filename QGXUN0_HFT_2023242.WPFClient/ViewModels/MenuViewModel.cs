using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

            var authorMenu = new CommandButton("AUTHOR MANAGER",
                new RelayCommand(() =>
                {
                    ActiveMenu = ActiveMenu.SUBMENU;
                    SubMenuTitle = "AUTHOR MANAGER";
                    OnPropertyChanged(nameof(ActiveMenu));
                    OnPropertyChanged(nameof(SubMenuTitle));

                    CrudActions.Clear();
                    CrudActions.Add(new CommandButton("Create new author"));
                    CrudActions.Add(new CommandButton("List all authors"));
                    CrudActions.Add(new CommandButton("Read author"));
                    CrudActions.Add(new CommandButton("Update author"));
                    CrudActions.Add(new CommandButton("Delete author"));

                    NonCrudActions.Clear();
                    NonCrudActions.Add(new CommandButton("Highest rated author"));
                    NonCrudActions.Add(new CommandButton("Lowest rated author"));
                    NonCrudActions.Add(new CommandButton("Series from an author"));
                    NonCrudActions.Add(new CommandButton("Select filtered book from an author"));
                }));

            var bookMenu = new CommandButton("BOOK MANAGER",
            new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "BOOK MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new book"));
                CrudActions.Add(new CommandButton("List all books"));
                CrudActions.Add(new CommandButton("Read book"));
                CrudActions.Add(new CommandButton("Update book"));
                CrudActions.Add(new CommandButton("Delete book"));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("Add authors to a book"));
                NonCrudActions.Add(new CommandButton("Remove authors from a book"));
                NonCrudActions.Add(new CommandButton("List books in year"));
                NonCrudActions.Add(new CommandButton("List books between years"));
                NonCrudActions.Add(new CommandButton("List books where the title has texts"));
                NonCrudActions.Add(new CommandButton("Select filtered book"));
            }));

            var collectionMenu = new CommandButton("COLLECTION MANAGER",
            new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "COLLECTION MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new collection"));
                CrudActions.Add(new CommandButton("List all collections"));
                CrudActions.Add(new CommandButton("Read collection"));
                CrudActions.Add(new CommandButton("Update collection"));
                CrudActions.Add(new CommandButton("Delete collection"));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("Add books to a collection"));
                NonCrudActions.Add(new CommandButton("Remove books from a collection"));
                NonCrudActions.Add(new CommandButton("List series collections"));
                NonCrudActions.Add(new CommandButton("List non-series collections"));
                NonCrudActions.Add(new CommandButton("List collections in year"));
                NonCrudActions.Add(new CommandButton("List collections between years"));
                NonCrudActions.Add(new CommandButton("Summarized price of a collection"));
                NonCrudActions.Add(new CommandButton("Average rating of a collection"));
                NonCrudActions.Add(new CommandButton("Select filtered collection"));
                NonCrudActions.Add(new CommandButton("Select filtered book from a collection"));
            }));

            var publisherMenu = new CommandButton("PUBLISHER MANAGER",
            new RelayCommand(() =>
            {
                ActiveMenu = ActiveMenu.SUBMENU;
                SubMenuTitle = "PUBLISHER MANAGER";
                OnPropertyChanged(nameof(ActiveMenu));
                OnPropertyChanged(nameof(SubMenuTitle));

                CrudActions.Clear();
                CrudActions.Add(new CommandButton("Create new publisher"));
                CrudActions.Add(new CommandButton("List all publishers"));
                CrudActions.Add(new CommandButton("Read publisher"));
                CrudActions.Add(new CommandButton("Update publisher"));
                CrudActions.Add(new CommandButton("Delete publisher"));

                NonCrudActions.Clear();
                NonCrudActions.Add(new CommandButton("List series publishers"));
                NonCrudActions.Add(new CommandButton("List only series publishers"));
                NonCrudActions.Add(new CommandButton("Highest rated publisher"));
                NonCrudActions.Add(new CommandButton("Lowest rated publisher"));
                NonCrudActions.Add(new CommandButton("Average rating of a publisher"));
                NonCrudActions.Add(new CommandButton("Authors"));
                NonCrudActions.Add(new CommandButton("Permanent authors"));
                NonCrudActions.Add(new CommandButton("Permanent authors of a publisher"));
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
