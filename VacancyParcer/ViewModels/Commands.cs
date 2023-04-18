﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VacancyParcer.ViewModel.ViewModel
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;//в этом делегате хранится метод который проверяет условия, если команда может выполниться то возвращает 1 или 0

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;

        }

        public ActionCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
