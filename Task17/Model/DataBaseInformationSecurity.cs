using System.Collections.Generic;
using System.Linq;

namespace Task17.Model
{
    /// <summary>
    /// Класс, предоставляющий функции безопасности базы данных
    /// </summary>
    public static class DataBaseInformationSecurity
    {
        /// <summary>
        /// Примеры инъекций
        /// </summary>
        private static List<string> _injections;

        static DataBaseInformationSecurity()
        {
            _injections = new List<string>()
            {
                ";",
                "--",
                "DROP ",
                "UPDATE ",
                "CREATE ",
                "INSERT "
            };
        }

        /// <summary>
        /// Определяет, содержит ли запрос SQL-инъекцию
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>
        /// true - инъекция обнаружена
        /// false - инъекция не обнаружена
        /// </returns>
        public static bool IsSQLInjection(string query)
        {
            return _injections.Any(injection => query.Contains(injection));
        }
    }
}
