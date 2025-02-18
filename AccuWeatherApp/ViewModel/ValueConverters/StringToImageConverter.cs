using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AccuWeatherApp.ViewModel.ValueConverters;

public class StringToImageConverter : IValueConverter
{
    private IValueConverter _valueConverterImplementation;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string url && !string.IsNullOrWhiteSpace(url)) //  && !url.Contains("00")
        {
            return new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
        }
        return "https://developer.accuweather.com/sites/default/files/01-s.png";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}