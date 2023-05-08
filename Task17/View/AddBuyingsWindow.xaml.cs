using System.Data;
using System.Windows;

using Task17.Model;

namespace Task17.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrdersWindow.xaml
    /// </summary>
    public partial class AddBuyingsWindow : Window
    {
        /// <summary>
        /// Констрктор
        /// </summary>
        private AddBuyingsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="row">Запись</param>
        public AddBuyingsWindow(DataRow row) : this()
        {
            CancelButton.Click += delegate { DialogResult = false; };
            AddButton.Click += delegate
            {
                // Проверка на наличие инъекции
                if (DataBaseInformationSecurity.IsSQLInjection(FirstNameTextBox.Text) ||
                    DataBaseInformationSecurity.IsSQLInjection(MiddleNameTextBox.Text) ||
                    DataBaseInformationSecurity.IsSQLInjection(LastNameTextBox.Text) ||
                    DataBaseInformationSecurity.IsSQLInjection(PhoneNumberTextBox.Text) ||
                    DataBaseInformationSecurity.IsSQLInjection(EmailTextBox.Text))
                {
                    DialogResult = false;
                    MessageBox.Show("Обнаружена SQL-инъекция");
                    return;
                }

                // Если все хорошо, то инициализирую запись
                row["FirstName"] = FirstNameTextBox.Text;
                row["MiddleName"] = MiddleNameTextBox.Text;
                row["LastName"] = LastNameTextBox.Text;
                row["PhoneNumber"] = PhoneNumberTextBox.Text;
                row["Email"] = EmailTextBox.Text;

                DialogResult = true;
            };
        }
    }
}
