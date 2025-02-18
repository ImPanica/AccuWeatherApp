using System.Globalization;
using System.Windows.Data;

namespace AccuWeatherApp.ViewModel.ValueConverters;

public class BoolToRainConverter : IValueConverter
{
    private IValueConverter _valueConverterImplementation;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool isRaining = (bool)value;
        return isRaining ? "Осадки" : "Без осадков";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string isRaining = (string)value;
        return isRaining == "Осадки";
    }
}