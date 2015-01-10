using System;
using System.Collections.Generic;
using System.Text;
using DbEntityHelper.EntityModel;
using DbEntityHelper.Helper;

namespace Share.Models
{
    public class User : Entity
    {
        private string userID;
        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }

        private string employee_No;
        /// <summary>
        /// 
        /// </summary>
        public string Employee_No
        {
            get { return employee_No; }
            set
            {
                employee_No = value;
                OnPropertyChanged("Employee_No");
            }
        }

        private string grp;
        /// <summary>
        /// 
        /// </summary>
        public string GRP
        {
            get { return grp; }
            set
            {
                grp = value;
                OnPropertyChanged("GRP");
            }
        }

        public User(string id, string no, string grp)
        {
            UserID = id;
            Employee_No = no;
            GRP = grp;
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// insert this user to database
        /// </summary>
        public void InsertToDataBase()
        {
            StringBuilder insertSqlString = new StringBuilder("insert into TABLE (");
            foreach (KeyValuePair<String, Object> pair in this.GetSqlFieldsAndValuesPair())
            {
                insertSqlString.Append(pair.Key + ",");
            }
            insertSqlString.Remove(insertSqlString.Length - 1, 1);
            insertSqlString.Append(") VALUES (");
            foreach (KeyValuePair<String, Object> pair in this.GetSqlFieldsAndValuesPair())
            {
                insertSqlString.Append("'" + pair.Value + "',");
            }
            insertSqlString.Remove(insertSqlString.Length - 1, 1);
            insertSqlString.Append(");");
            DialogHelper.ShowMessageBox(insertSqlString.ToString());
        }
    }
}
