using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DbEntityHelper.Helper
{
    /// <summary>
    /// A helper class with several static functions for creating windows forms tool strip item
    /// </summary>
    public class ToolStripHelper
    {
        /// <summary>
        /// ToolStripItem templates
        /// </summary>
        [Flags]
        public enum Template : int
        {
            /* Note that you shouldn't modify the order of New,Modify,Delete,Inquire,Print, and Approve. */
            /* They are controlled by the permission string of eSS system */
            /// <summary>
            /// New button, tlsbtnNew
            /// </summary>
            New = 1,
            /// <summary>
            /// Modify button, tlsbtnModify
            /// </summary>
            Modify = 2,
            /// <summary>
            /// Delete button, tlsbtnDelete
            /// </summary>
            Delete = 4,
            /// <summary>
            /// Inquire button, tlsbtnInquire
            /// </summary>
            Inquire = 8,
            /// <summary>
            /// Print button, tlsbtnPrint
            /// </summary>
            Print = 16,
            /// <summary>
            /// Approve button, tlsbtnApprove
            /// </summary>
            Approve = 32,
            /// <summary>
            /// Reject button, tlsbtnReject
            /// </summary>
            Reject = 1024,
            /// <summary>
            /// Send button, tlsbtnSend
            /// </summary>
            Send,
            /// <summary>
            /// Clear button, tlsbtnClear
            /// </summary>
            Clear,
            /// <summary>
            /// Exit button, tlsbtnExit
            /// </summary>
            Exit,
            /// <summary>
            /// Separator
            /// </summary>
            Separator,
            /// <summary>
            /// Export, tlsbtnExport
            /// </summary>
            Export,
            /// <summary>
            /// Download, tlsbtnDownload
            /// </summary>
            Download,
            /// <summary>
            /// Link, tlsbtnLink
            /// </summary>
            Link,
            /// <summary>
            /// Copy, tlsbtnCopy
            /// </summary>
            Copy,
            /// <summary>
            /// Settings, tlsbtnSettings
            /// </summary>
            Settings,
            /// <summary>
            /// Calendar, tlsbtnCalendar
            /// </summary>
            Calendar

        }

        /// <summary>
        /// Create a tool strip item by the template enum
        /// </summary>
        /// <param name="template">the template enum</param>
        /// <returns>a tool strip item object</returns>
        public static ToolStripItem CreateItem(Template template)
        {
            ToolStripItem item;

            if (template == Template.Separator)
            {
                item = new ToolStripSeparator();
            }
            else
            {
                item = new ToolStripButton();
                switch (template)
                {
                    case Template.Approve:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoOk;
                        item.Name = "tlsbtnApprove";
                        item.Text = "&Approve";
                        break;
                    case Template.Calendar:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoCalendar;
                        item.Name = "tlsbtnCalendar";
                        item.Text = "&Calendar";
                        break;
                    case Template.Clear:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoErase;
                        item.Name = "tlsbtnClear";
                        item.Text = "&Clear";
                        break;
                    case Template.Copy:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoCopy;
                        item.Name = "tlsbtnCopy";
                        item.Text = "&Copy";
                        break;
                    case Template.Delete:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoTrash;
                        item.Name = "tlsbtnDelete";
                        item.Text = "&Delete";
                        break;
                    case Template.Download:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoDownload;
                        item.Name = "tlsbtnDownload";
                        item.Text = "&Download";
                        break;
                    case Template.Exit:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoLogout;
                        item.Name = "tlsbtnExit";
                        item.Text = "&Exit";
                        break;
                    case Template.Export:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoExport;
                        item.Name = "tlsbtnExport";
                        item.Text = "&Export";
                        break;
                    case Template.Inquire:
                        item.Image = global::DbEntityHelper.Properties.Resources.isoSearch;
                        item.Name = "tlsbtnInquire";
                        item.Text = "&Inquire";
                        break;
                    case Template.Link:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoLink;
                        item.Name = "tlsbtnLink";
                        item.Text = "&Link";
                        break;
                    case Template.Modify:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoEdit;
                        item.Name = "tlsbtnModify";
                        item.Text = "&Modify";
                        break;
                    case Template.New:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoNew;
                        item.Name = "tlsbtnNew";
                        item.Text = "&New";
                        break;
                    case Template.Print:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoPrinter;
                        item.Name = "tlsbtnPrint";
                        item.Text = "&Print";
                        break;
                    case Template.Reject:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoCancel;
                        item.Name = "tlsbtnReject";
                        item.Text = "&Reject";
                        break;
                    case Template.Send:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoSend;
                        item.Name = "tlsbtnSend";
                        item.Text = "&Send";
                        break;
                    case Template.Settings:
                        item.Image = global::DbEntityHelper.Properties.Resources.icoSettings;
                        item.Name = "tlsbtnSettings";
                        item.Text = "&Settings";
                        break;
                    default:
                        break;
                }

                SetItemCore(item);
            }
            return item;
        }

        /// <summary>
        /// Apply permission filter on a toolstripitem list and doing the action
        /// </summary>
        /// <param name="list">the list of templates</param>
        /// <param name="action">the action to do with each item object</param>
        /// <param name="filter">the permission filter string
        /// <para>
        /// <seealso cref="DbEntityHelper.Global.BaseForm.Permission"/>
        /// </para>
        /// </param>
        public static void ApplyFilter(List<Template> list, Action<object> action, string filter)
        {
            List<Template> l = ApplyFilter(list, filter);
            foreach (Template t in l)
            {
                action(CreateItem(t));
            }
        }

        /// <summary>
        /// Apply permission filter on a toolstripitem list
        /// </summary>
        /// <param name="list">the list of templates</param>
        /// <param name="filter">the permission filter string
        /// <para>
        /// <seealso cref="DbEntityHelper.Global.BaseForm.Permission"/>
        /// </para>
        /// </param>
        public static List<Template> ApplyFilter(List<Template> list, string filter)
        {
            string f = filter.ToLower().Trim();

            for (int i = 0; i < 6; i++) // filter string is consisted of 6 chars
            {
                if (f[i].CompareTo('y') != 0)
                {
                    Template t = (Template)(1 << i);
                    list.Remove(t);
                    // Approve is synced with Reject
                    if (t == Template.Approve)
                        list.Remove(Template.Reject);
                }
            }

            return list;
        }

        private static void SetItemCore(ToolStripItem item)
        {
            item.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            item.ImageScaling = ToolStripItemImageScaling.None;
            item.ImageTransparentColor = System.Drawing.Color.Magenta;
            item.Padding = new System.Windows.Forms.Padding(3);
            item.Size = new System.Drawing.Size(44, 52);
            item.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
        }
    }
}
