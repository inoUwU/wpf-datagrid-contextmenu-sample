using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace wpf_list_view
{
    public class NullItemToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // コンボボックスのSelectedItemがnullかどうかをチェック
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                // 選択項目がない場合、Gridの可視性をCollapsedに設定
                return Visibility.Collapsed;
            }
            else
            {
                // 選択項目がある場合、Gridの可視性をVisibleに設定
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
