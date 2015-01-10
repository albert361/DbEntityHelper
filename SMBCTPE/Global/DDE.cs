using System;
using System.Collections.Generic;
using System.Text;
using NDde.Client;

namespace DbEntityHelper.Global
{
    internal sealed class DDE
    {
        private static DdeClient client;

        private static void InitDDE()
        {
            client = new DdeClient("logon", "frmlogon");
        }

        /// <summary>
        /// Get login user information in DDE
        /// <para>These information will be stored into SMBCTEP.Global.LoginInfo</para>
        /// </summary>
        internal static void GetUserInfo()
        {
            InitDDE();

            try
            {
                client.Connect();

                LoginInfo loginInfo = LoginInfo.Instance;
                loginInfo.DSN = client.Request("txtdsn", 60000).Replace("\0", "");
                loginInfo.UID = client.Request("txtuid", 60000).Replace("\0", "");
                loginInfo.AUTH = client.Request("txtauth", 60000).Replace("\0", "");
                loginInfo.HENV = client.Request("txthenv", 60000).Replace("\0", "");
                loginInfo.HDBC = client.Request("txthdbc", 60000).Replace("\0", "");
                loginInfo.OK = client.Request("txtcmd", 60000).Replace("\0", "");
                loginInfo.Group = client.Request("txtGroup", 60000).Replace("\0", "");
                loginInfo.DPT = client.Request("txtDpt", 60000).Replace("\0", "");
                loginInfo.Visible = client.Request("txtVisible", 60000).Replace("\0", "");
            }
            catch (Exception ex)
            {
                throw new Exception("[Warning] Please Login eSS System !!!\n" + ex.Message);
            }
            finally
            {
                client.Disconnect();
            }
        }
    }
}
