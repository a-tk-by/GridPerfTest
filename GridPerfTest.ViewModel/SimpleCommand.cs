using System.Windows.Input;

namespace GridPerfTest.ViewModel;

#pragma warning disable CS0067

public sealed class SimpleCommand : ICommand
{
    private readonly Action _execute;

    public SimpleCommand(Action execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public event EventHandler? CanExecuteChanged;
}