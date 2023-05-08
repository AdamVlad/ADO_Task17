using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

using Task17.ViewModel;

namespace Task17.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            _mainVM = new MainVM();
            BuyingsDataGrid.ItemsSource = _mainVM.MSSQLDataTable.DefaultView;
            OrdersDataGrid.ItemsSource = _mainVM.AccessDataTable.DefaultView;
        }

        /// <summary>
        /// Обработчик клика по кнопке добавления записи в таблицу Buyings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingsMenuItemAddClick(object sender, RoutedEventArgs e)
        {
            // Создаю новую запись и вызываю окно добавление записи
            DataRow dataRow = _mainVM.MSSQLDataTable.NewRow();
            AddBuyingsWindow addBuyingsWindow = new AddBuyingsWindow(dataRow);
            addBuyingsWindow.ShowDialog();

            // Если в результате добавления записи все успешно, то создаю ее и обновляю таблицу
            if (addBuyingsWindow.DialogResult.Value)
            {
                _mainVM.MSSQLAddRow(dataRow);
                _mainVM.MSSQLUpdate();
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке удаления записи в таблице Buyings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingsMenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            _mainVM.CurrentDataRowView = (DataRowView)BuyingsDataGrid.SelectedItem;
            _mainVM.CurrentDataRowView.Row.Delete();
            _mainVM.MSSQLUpdate();

        }

        /// <summary>
        /// Обработчик выбора новой записи в таблице Buyings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingsCurrentCellChanged(object sender, EventArgs e)
        {
            if (_mainVM.CurrentDataRowView == null) return;
            _mainVM.CurrentDataRowView.EndEdit();
            _mainVM.MSSQLUpdate();
        }

        /// <summary>
        /// Обработчик завершения редактирования записи в таблице Buyings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuyingsCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _mainVM.CurrentDataRowView = (DataRowView)BuyingsDataGrid.SelectedItem;
            _mainVM.CurrentDataRowView.BeginEdit();
        }

        /// <summary>
        /// Обработчик клика по кнопке удаления записи в таблице Orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersMenuItemAddClick(object sender, RoutedEventArgs e)
        {
            // Создаю новую запись и вызываю окно добавление записи
            DataRow dataRow = _mainVM.AccessDataTable.NewRow();
            AddOrdersWindow addOrdersWindow = new AddOrdersWindow(
                dataRow,
                _mainVM.AccessGetNextId,
                _mainVM.IsEmailExistsInBuyingsDB);

            addOrdersWindow.ShowDialog();

            // Если в результате добавления записи все успешно, то создаю ее и обновляю таблицу
            if (addOrdersWindow.DialogResult.Value)
            {
                _mainVM.AccessAddRow(dataRow);
                _mainVM.AccessUpdate();
            }
        }

        /// <summary>
        /// Обработчик клика по кнопке удаления записи в таблице Orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersMenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            _mainVM.CurrentDataRowView = (DataRowView)OrdersDataGrid.SelectedItem;
            _mainVM.CurrentDataRowView.Row.Delete();
            _mainVM.AccessUpdate();
        }

        /// <summary>
        /// Обработчик выбора новой записи в таблице Orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersCurrentCellChanged(object sender, EventArgs e)
        {
            if (_mainVM.CurrentDataRowView == null) return;
            _mainVM.CurrentDataRowView.EndEdit();
            _mainVM.AccessUpdate();
        }

        /// <summary>
        /// Обработчик завершения редактирования записи в таблице Orders 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _mainVM.CurrentDataRowView = (DataRowView)OrdersDataGrid.SelectedItem;
            _mainVM.CurrentDataRowView.BeginEdit();
        }

        private MainVM _mainVM;
    }
}
