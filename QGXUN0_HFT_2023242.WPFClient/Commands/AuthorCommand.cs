using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023242.WPFClient.Logics;

namespace QGXUN0_HFT_2023241.WPFClient.Commands
{
    public static class AuthorCommand
    {
        private static AuthorWindowLogic logic = new AuthorWindowLogic();


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


        public static RelayCommand HighestRated = new(
            () => logic.HighestRated(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand LowestRated = new(
            () => logic.LowestRated(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Series = new(
            () => logic.Series(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand SelectBook = new(
            () => logic.SelectBook(),
            () => logic.NotEmptyCondition()
        );


        public static void NotifyChanges()
        {
            Create.NotifyCanExecuteChanged();
            Read.NotifyCanExecuteChanged();
            Update.NotifyCanExecuteChanged();
            Delete.NotifyCanExecuteChanged();
            ReadAll.NotifyCanExecuteChanged();
            HighestRated.NotifyCanExecuteChanged();
            LowestRated.NotifyCanExecuteChanged();
            Series.NotifyCanExecuteChanged();
            SelectBook.NotifyCanExecuteChanged();
        }
    }
}
