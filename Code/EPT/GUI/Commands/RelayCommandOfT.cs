﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace EPT.GUI.Commands
{
    public sealed class RelayCommand<T>
        : ICommand
    {
        #region Private Member

        private readonly Action<T> _Execute;

        private readonly Predicate<T> _CanExecute;

        #endregion Private Member

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._Execute = execute;
            this._CanExecute = canExecute;
        }

        #endregion Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._CanExecute == null ? true : this._CanExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            this._Execute((T)parameter);
        }

        #endregion ICommand Members
    }
}