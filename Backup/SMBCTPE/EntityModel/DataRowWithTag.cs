using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SMBCTPE.EntityModel
{
    /// <summary>
    /// A custom datarow with a Tag object
    /// </summary>
    public class DataRowWithTag : DataRow
    {
        private DataTableWithRowsTag dataTable;

        /// <summary>
        /// A Tag attached with this row
        /// </summary>
        public Object Tag;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="rb"></param>
        internal DataRowWithTag(DataRowBuilder rb)
            : base(rb)
        {
            dataTable = (DataTableWithRowsTag)base.Table;
        }
    }
}
