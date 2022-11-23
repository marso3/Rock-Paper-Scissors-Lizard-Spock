using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RockPaperScissorsLizardSpock.Model.Infrastructure
{
    public class StateToConfigurationVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameState state = (GameState)value;
            result = state == GameState.Configuration ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
    public class StateToPlayingVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameState state = (GameState)value;
            result = state == GameState.Playing ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
    public class StateToShowingHandsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameState state = (GameState)value;
            result = state == GameState.ShowingHands ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
    public class StateToPlayingORShowingHandsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameState state = (GameState)value;
            result = (state == GameState.ShowingHands) || (state == GameState.Playing) ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
    public class StateToFinalVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameState state = (GameState)value;
            result = state == GameState.Final ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }

    public class StateToSinglePlayerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameMode mode = (GameMode)value;
            result = mode == GameMode.SinglePlayer ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
    public class StateToMutiPlayerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result;
            GameMode mode = (GameMode)value;
            result = mode == GameMode.Multiplayer ?
                Visibility.Visible
                : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Boolean && (bool)value)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility && (Visibility)value == Visibility.Visible)
            {
                return true;
            }
            return false;
        }
    }

    public class OptionToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Option option = (Option)value;
            BitmapImage result = new();
            result.BeginInit();
            if (option == Option.Rock)
            {
                result.UriSource = new Uri("/Images/rockIcon.png", UriKind.Relative);
            }
            else if (option == Option.Paper)
            {
                result.UriSource = new Uri("/Images/paperIcon.png", UriKind.Relative);
            }
            else if (option == Option.Scissors)
            {
                result.UriSource = new Uri("/Images/scissorsIcon.png", UriKind.Relative);
            }
            else if (option == Option.Lizard)
            {
                result.UriSource = new Uri("/Images/lizardIcon.png", UriKind.Relative);
            }
            else if (option == Option.Spock)
            {
                result.UriSource = new Uri("/Images/spockIcon.png", UriKind.Relative);
            }
            else
            {
                return null;
            }
            result.EndInit();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
}
