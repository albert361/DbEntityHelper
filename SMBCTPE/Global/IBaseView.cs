using System;
using System.Collections.Generic;
using System.Text;

namespace DbEntityHelper.Global
{
    /// <summary>
    /// The BaseView interface
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Handle the tool strip item click event from BaseForm
        /// </summary>
        /// <param name="itemName">the clicked name</param>
        void ToolStripItemClicked(string itemName);
    }
}
