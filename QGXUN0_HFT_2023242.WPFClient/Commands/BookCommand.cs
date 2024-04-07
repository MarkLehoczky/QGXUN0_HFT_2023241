using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023242.WPFClient.Logics;

namespace QGXUN0_HFT_2023241.WPFClient.Commands
{
    public static class BookCommand
    {
        private static BookWindowLogic logic = new BookWindowLogic();


        public static RelayCommand Create = new(
            () => logic.Create(),
            () => logic.DefaultCondition()
            );

        public static RelayCommand Read = new(
            () => logic.Read(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Update = new(
            () => logic.Update(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Delete = new(
            () => logic.Delete(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand ReadAll = new(
            () => logic.ReadAll(),
            () => logic.NotEmptyCondition()
        );


        public static RelayCommand AddAuthors = new(
            () => logic.AddAuthors(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand RemoveAuthors = new(
            () => logic.RemoveAuthors(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand InYear = new(
            () => logic.InYear(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand BetweenYears = new(
            () => logic.BetweenYears(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand TitleContains = new(
            () => logic.TitleContains(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Select = new(
            () => logic.Select(),
            () => logic.NotEmptyCondition()
        );


        public static void NotifyChanges()
        {
            Create.NotifyCanExecuteChanged();
            Read.NotifyCanExecuteChanged();
            Update.NotifyCanExecuteChanged();
            Delete.NotifyCanExecuteChanged();
            ReadAll.NotifyCanExecuteChanged();
            AddAuthors.NotifyCanExecuteChanged();
            RemoveAuthors.NotifyCanExecuteChanged();
            InYear.NotifyCanExecuteChanged();
            BetweenYears.NotifyCanExecuteChanged();
            TitleContains.NotifyCanExecuteChanged();
            Select.NotifyCanExecuteChanged();
        }
    }
}
