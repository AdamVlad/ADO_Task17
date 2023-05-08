using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Task17.Model;

namespace Task17.ViewModel
{
    /// <summary>
    /// Класс, соединяющий модель и представление
    /// </summary>
    public class MainVM
    {
        // Поля для MSSQL
        public DataTable MSSQLDataTable { get; set; }
        public SqlDataAdapter MSSQLDataAdapter { get; set; }

        // Поля для Access
        public DataTable AccessDataTable { get; set; }
        public OleDbDataAdapter AccessDataAdapter { get; set; }

        // Текущая выделенная запись
        public DataRowView CurrentDataRowView { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainVM()
        {
            //Создаю два потока и в каждом из них инициализирую подключение к базе данных

            Task mssqlTask = Task.Factory.StartNew(delegate
            {
                _mssqlDataBaseManager = new MSSQLDataBaseManager();
                MSSQLDataTable = _mssqlDataBaseManager.MSSQLDataTable;
                MSSQLDataAdapter = _mssqlDataBaseManager.MSSQLDataAdapter;
            });

            Task accessTask = Task.Factory.StartNew(delegate
            {
                _accessDataBaseManager = new AccessDataBaseManager();
                AccessDataTable = _accessDataBaseManager.AccessDataTable;
                AccessDataAdapter = _accessDataBaseManager.AccessDataAdapter;
            });

            // Жду завершения потоков
            Task.WaitAll(mssqlTask, accessTask);
        }

        /// <summary>
        /// Определяет, существует ли переданый Email в таблице покупателей
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>
        /// true - существует
        /// false - не существует
        /// </returns>
        public bool IsEmailExistsInBuyingsDB(string email)
        {
            foreach (var row in MSSQLDataTable.Rows)
            {
                if (((DataRow)row)["Email"].ToString() == email)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Добавляет запись в MSSQL
        /// </summary>
        /// <param name="newRow">Запись</param>
        public void MSSQLAddRow(DataRow newRow)
        {
            MSSQLDataTable.Rows.Add(newRow);
            MSSQLDataAdapter.Update(MSSQLDataTable);
        }

        /// <summary>
        /// Обновляет данные MSSQL
        /// </summary>
        public void MSSQLUpdate()
        {
            MSSQLDataAdapter.Update(MSSQLDataTable);
        }

        /// <summary>
        /// Добавляет запись в Access
        /// </summary>
        /// <param name="newRow">Запись</param>
        public void AccessAddRow(DataRow newRow)
        {
            AccessDataTable.Rows.Add(newRow);
            AccessDataAdapter.Update(AccessDataTable);
        }

        /// <summary>
        /// Обновляет данные Access
        /// </summary>
        public void AccessUpdate()
        {
            AccessDataAdapter.Update(AccessDataTable);
        }

        /// <summary>
        /// Генерирует следующее значение ID
        /// </summary>
        /// <returns>Следующее значение ID</returns>
        public string AccessGetNextId()
        {
            return _accessDataBaseManager.GetNextID();
        }

        private MSSQLDataBaseManager _mssqlDataBaseManager;
        private AccessDataBaseManager _accessDataBaseManager;
    }
}
