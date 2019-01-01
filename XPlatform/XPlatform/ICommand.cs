using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XPlatform
{
    public interface ICommand
    {
        void Execute(Object parameter);

        bool CanExecute(Object parameter);

        event EventHandler CanExecuteChanged;
    }


    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Action<T> execute)
           : this(execute, null)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add {  }
            remove {  }
        }
    }

    public class RelayCommand : Command
    {
        public RelayCommand(Action<object> execute)
            : base(execute)
        {
        }

        public RelayCommand(Action execute)
            : this(o => execute())
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute, INotifyPropertyChanged npc = null)
            : base(execute, canExecute)
        {
            if (npc != null)
                npc.PropertyChanged += delegate { ChangeCanExecute(); };
        }

        public RelayCommand(Action execute, Func<bool> canExecute, INotifyPropertyChanged npc = null)
            : this(o => execute(), o => canExecute(), npc)
        {
        }
    }
}
