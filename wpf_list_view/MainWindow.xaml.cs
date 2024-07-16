using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace wpf_list_view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ListValue> ListValues { get; private set; } = [];

        public ObservableCollection<string> PayTypes { get; private set; } =
            new ObservableCollection<string>(EnumHelper.GetEnumDisplayNames<PayType>());

        public MainWindow()
        {
            InitializeComponent();
            ListValues.Add(new ListValue()
            {
                Name = "山田 太郎",
                Age = "30",
                Address = "東京都新宿区",
                PayType = PayType.Cache,
                IsDeletable = true
            });
            ListValues.Add(new ListValue()
            {
                Name = "佐藤 花子",
                Age = "25",
                Address = "神奈川県横浜市",
                PayType = PayType.Card,
                IsDeletable = false
            });
            ListValues.Add(new ListValue()
            {
                Name = "田中 一郎",
                Age = "40",
                Address = "大阪府大阪市",
                PayType = PayType.Card,
                IsDeletable = true
            });
            ListValues.Add(new ListValue()
            {
                Name = "伊藤 次郎",
                Age = "55",
                Address = "大阪府大阪市",
                PayType = PayType.Cache,
                IsDeletable = false
            });

            this.DataContext = this;
        }

        [RelayCommand]
        public void View(object sender)
        {
            if (sender is ListValue listValue)
            {
                MessageBox.Show($"You selected: {listValue.Name}");
            }
        }

        [RelayCommand]
        public void Delete(object sender)
        {
            if (sender is DataGridRow row)
            {
                if (row.DataContext is ListValue listValue)
                {
                    MessageBox.Show($"You selected: {listValue.Name}");
                }
            }
        }

        private void ClearItem(object sender, RoutedEventArgs e)
        {
            cbox.SelectedValue = null;
            DataGridHelper.ApplyFilter(MyDataGrid, item =>
            {
                return true;
            });
            ClearButton.Visibility = Visibility.Hidden;
        }

        private void Cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedIndex == null) return;
            PayType enmVal = (PayType)Enum.ToObject(typeof(PayType), comboBox.SelectedIndex);

            DataGridHelper.ApplyFilter(MyDataGrid, item =>
            {
                return item is not ListValue listValue || listValue.PayType == enmVal;
            });
            ClearButton.Visibility = Visibility.Visible;
        }
    }

    public enum PayType
    {
        [Display(Name = "カード")]
        Card,
        [Display(Name = "キャッシュ")]
        Cache
    }

    public class ListValue : ObservableObject
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
        public PayType PayType { get; set; }
        public bool IsDeletable { get; set; }
    }
}