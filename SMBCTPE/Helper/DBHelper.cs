using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using DbEntityHelper.EntityModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using DbEntityHelper.DataAccess;
using DbEntityHelper.Global;

namespace DbEntityHelper.Helper
{
    /// <summary>
    /// A helper class with several static functions for processing datareader, datatable, and entities
    /// </summary>
    public class DBHelper
    {
        internal static T DataReaderMapping<T>(IDataReader dr) where T : class
        {
            PropertyInfo[] ps = typeof(T).GetProperties();
            T obj = Activator.CreateInstance(typeof(T)) as T;
            int idx = 0;
            foreach (PropertyInfo p in ps)
            {
                try
                {
                    p.SetValue(obj, ReturnValue(dr[p.Name]), null);
                    idx++;
                }
                catch (IndexOutOfRangeException idxOutEx)
                {
                    try
                    {
                        p.SetValue(obj, ReturnValue(dr[idx++]), null);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        DialogHelper.ExceptionDialog("Property: ", idxOutEx);
                        throw idxOutEx;
                    }
                }
                catch (InvalidOperationException invalidOpEx)
                {
                    DialogHelper.ExceptionDialog(invalidOpEx);
                    throw invalidOpEx;
                }
            }
            return obj;
        }

        private static object ReturnValue(object data)
        {
            //      Data Type Table
            //  -------------------------------------
            //   C# Type        SQL Type
            //  -------------------------------------
            //  System.Int64	bigint              *
            //  System.Byte[]	binray              *
            //  System.Boolean	bit                 *
            //  System.String	char                *
            //  System.DateTime	datetime            *
            //  System.Decimal	decimal             *
            //  System.Double	float               *
            //  System.Byte[]	image               *
            //  System.Int32	int                 *
            //  System.Decimal	money               *
            //  System.String	nchar               *
            //  System.String	ntext               *
            //  System.Decimal	numeric             *
            //  System.String	nvarchar            *
            //  System.Single	real                *
            //  System.DateTime	smalldatetime       *
            //  System.Int16	smallint            *
            //  System.Decimal	smallmoney          *
            //  System.Object	sql_variant         *
            //  System.String	text                *
            //  System.Byte[]	timestamp           *
            //  System.Byte	    tinyint             *
            //  System.Guid	    uniqueidentifier    *
            //  System.Byte[]	varbinary           *
            //  System.String	varchar             *
            //  System.String	xml                 *
            //  -------------------------------------
            if (DBNull.Value == data) return null;

            string typename = data.GetType().Name.ToLower();
            if (typename.Contains("string"))
            {
                return data.ToString();
            }
            else if (typename.Contains("int"))
            {
                return int.Parse(data.ToString());
            }
            else if (typename.Contains("datetime"))
            {
                DateTime dateTimeValue = new DateTime();
                DateTime.TryParse(data.ToString(), out dateTimeValue);
                return dateTimeValue;
            }
            else if (typename.Contains("decimal"))
            {
                return decimal.Parse(data.ToString());
            }
            else if (typename.Contains("byte[]"))
            {
                return data as Byte[];
            }
            else if (typename.Contains("byte"))
            {
                return byte.Parse(data.ToString());
            }
            else if (typename.Contains("boolean"))
            {
                return data;
            }
            else if (typename.Contains("double"))
            {
                return double.Parse(data.ToString());
            }
            else if (typename.Contains("single"))
            {
                return Single.Parse(data.ToString());
            }
            else if (typename.Contains("guid"))
            {
                return new Guid(data.ToString());
            }
            else if (typename.Contains("object"))
            {
                return data;
            }
            return null;
        }
        
        /// <summary>
        /// Get the sql server date time
        /// </summary>
        /// <returns>the DateTime object</returns>
        public static DateTime GetServerDateTime()
        {
            DateTime dateTime = (DateTime)DbAccess.Instance.ExecuteScalar("select getdate()");
            return dateTime;
        }

        /// <summary>
        /// Get the Serial Number of the specified system and date
        /// </summary>
        /// <param name="App_Name">system name</param>
        /// <param name="obj_Date">date string in format yyyyMMdd</param>
        /// <returns>the date string with serial number in foramt yyyyMMdd######</returns>
        public static string GetSerialNumber(string App_Name, string obj_Date)
        {
            Dictionary<String, object> parameters = new Dictionary<string,object>();
            parameters.Add("App_Name", App_Name);
            parameters.Add("obj_Date", obj_Date);
            return (String)DbAccess.Instance.ExecuteSPScalar("sp_GetNextSRNo", parameters);
        }

        /// <summary>
        /// Get the insert sql string for inserting log to server
        /// </summary>
        /// <param name="tranNo">Transaction No</param>
        /// <param name="apName">App name</param>
        /// <param name="opType">Operation type</param>
        /// <param name="remarks">Remark</param>
        /// <returns>the insert sql string</returns>
        public static string GetLogInsertString(string tranNo, string apName, string opType, string remarks)
        {
            string sqlc = "";
            string user = "";
            string Remarks = "";

            user = LoginInfo.Instance.Group + " " + LoginInfo.Instance.UID;
            if (user.Length <= 3)
            {
                throw new ConstraintException("You should login eSS first!");
            }

            Remarks = remarks;

            user = (user.Length > 20) ? user.Substring(0, 20) : user;
            Remarks = (Remarks.Length > 20) ? Remarks.Substring(0, 20) : Remarks;

            sqlc = " Insert into Customer..APSysOprLog (TrsRefNo, SystName, Opr_Type, Opr_User, Opr_Time, Remarks) " +
                   " VALUES ('" + tranNo + "','" + apName + "','" + opType + "','" +
                   user +
                   "',CONVERT(Char(8),GETDATE(),112) + CONVERT(CHAR(8),GETDATE(),108), '" + Remarks + "') ";

            return sqlc;
        }

        /// <summary>
        /// Create a batch to run action repeatly in a specified times
        /// </summary>
        /// <typeparam name="T">Entities Type, ex:Users</typeparam>
        /// <param name="sqlQuery">The sql query string for creating data reader, ex:Select * from General..Users order by UserID</param>
        /// <param name="bo">The BatchObject which conatains the entities</param>
        /// <param name="action">An delegate Action for repeatly operation</param>
        /// <param name="countsPerAction">The number of entities should be read from reader into entities per action</param>
        /// <param name="timesAction">Times to run actions</param>
        public static void Batch<T>(string sqlQuery, BatchObject<T> bo, Action<BatchObject<T>> action, int countsPerAction, int timesAction)
            where T : Entity
        {
            using (DbConnection con = DbEntityHelper.DataAccess.DbAccess.Instance.GetConnection())
            {
                DbCommand cmd = null;
                DbDataReader reader = null;
                try
                {
                    cmd = con is SqlConnection ? new SqlCommand() as DbCommand : new OdbcCommand() as DbCommand;
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = con;
                    reader = cmd.ExecuteReader();

                    for (int i = 0; i < timesAction; i++)
                    {
                        ((Entities<T>)bo.entities).Fill(reader, countsPerAction);
                        action(bo);
                    }
                }
                catch (Exception ex)
                {
                    DialogHelper.ExceptionDialogAndExitApp("ExecuteScalar error: ", ex);
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
        /// The class for DBHelper.Batch function
        /// </summary>
        /// <typeparam name="T">The type of entity for batch function</typeparam>
        public class BatchObject<T>
            where T : Entity
        {
            /// <summary>
            /// The entities for batch operation
            /// </summary>
            public Entities<T> entities; 

            /// <summary>
            /// An object for batch operation
            /// </summary>
            public object tag;

            /// <summary>
            /// Constructor of new batchobject with entities and tag
            /// </summary>
            /// <param name="e">The entities for batch operation</param>
            /// <param name="t">An object for batch operation</param>
            public BatchObject(Entities<T> e, object t)
            {
                entities = e;
                tag = t;
            }
        }
    }
}
