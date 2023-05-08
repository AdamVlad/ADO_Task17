using System;
using System.Data;
using System.Data.OleDb;

namespace Task17.Model
{
    /// <summary>
    /// Класс, предназначенный для подключения и инициализации базы данных Access
    /// </summary>
    public class AccessDataBaseManager
    { 
        /// <summary>
        /// Конструктор
        /// </summary>
        public AccessDataBaseManager()
        {
            // Инициализирую строку подключения
            _connectionStringBuilder = new OleDbConnectionStringBuilder()
            {
                DataSource = @"E:\Courses\Aplication\Task17\Task17\DataBases\AccessShopDB.accdb",
                Provider = @"Microsoft.ACE.OLEDB.12.0",
                PersistSecurityInfo = true
            };

            // Инициализирую поля и команды
            InitializeFields();
            InitializeSelectCommand();
            InitializeInsertCommand();
            InitializeUpdateCommand();
            InitializeDeleteCommand();

            // Заполняю таблицу
            AccessDataAdapter.Fill(AccessDataTable);
        }

        /// <summary>
        /// Таблица, содержащая данные о записях
        /// </summary>
        public DataTable AccessDataTable { get; set; }

        /// <summary>
        /// Адаптер базы данных
        /// </summary>
        public OleDbDataAdapter AccessDataAdapter { get; set; }

        /// <summary>
        /// Инициализация полей
        /// </summary>
        private void InitializeFields()
        {
            _accessConnection = new OleDbConnection(_connectionStringBuilder.ConnectionString);
            AccessDataTable = new DataTable();
            AccessDataAdapter = new OleDbDataAdapter();
        }

        /// <summary>
        /// Инициализация запроса SELECT
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeSelectCommand()
        {
            if (_accessConnection == null || AccessDataAdapter == null) throw new NullReferenceException();

            var command = @"SELECT * FROM Orders ORDER BY Orders.ID";
            AccessDataAdapter.SelectCommand = new OleDbCommand(command, _accessConnection);
        }

        /// <summary>
        /// Инициализация запроса INSERT
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeInsertCommand()
        {
            if (_accessConnection == null || AccessDataAdapter == null) throw new NullReferenceException();

            var command = @"INSERT INTO Orders (ID, Email, ProductCode, ProductName)
                                   VALUES (@ID, @Email, @ProductCode, @ProductName)";

            AccessDataAdapter.InsertCommand = new OleDbCommand(command, _accessConnection);

            AccessDataAdapter.InsertCommand.Parameters.Add(
                "@ID", OleDbType.Integer, 20, "ID");
            AccessDataAdapter.InsertCommand.Parameters.Add(
                "@Email", OleDbType.Char, 20, "Email");
            AccessDataAdapter.InsertCommand.Parameters.Add(
                "@ProductCode", OleDbType.Integer, 4, "ProductCode");
            AccessDataAdapter.InsertCommand.Parameters.Add(
                "@ProductName", OleDbType.Char, 20, "ProductName");
        }

        /// <summary>
        /// Инициализация запроса UPDATE
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeUpdateCommand()
        {
            if (_accessConnection == null || AccessDataAdapter == null) throw new NullReferenceException();

            var command = @"UPDATE Orders SET
                                   ID = @ID,
                                   Email = @Email, 
                                   ProductCode = @ProductCode, 
                                   ProductName = @ProductName
                                   WHERE ID = @ID";

            AccessDataAdapter.UpdateCommand = new OleDbCommand(command, _accessConnection);

            AccessDataAdapter.UpdateCommand.Parameters.Add(
                    "@ID", OleDbType.Integer, 20, "ID").SourceVersion = DataRowVersion.Original;
            AccessDataAdapter.UpdateCommand.Parameters.Add(
                "@Email", OleDbType.Char, 20, "Email");
            AccessDataAdapter.UpdateCommand.Parameters.Add(
                "@ProductCode", OleDbType.Integer, 4, "ProductCode");
            AccessDataAdapter.UpdateCommand.Parameters.Add(
                "@ProductName", OleDbType.Char, 20, "ProductName");
        }

        /// <summary>
        /// Инициализация запроса DELETE
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeDeleteCommand()
        {
            if (_accessConnection == null || AccessDataAdapter == null) throw new NullReferenceException();

            var command = @"DELETE FROM Orders WHERE ID = @ID";

            AccessDataAdapter.DeleteCommand = new OleDbCommand(command, _accessConnection);
            AccessDataAdapter.DeleteCommand.Parameters.Add("@ID", OleDbType.Integer, 20, "ID");
        }

        /// <summary>
        /// Генерация следующего ID
        /// </summary>
        /// <returns>Следующий ID</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public string GetNextID()
        {
            if (_accessConnection == null || AccessDataAdapter == null) throw new NullReferenceException();

            // Создаю запрос
            var query = @"SELECT MAX(ID) + 1 FROM Orders";
            OleDbCommand command = new OleDbCommand(query, _accessConnection);

            string result = "";

            try
            {
                // Открываю подключение и выполняю запрос
                _accessConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = reader[0].ToString();
                }

                reader.Close();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _accessConnection.Close();
            }
        }

        private OleDbConnection _accessConnection;

        private OleDbConnectionStringBuilder _connectionStringBuilder;
    }
}
