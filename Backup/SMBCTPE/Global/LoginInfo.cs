using System;
using System.Collections.Generic;
using System.Text;
using SMBCTPE.DataAccess;
using SMBCTPE.Helper;

namespace SMBCTPE.Global
{
    /// <summary>
    /// A singleton object stores user login info.
    /// </summary>
    public sealed class LoginInfo
    {
        private LoginInfo() { }

        private static object sync = new object();

        private static volatile LoginInfo instance;
        /// <summary>
        /// Get the LoginInfo unique instance
        /// </summary>
	    public static LoginInfo Instance
	    {
		    get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new LoginInfo();
                        }
                    }
                }
                return instance;
            }
	    }

        /// <summary>
        /// Get permission string, ex:YNYYNYN
        /// </summary>
        /// <param name="functionId">Function ID</param>
        /// <param name="permission">permission string</param>        
        /// <returns>bool</returns>
        public bool GetLoginInfoAndPermissions(string functionId, ref string permission)
        {
            if (instance == null)
                throw new NullReferenceException();

            try
            {
                DDE.GetUserInfo();
                instance.SeSSName = System.Windows.Forms.Application.ProductName;
                string strSQL = @"Select a.ins+a.upd+a.del+a.qry+a.approve+a.prt+a.Approve 
                           from General..Permission a 
                           join General..Users b 
                             on a.groupid=b.grp 
                            and b.userid='" + instance.UID + @"'
                          where a.functionname='" + functionId + "'";
                object o = DbAccess.Instance.ExecuteScalar(strSQL);
                if (o == null)
                {
                    DialogHelper.ShowMessageBox("No permission!");
                    return false;
                }
                else
                {
                    permission = o.ToString();
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ExceptionDialog("getPermission Error: ", ex);
                return false;
            } 
            return true;
        }

        #region Public members
        
        private string dsn = "";
        /// <summary>
        /// Database server name
        /// </summary>
        public string DSN
        {
            get { return dsn; }
            internal set { dsn = value; }
        }
	
        private string uid = "";
        /// <summary>
        /// User ID
        /// </summary>
        public string UID
        {
            get { return uid; }
            internal set { uid = value; }
        }
	
        private string auth = "";
        /// <summary>
        /// Auth
        /// </summary>
        public string AUTH
        {
            get { return auth; }
            internal set { auth = value; }
        }
	
        private string henv = "";
        /// <summary>
        /// 
        /// </summary>
        public string HENV
        {
            get { return henv; }
            internal set { henv = value; }
        }
	
        private string hdbc = "";
        /// <summary>
        /// 
        /// </summary>
        public string HDBC
        {
            get { return hdbc; }
            internal set { hdbc = value; }
        }
	
        private string ok = "";
        /// <summary>
        /// 
        /// </summary>
        public string OK
        {
            get { return ok; }
            internal set { ok = value; }
        }
	
        private string group = "";
        /// <summary>
        /// 
        /// </summary>
        public string Group
        {
            get { return group; }
            internal set { group = value; }
        }
	
        private string dpt = "";
        /// <summary>
        /// Department
        /// </summary>
        public string DPT
        {
            get { return dpt; }
            internal set { dpt = value; }
        }
	
        private string visible = "";
        /// <summary>
        /// 
        /// </summary>
        public string Visible
        {
            get { return visible; }
            internal set { visible = value; }
        }
	
        private string ulevel = "";
        /// <summary>
        /// User level
        /// </summary>
        public string ULevel
        {
            get { return ulevel; }
            internal set { ulevel = value; }
        }
	
        private string seSSName = "";
        /// <summary>
        /// Project name
        /// </summary>
        public string SeSSName
        {
            get { return seSSName; }
            internal set { seSSName = value; }
        }
	
        #endregion
    }
}
