using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace QGXUN0_HFT_2023242.WPFClient.ViewModels
{
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
        public CommandButton(string name, Action action)
        {
            Name = name;
            Command = new RelayCommand(action, () => true);
        }
        public CommandButton(string name, Action action, Func<bool> condition)
        {
            Name = name;
            Command = new RelayCommand(action, condition);
        }
    }
}
