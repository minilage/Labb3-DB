using System.Windows.Input;

namespace TinasLabb03.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;

        public event EventHandler? CanExecuteChanged;

        // Konstruktor där vi kan skicka in execute och canExecute
        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Anropa denna för att uppdatera knappens status
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        // Bestämmer om kommandot kan köras
        public bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

        // Exekverar kommandots logik
        public void Execute(object? parameter)
        {
            // Tillåt null-argument för kommandon som inte behöver en parameter
            execute(parameter);
        }
    }
}