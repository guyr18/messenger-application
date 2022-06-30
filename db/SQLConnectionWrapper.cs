using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;

namespace messenger_app.db
{

    public sealed class SQLConnectionWrapper
    {

        private SqlConnection _conn;
        private string _connectionString;

        public bool IsConnected { get => _conn != null;  }

        // Default constructor that accepts a connection string and a boolean variable, autoConnect.
        // If autoConnect is true, the connect method will automatically be invoked.
        public SQLConnectionWrapper(string connectionString, bool autoConnect = false)
        {

            _connectionString = connectionString;

            if(autoConnect)
            {

                this.connect();

            }
        }

        // Connect() attempts to establish a connection to the specified data source, _connectionString. If an error
        // occurs, a SqlException will be thrown.
        public void connect()
        {

            try
            {

                _conn = new SqlConnection(_connectionString);
                _conn.Open();
                Debug.WriteLine("Connection established.");

            }
            catch(SqlException ex)
            {

                Debug.WriteLine(ex.ToString());

            }
        }

        // Connect(dsn) will try to establish a connection to the explicitly specified data source, @param dsn.
        // This is useful if you would like to specify a new data source, post-instantiation; i.e.: promotes reusability.
        public void connect(string dsn)
        {

            try
            {

                _conn = new SqlConnection(dsn);
                _conn.Open();
                _connectionString = dsn;

            }
            catch (SqlException ex)
            {

                Debug.WriteLine(ex.ToString());

            }
        }

        // Close() will terminate the active SqlConnection object, clean any resources, and nullify the object. If this is not
        // applicable, the method will do nothing.
        public void close()
        {

            if(IsConnected)
            {

                _conn.Close();
                _conn = null;

            }
        }

        // SQL operation methods.

        // Insert(q) executes a query q and commits the corresponding changes based on the context of the T-SQL query. It
        // will throw an exception if the query is invalid or unsuccessful.

        public void insert(string q)
        {

            if(IsConnected && q != "" && q != null)
            {

                try
                {

                    SqlCommand command = new SqlCommand(q, _conn);
                    int result = command.ExecuteNonQuery();

                    if(result < 0)
                    {

                        Debug.WriteLine(string.Format("Error occurred when running SQLConnectionWrapper.insert({0})!", q));

                    }
                }
                catch(SqlException ex)
                {

                    Debug.WriteLine(ex.ToString());

                }
            }
        }

        // Query(q) executes a query q and returns a Dictionary<string, object> r. R contains key-value entries
        // that represent the column name and column value for matching relations. It will throw a SqlException
        // for an invalid query parameter q.
        public Dictionary<string, List<object>> query(string q)
        {

            Dictionary<string, List<object>> result = new Dictionary<string, List<object>>();

            if (IsConnected && q != "" && q != null)
            {

                try
                {

                    SqlCommand command = new SqlCommand(q, _conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        int i = 0;

                        foreach(var column in reader.GetColumnSchema())
                        {

                            string columnName = column.ColumnName;

                            if(result.ContainsKey(columnName))
                            {

                                result[columnName].Add(reader.GetValue(i));

                            }
                            else
                            {

                                result[columnName] = new List<object> { reader.GetValue(i) };

                            }

                            i++;

                        }
                    }

                    reader.Close();

                }
                catch(SqlException ex)
                {

                    Debug.WriteLine(string.Format("SQLConnectionWrapper.query({0}) error occurred. Most likely you have a malformed query input!", q));
                    Debug.WriteLine(ex.ToString());

                }
            }
            else
            {

                Debug.WriteLine("This method should only be called after establishing a connection to a valid data source!");

            }

            return result;

        }
    }
}