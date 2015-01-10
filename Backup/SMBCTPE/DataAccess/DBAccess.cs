using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using SMBCTPE.Global;
using SMBCTPE.Helper;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data;
using SMBCTPE.EntityModel;

namespace SMBCTPE.DataAccess
{
    /// <summary>
    /// Database Access object
    /// 
    /// </summary>
    public class DbAccess
    {
        private DbAccess() { }

        private static object sync = new object();

        private static volatile DbAccess instance;

        private DbAccessProvider dbAccessProvider;
        /// <summary>
        /// Return the current database access provider
        /// <para>The type is defined in SMBCTPE.DataAccess.DbAccessProvider</para>
        /// </summary>
        public DbAccessProvider CurrentDbAccessProvider
        {
            get { return dbAccessProvider; }
        }
	
        /// <summary>
        /// Get the DbAccess unique instance
        /// </summary>
        public static DbAccess Instance
	    {
		    get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new DbAccess();
                        }
                    }
                }
                return instance;
            }
	    }

        /// <summary>
        /// Get Database connection, the provider will be choosen automatically by the LoginInfo
        /// <para>
        /// The username, password, and datasource will be filled with the eSS DDE value, i.e., LoginInfo
        /// </para>
        /// </summary>
        /// <returns>Connection object</returns>
        internal DbConnection GetConnection()
        {
            return GetConnection(false);
        }

        /// <summary>
        /// Get Database connection, the provider will be choosen automatically by the LoginInfo unless you force
        /// to use ODBC provider
        /// <para>
        /// The username, password, and datasource will be filled with the eSS DDE value, i.e., LoginInfo
        /// </para>
        /// </summary>
        /// <param name="forceODBC">
        /// <para>false: automatically</para>
        /// <para>true: use ODBC provider</para>
        /// </param>
        /// <returns>Connection object</returns>
        internal DbConnection GetConnection(bool forceODBC)
        {
            try
            {
                DDE.GetUserInfo();
                LoginInfo loginInfo = LoginInfo.Instance;

                switch (loginInfo.DSN.ToUpper())
                {
                    case "SMBCTEST":
                        dbAccessProvider = forceODBC ? DbAccessProvider.ODBC : DbAccessProvider.SqlClient;
                        return GetConnection(dbAccessProvider, "SQL Server", DataSources.OU,
                                             "SA", "smbctest", "General", loginInfo.SeSSName, "");
                    case "SMBCESS":
                        dbAccessProvider = forceODBC ? DbAccessProvider.ODBC : DbAccessProvider.SqlClient;
                        return GetConnection(dbAccessProvider, "SQL Server", DataSources.TWTADB1,
                                             loginInfo.UID, loginInfo.AUTH, "General", loginInfo.SeSSName, "");
                    default:
                        throw new ArgumentException("DSN:" + loginInfo.DSN + " is unknown.", "DSN");
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ExceptionDialog("GetConnection Error: ", ex);
                return null;
            } 
        }

        /// <summary>
        /// Get AS400 Database connection, the provider is fixed to use ODBC only
        /// <para>
        /// The username, password, and datasource will be filled with the eSS DDE value, i.e., LoginInfo
        /// </para>
        /// </summary>
        /// <returns>Connection object</returns>
        internal DbConnection GetConnectionAS400()
        {
            try
            {
                DDE.GetUserInfo();
                LoginInfo loginInfo = LoginInfo.Instance;

                switch (loginInfo.DSN.ToUpper())
                {
                    case "SMBCTEST":
                    case "SMBCESS":
                        dbAccessProvider = DbAccessProvider.ODBC;
                        return GetConnection(dbAccessProvider, "", loginInfo.DSN,
                                             loginInfo.UID, loginInfo.AUTH, "General", loginInfo.SeSSName, 
                                             "UniCodeSQL=1;");
                    default:
                        throw new ArgumentException("DSN:" + loginInfo.DSN + " is unknown.", "DSN");
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ExceptionDialog("GetConnectionAS400 Error: ", ex);
                return null;
            } 
        }

        private DbConnection GetConnection(DbAccessProvider provider, string driver, string ds,
                                           string uid, string pwd, string dbName, string apName,
                                           string misc)
        {
            DbConnection con = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                switch (provider)
                {
                    case DbAccessProvider.ODBC:
                        if (driver.Length == 0)
                        {
                            // DSN method
                            sb.Append("DSN=" + ds + ";UID=" + uid + ";PWD=" + pwd + ";UniCodeSQL=1;");
                        }
                        else
                        {
                            // General
                            sb.Append("Driver={" + driver + "};");
                            sb.Append("Server=" + ds + ";");
                            sb.Append("UID=" + uid + ";");
                            sb.Append("PWD=" + pwd + ";");
                            sb.Append("Database=" + dbName + ";");
                            sb.Append(misc);
                        }
                        con = new OdbcConnection(sb.ToString());
                        break;
                    case DbAccessProvider.SqlClient:
                        sb.Append("Data Source=" + ds + ";");
                        sb.Append("Initial Catalog=" + dbName + ";");
                        sb.Append("User ID=" + uid + ";");
                        sb.Append("Password=" + pwd + ";");
                        sb.Append("Application Name=" + apName + ";");
                        sb.Append(misc);
                        con = new SqlConnection(sb.ToString());
                        break;
                    default:
                        throw new ArgumentException("DbAccessProvider:" + provider + " is unknown.", "provide");
                }

                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                DialogHelper.ExceptionDialog("GetConnection Error:", ex);
                return null;
            }
        }

        /// <summary>
        /// Executes query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="sqlString">query string</param>
        /// <returns>
        /// <para>object: the first column data</para>
        /// <para>null: no data queried</para>
        /// </returns>
        public object ExecuteScalar(string sqlString)
        {
            using (DbConnection con = GetConnection())
            {
                DbCommand cmd = null;
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = sqlString;
                    cmd.Connection = con;
                    object o = cmd.ExecuteScalar();

                    return o;
                }
                catch (Exception ex)
                {
                    DialogHelper.ExceptionDialog("ExecuteScalar error: ", ex);
                    return null;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes a non-query sql command.
        /// <para>If the command is failed, it will use the transaction to rollback to last state.</para>
        /// </summary>
        /// <param name="sqlString">sql string</param>
        /// <returns>The affected rows by this command</returns>
        public int ExecuteNonQuery(string sqlString)
        {
            using (DbConnection con = GetConnection())
            {
                DbCommand cmd = null;
                DbTransaction transaction = con.BeginTransaction();
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = sqlString;
                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                    int affectedCounts = cmd.ExecuteNonQuery();
                    transaction.Commit();

                    return affectedCounts;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    DialogHelper.ExceptionDialog("ExecuteNonQuery error: ", ex);
                    return -1;
                }
                finally
                {
                    transaction.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes query, and returns the Table object
        /// </summary>
        /// <param name="sqlString">query string</param>
        /// <returns>table: the table object. 
        /// <para>Note that the object is returned with empty rows even no records exsist.</para>
        /// </returns>
        public Table ExecuteQuery(string sqlString)
        {
            using (DbConnection con = GetConnection())
            {
                DbCommand cmd = null;
                DbDataReader reader = null;
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = sqlString;
                    cmd.Connection = con;
                    reader = cmd.ExecuteReader();
                    
                    return new Table(reader);
                }
                catch (Exception ex)
                {
                    DialogHelper.ExceptionDialog("ExecuteQuery error: ", ex);
                    return null;
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes query, and returns the Entities model
        /// </summary>
        /// <typeparam name="T">type of the element in entities model</typeparam>
        /// <param name="sqlString">query string</param>
        /// <returns>Entities model</returns>
        public Entities<T> ExecuteQuery<T>(string sqlString)
            where T : Entity
        {
            return ExecuteQuery<T>(sqlString, -1);
        }

        /// <summary>
        /// Executes query, and returns the Entities model with a limit number by maxRowCount
        /// </summary>
        /// <typeparam name="T">type of the element in entities model</typeparam>
        /// <param name="sqlString">query string</param>
        /// <param name="maxRowCount">the limit number</param>
        /// <returns>Entities model</returns>
        public Entities<T> ExecuteQuery<T>(string sqlString, int maxRowCount)
            where T : Entity
        {
            using (DbConnection con = GetConnection())
            {
                DbCommand cmd = null;
                DbDataReader reader = null;
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = sqlString;
                    cmd.Connection = con;
                    reader = cmd.ExecuteReader();

                    Entities<T> entities = new Entities<T>(reader, maxRowCount);
                    return entities;
                }
                catch (Exception ex)
                {
                    DialogHelper.ExceptionDialog("ExecuteQuery error: ", ex);
                    return null;
                }
                finally
                {
                    reader.Close();
                    reader.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        /// <summary>
        /// Execute a stored procedure and return the affected row counts
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>The affected row counts</returns>
        public int ExecuteSPNonQuery(String storedProcedureName, Dictionary<string, object> parameters)
        {
            return (int)ExecuteStoredProcedure(storedProcedureName, parameters, StoredProcedureExecutionType.NonQuery);
        }

        /// <summary>
        /// Execute a stored procedure and return the first column data in first row
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>the data in table[0][0]</returns>
        public object ExecuteSPScalar(String storedProcedureName, Dictionary<string, object> parameters)
        {
            return ExecuteStoredProcedure(storedProcedureName, parameters, StoredProcedureExecutionType.Scalar);
        }

        /// <summary>
        /// Execute a stored procedure and return a data table
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameters">Stored procedure parameters</param>
        /// <returns>the table object</returns>
        public Table ExecuteSPQuery(String storedProcedureName, Dictionary<string, object> parameters)
        {
            return ExecuteStoredProcedure(storedProcedureName, parameters, StoredProcedureExecutionType.Table) as Table;
        }

        private Object ExecuteStoredProcedure(String storedProcedureName, Dictionary<string, object> parameters, StoredProcedureExecutionType spType)
        {
            using (DbConnection con = GetConnection())
            {
                DbCommand cmd = null;
                DbTransaction transaction = con.BeginTransaction();
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = storedProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Transaction = transaction;

                    foreach (string parameterName in parameters.Keys)
                    {
                        DbParameter parameter = cmd.CreateParameter();
                        Type type = parameters[parameterName].GetType();
                        string exception = String.Format("The parameter data type of {0} is invalid.", type.Name);

                        switch (type.FullName)
                        {
                            case "System.Boolean":
                                //parameter.SqlDbType = SqlDbType.Bit;
                                parameter.DbType = DbType.Boolean;
                                break;
                            case "System.Byte":
                            case "System.SByte":
                                //parameter.SqlDbType = SqlDbType.TinyInt;
                                parameter.DbType = DbType.SByte;
                                break;
                            case "System.Int16":
                            case "System.UInt16":
                                //parameter.SqlDbType = SqlDbType.SmallInt;
                                parameter.DbType = DbType.Int16;
                                break;
                            case "System.Int32":
                            case "System.UInt32":
                                //parameter.SqlDbType = SqlDbType.Int;
                                parameter.DbType = DbType.Int32;
                                break;
                            case "System.Int64":
                            case "System.UInt64":
                                //parameter.SqlDbType = SqlDbType.BigInt;
                                parameter.DbType = DbType.Int64;
                                break;
                            case "System.Decimal":
                                //parameter.SqlDbType = SqlDbType.Decimal;
                                parameter.DbType = DbType.Decimal;
                                break;
                            case "System.Double":
                                //parameter.SqlDbType = SqlDbType.Float;
                                parameter.DbType = DbType.Double;
                                break;
                            case "System.Single":
                                //parameter.SqlDbType = SqlDbType.Real;
                                parameter.DbType = DbType.Double;
                                break;
                            case "System.Byte[]":
                                //parameter.SqlDbType = SqlDbType.VarBinary;
                                parameter.DbType = DbType.Binary;
                                break;
                            case "System.Char[]":
                            case "System.String":
                                //parameter.SqlDbType = SqlDbType.NVarChar;
                                parameter.DbType = DbType.String;
                                break;
                            case "System.DateTime":
                                //parameter.SqlDbType = SqlDbType.DateTime;
                                parameter.DbType = DbType.DateTime;
                                break;
                            case "System.Guid":
                                //parameter.SqlDbType = SqlDbType.UniqueIdentifier;
                                parameter.DbType = DbType.Guid;
                                break;
                            case "System.Xml":
                                //parameter.SqlDbType = SqlDbType.Xml;
                                parameter.DbType = DbType.Xml;
                                break;
                            case "System.Object":
                                //parameter.SqlDbType = SqlDbType.Variant;
                                parameter.DbType = DbType.Object;
                                break;
                            default:
                                throw new ArgumentException(exception);
                        }

                        parameter.ParameterName = parameterName;
                        parameter.Value = parameters[parameterName];
                        cmd.Parameters.Add(parameter);
                    }

                    object o = null;

                    switch (spType)
                    {
                        case StoredProcedureExecutionType.NonQuery:
                            o = cmd.ExecuteNonQuery();
                            break;
                        case StoredProcedureExecutionType.Scalar:
                            o = cmd.ExecuteScalar();
                            break;
                        case StoredProcedureExecutionType.Table:
                            DbDataReader reader = cmd.ExecuteReader();
                            try
                            {
                                Table table = new Table(reader);
                                o = table;
                            }
                            catch (Exception) { }
                            finally
                            {
                                reader.Close();
                                reader.Dispose();
                            }
                            break;
                    }

                    transaction.Commit();

                    return o;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    DialogHelper.ExceptionDialog("ExecuteScalar error: ", ex);
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }

    /// <summary>
    /// Enum providers for database access
    /// </summary>
    public enum DbAccessProvider
    {
        /// <summary>
        /// ODBC - use System.Data.Odbc
        /// </summary>
        ODBC,
        /// <summary>
        /// SqlClient - use System.Data.SqlClient
        /// </summary>
        SqlClient
    }

    internal enum StoredProcedureExecutionType
    {
        NonQuery,
        Table,
        Scalar
    }

    /// <summary>
    /// Datasources (Server name)
    /// </summary>
    public class DataSources
    {
        /// <summary>
        /// ou, Test server
        /// </summary>
        public static string OU = "ou";
        /// <summary>
        /// TWTADB1, Official TPE branch server
        /// </summary>
        public static string TWTADB1 = "TWTADB1";
    }

    /// <summary>
    /// Table cell class, key is the cell name (field name), and value is its data in any types
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Cell name, i.e. the SQL data field
        /// </summary>
        public string Key;
        /// <summary>
        /// the field value
        /// </summary>
        public object Value;

        /// <summary>
        /// Constructor of a table cell
        /// </summary>
        /// <param name="k">Key</param>
        /// <param name="v">Value</param>
        public TableCell(string k, object v)
        {
            Key = k;
            Value = v;
        }
    }

    /// <summary>
    /// Table row class, it consists of several table cell objects
    /// </summary>
    public class TableRow : System.Collections.IEnumerable
    {
        private List<TableCell> row;

        private readonly int columnCount;

        /// <summary>
        /// Get the column count
        /// </summary>
        public int ColumnCount
        {
            get { return columnCount; }
        }

        internal TableRow(int ColumnCount, List<TableCell> ColumnsHeader)
        {
            columnCount = ColumnCount;
            row = new List<TableCell>(columnCount);
        }

        /// <summary>
        /// Add cell item
        /// </summary>
        /// <param name="cell">Table cell item</param>
        /// <returns>true if success</returns>
        internal bool AddCell(TableCell cell)
        {
            row.Add(cell);
            return true;
        }

        /// <summary>
        /// Get the table cell of index i
        /// </summary>
        /// <param name="i">cell index, 0 to n-1</param>
        /// <returns>table cell object</returns>
        public TableCell this[int i]
        {
            get 
            {
                if ((uint)i >= (uint)row.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return row[i];
            }
        }

        /// <summary>
        /// Get the table cell with field name is equal to k
        /// </summary>
        /// <param name="k">cell name (field name)</param>
        /// <returns>table cell object</returns>
        public TableCell this[string k]
        {
            get
            {
                TableCell cell = row.Find(delegate(TableCell c) { return c.Key == k; });
                if (cell == null)
                    throw new IndexOutOfRangeException("The key: " + k + " is not found!");
                else
                    return cell;
            }
        }

        #region IEnumerable 成員

        /// <summary>
        /// Get each TableCell from this row
        /// </summary>
        /// <returns>the TableCell enumerator</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return row.GetEnumerator();
        }

        #endregion
    }

    /// <summary>
    /// Table class, it consists of several table row objects
    /// </summary>
    public class Table : System.Collections.IEnumerable
    {
        private List<TableRow> table;
        private List<TableCell> columnsHeader;

        internal Table(IDataReader reader)
        {
            table = new List<TableRow>();
            columnsHeader = new List<TableCell>();
            Fill(reader);
        }

        /// <summary>
        /// Get the table row object of index i
        /// </summary>
        /// <param name="i">the row index</param>
        /// <returns>the table row object</returns>
        public TableRow this[int i]
        {
            get
            {
                if ((uint)i >= (uint)table.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return table[i];
            }
        }

        /// <summary>
        /// Get a list of TableCell with the column names and types
        /// </summary>
        public List<TableCell> ColumnsHeader
        {
            get
            {
                return columnsHeader;
            }
        }

        /// <summary>
        /// Create a new row by the schema of this table
        /// <para>
        /// Note that the new row will be added into table instantly once the function is called.
        /// </para>
        /// </summary>
        /// <returns>the reference to the new row object
        /// <para>
        /// if the column headers weren't defined yet, the return value is null.
        /// </para>
        /// </returns>
        internal TableRow NewTableRow()
        {
            if (ColumnCount != 0)
            {
                TableRow row = new TableRow(ColumnCount, columnsHeader);
                table.Add(row);
                return row;
            }
            else
                return null;
        }

        private void Fill(IDataReader reader)
        {
            table.Clear();
            columnsHeader.Clear();
            // columns: name
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i).ToString();
                Type type = reader.GetFieldType(i);
                columnsHeader.Add(new TableCell(name, type));
            }
            // rows: data
            while (reader.Read())
            {
                TableRow row = NewTableRow();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row.AddCell(new TableCell(reader.GetName(i), reader.GetValue(i)));
                }
            }
        }

        /// <summary>
        /// Get the column count
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return columnsHeader.Count;
            }
        }

        /// <summary>
        /// Get the row count
        /// </summary>
        public int RowCount
        {
            get
            {
                return table.Count;
            }
        }

        #region IEnumerable 成員

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return table.GetEnumerator();
        }

        #endregion
    }
}
