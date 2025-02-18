using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AccuWeatherApp.Model;
using AccuWeatherApp.ViewModel.Commands;
using AccuWeatherApp.ViewModel.Http;

namespace AccuWeatherApp.ViewModel;

public class WeatherVM : INotifyPropertyChanged
{
    private const string ICON_ENDPOINT = "https://developer.accuweather.com/sites/default/files/{0:D2}-s.png";
    
    #region Properties

    private string _query { get; set; }

    public string Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged("Query");
        }
    }

    private CurrentConditions? _currentConditions;

    public CurrentConditions? CurrentConditions
    {
        get => _currentConditions;
        set
        {
            _currentConditions = value;
            OnPropertyChanged("CurrentConditions");
            // Обновляем WeatherIconCode при изменении CurrentConditions
            if (_currentConditions != null)
            {
                WeatherIconCode = _currentConditions.WeatherIcon;
            }
        }
    }

    private City _selectedCity;

    public City SelectedCity
    {
        get => _selectedCity;
        set
        {
            if (_selectedCity != value)
            {
                _selectedCity = value;
                OnPropertyChanged("SelectedCity");
                if (_selectedCity != null)
                {
                    GetCurrentConditions();
                }
            }
        }
    }

    private int _weatherIconCode;

    public int WeatherIconCode
    {
        get => _weatherIconCode;
        set
        {
            _weatherIconCode = value;
            OnPropertyChanged(nameof(WeatherIconCode));
            OnPropertyChanged(nameof(IconUrl));
        }
    }
    
    // Вычисляемый URL иконки
    public string IconUrl => string.Format(ICON_ENDPOINT, WeatherIconCode);

    public ObservableCollection<City> Cities { get; set; }

    #endregion

    #region Commands

    public SearchCommand SearchCommand { get; set; }

    #endregion

    public WeatherVM()
    {
        SearchCommand = new SearchCommand(this);
        Cities = new ObservableCollection<City>();
    }

    #region Methods

    private async void GetCurrentConditions()
    {
        if(SelectedCity == null)
            return;
        
        // Сохраняем ключ выбранного города перед очисткой коллекции
        string? selectedCityKey = SelectedCity?.Key;
        
        Query = String.Empty;
        CurrentConditions = await AccuWeatherClient.GetCurrentConditions(selectedCityKey);
    }

    public async void MakeQuery()
    {
        var cities = await AccuWeatherClient.GetCities(Query);
        Cities.Clear();
        cities.ForEach(city => Cities.Add(city));
    }

    #endregion

    #region Events

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}