using System.Windows.Input;

namespace AccuWeatherApp.ViewModel.Commands;

public class SearchCommand : ICommand
{
    private WeatherVM _weatherVM;

    public SearchCommand(WeatherVM weatherVM)
    {
        _weatherVM = weatherVM;
    }
    public bool CanExecute(object? parameter)
    {
        string query = parameter as string;
        if (string.IsNullOrWhiteSpace(query))
            return false;
        
        return true;
    }

    public void Execute(object? parameter)
    {
        _weatherVM.MakeQuery();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}