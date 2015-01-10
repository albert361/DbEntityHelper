using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SMBCTPE.EntityModel
{
    /// <summary>
    /// A custom datatable with DataRowWithTag rows
    /// </summary>
    public class DataTableWithRowsTag : DataTable
    {
        /// <summary>
        /// override the NewRow() function
        /// </summary>
        /// <param name="builder">the datarow builder</param>
        /// <returns>DataRowWithTag object as DataRow</returns>
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new DataRowWithTag(builder);
        }
    }
}