using System.Data;
using System.Data.SqlClient;

namespace LoginPanelApplication
{
    public static class SqlManager
    { 
        public static SqlConnection Connection;
        public static SqlDataAdapter dataAdapter;
        public static DataTable dataTable;
        public static SqlDataReader reader;
        public static SqlCommand selectCommand;
        public static SqlCommand updateCommand;

        static SqlManager()
        {
            Connection = new SqlConnection(Properties.Settings.Default.connectionString);
        }
    }
}
