using System.ComponentModel;
using System.Windows.Controls;

namespace wpf_list_view
{
    public static class DataGridHelper
    {
        public static void ApplyFilter(DataGrid dataGrid, Predicate<object> filter)
        {
            ICollectionView collectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            collectionView.Filter = filter;
        }
    }
}
