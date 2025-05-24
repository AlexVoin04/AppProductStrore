﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AppProductStrore.Helpers
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool b && !b) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is Visibility v && v != Visibility.Visible);
        }
    }
}
