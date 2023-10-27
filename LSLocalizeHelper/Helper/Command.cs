using System;
using System.Windows.Input;

namespace LSLocalizeHelper.Helper;

public class AnotherCommandImplementation : ICommand
{
  private readonly Action<object?> execute;
  private readonly Func<object?, bool> canExecute;

  public AnotherCommandImplementation(Action<object?> execute)
    : this(execute, null)
  { }

  public AnotherCommandImplementation(Action<object?> execute, Func<object?, bool>? canExecute)
  {
    if (execute is null) throw new ArgumentNullException(nameof(execute));

    this.execute = execute;
    this.canExecute = canExecute ?? (x => true);
  }

  public bool CanExecute(object? parameter) => this.canExecute(parameter);

  public void Execute(object? parameter) => this.execute(parameter);

  public event EventHandler? CanExecuteChanged
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

  public void Refresh() => CommandManager.InvalidateRequerySuggested();
}
