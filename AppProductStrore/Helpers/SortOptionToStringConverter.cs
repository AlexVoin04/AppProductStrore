using AppProductStrore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppProductStrore.Helpers
{
    public class SortOptionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is CartSortOption option)
            {
                switch (option)
                {
                    case CartSortOption.Default:
                        return "По умолчанию";
                    case CartSortOption.ByName:
                        return "По названию";
                    case CartSortOption.ByPrice:
                        return "По цене";
                    default:
                        return "Неизвестно";
                }
            }
            return "Неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Не используется, если только не будет редактирования текстом
            throw new NotImplementedException();
        }
    }
}
