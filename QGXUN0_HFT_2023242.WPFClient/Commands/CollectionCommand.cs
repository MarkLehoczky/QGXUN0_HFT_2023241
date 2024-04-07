using CommunityToolkit.Mvvm.Input;
using QGXUN0_HFT_2023242.WPFClient.Logics;

namespace QGXUN0_HFT_2023241.WPFClient.Commands
{
    public static class CollectionCommand
    {
        private static CollectionWindowLogic logic = new CollectionWindowLogic();


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


        public static RelayCommand AddBooks = new(
            () => logic.AddBooks(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand RemoveBooks = new(
            () => logic.RemoveBooks(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand ClearBooks = new(
            () => logic.ClearBooks(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Series = new(
            () => logic.Series(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand NonSeries = new(
            () => logic.NonSeries(),
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

        public static RelayCommand Price = new(
            () => logic.Price(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Rating = new(
            () => logic.Rating(),
            () => logic.NotEmptyCondition()
        );

        public static RelayCommand Select = new(
            () => logic.Select(),
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
            AddBooks.NotifyCanExecuteChanged();
            RemoveBooks.NotifyCanExecuteChanged();
            ClearBooks.NotifyCanExecuteChanged();
            Series.NotifyCanExecuteChanged();
            NonSeries.NotifyCanExecuteChanged();
            InYear.NotifyCanExecuteChanged();
            BetweenYears.NotifyCanExecuteChanged();
            Price.NotifyCanExecuteChanged();
            Rating.NotifyCanExecuteChanged();
            Select.NotifyCanExecuteChanged();
            SelectBook.NotifyCanExecuteChanged();
        }
    }
}
