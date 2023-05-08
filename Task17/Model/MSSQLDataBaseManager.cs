using System;
using System.Data;
using System.Data.SqlClient;

namespace Task17.Model
{
    /// <summary>
    /// Класс, предназначенный для подключения и инициализации базы данных MSSQL
    /// </summary>
    public class MSSQLDataBaseManager
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MSSQLDataBaseManager()
        {
            // Инициализирую строку подключения
            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"E:\Courses\Aplication\Task17\Task17\DataBases\MSSQLBuyersDB.mdf",
                IntegratedSecurity = false
            };

            // Инициализирую поля и команды
            InitializeFields();
            InitializeSelectCommand();
            InitializeInsertCommand();
            InitializeUpdateCommand();
            InitializeDeleteCommand();

            // Заполняю таблицу
            MSSQLDataAdapter.Fill(MSSQLDataTable);
        }

        /// <summary>
        /// Таблица, содержащая данные о записях
        /// </summary>
        public DataTable MSSQLDataTable { get; set; }

        /// <summary>
        /// Адаптер базы данных
        /// </summary>
        public SqlDataAdapter MSSQLDataAdapter { get; set; }

        /// <summary>
        /// Инициализация полей
        /// </summary>
        private void InitializeFields()
        {
            _sqlConnection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            MSSQLDataTable = new DataTable();
            MSSQLDataAdapter = new SqlDataAdapter();
        }

        /// <summary>
        /// Инициализация запроса SELECT
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeSelectCommand()
        {
            if (_sqlConnection == null || MSSQLDataAdapter == null) throw new NullReferenceException();

            var command = @"SELECT * FROM Buyings ORDER BY Buyings.ID";
            MSSQLDataAdapter.SelectCommand = new SqlCommand(command, _sqlConnection);
        }

        /// <summary>
        /// Инициализация запроса INSERT
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeInsertCommand()
        {
            if (_sqlConnection == null || MSSQLDataAdapter == null) throw new NullReferenceException();

            var command = @"INSERT INTO Buyings (FirstName, MiddleName, LastName, PhoneNumber, Email)
                                   VALUES (@FirstName, @MiddleName, @LastName, @PhoneNumber, @Email);
                        SET @ID = @@IDENTITY";

            MSSQLDataAdapter.InsertCommand = new SqlCommand(command, _sqlConnection);

            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@ID", SqlDbType.Int, 4, "ID").Direction = ParameterDirection.Output;
            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@LastName", SqlDbType.NVarChar, 20, "LastName");
            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@PhoneNumber", SqlDbType.NVarChar, 30, "PhoneNumber");
            MSSQLDataAdapter.InsertCommand.Parameters.Add(
                "@Email", SqlDbType.NVarChar, 30, "Email");
        }

        /// <summary>
        /// Инициализация запроса UPDATE
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeUpdateCommand()
        {
            if (_sqlConnection == null || MSSQLDataAdapter == null) throw new NullReferenceException();

            var command = @"UPDATE Buyings SET 
                                   FirstName = @FirstName, 
                                   MiddleName = @MiddleName, 
                                   LastName = @LastName,
                                   PhoneNumber = @PhoneNumber,
                                   Email = @Email
                        WHERE ID = @ID";

            MSSQLDataAdapter.UpdateCommand = new SqlCommand(command, _sqlConnection);

            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                    "@ID", SqlDbType.Int, 0, "ID").SourceVersion = DataRowVersion.Original;
            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                "@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                "@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                "@LastName", SqlDbType.NVarChar, 20, "LastName");
            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                "@PhoneNumber", SqlDbType.NVarChar, 30, "PhoneNumber");
            MSSQLDataAdapter.UpdateCommand.Parameters.Add(
                "@Email", SqlDbType.NVarChar, 30, "Email");
        }

        /// <summary>
        /// Инициализация запроса DELETE
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        private void InitializeDeleteCommand()
        {
            if (_sqlConnection == null || MSSQLDataAdapter == null) throw new NullReferenceException();

            var command = @"DELETE FROM Buyings WHERE ID = @ID";

            MSSQLDataAdapter.DeleteCommand = new SqlCommand(command, _sqlConnection);
            MSSQLDataAdapter.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID");
        }


        private SqlConnection _sqlConnection;

        private SqlConnectionStringBuilder _connectionStringBuilder;

    }
}
