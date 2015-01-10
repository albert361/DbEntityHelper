using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SMBCTPE.Helper;
using SMBCTPE.DataAccess;

namespace SMBCTPE.Global
{
    /// <summary>
    /// Base Form class for all eSS App
    /// </summary>
    public partial class BaseForm : Form
    {
        private String permission = "";
        private String functionId = "";

        /// <summary>
        /// The permission string gotten from eSS
        /// <para>INS,UPD,DEL,QRY,PRT,APPROVE</para>
        /// <para>Add,Mod,Del,Inq,Prt,Approve</para>
        /// </summary>
        public String Permission
        {
            get { return permission; }
            private set { permission = value; }
        }

        /// <summary>
        /// The eSS function ID
        /// </summary>
        [Description("Sets the eSS Function ID"),
         Category("Values"),
         DefaultValue(""),
         Browsable(true)]
        public String FunctionId
        {
            get { return functionId; }
            set { functionId = value; }
        }

        /// <summary>
        /// Return the current view
        /// </summary>
        public UserControl CurrentView
        {
            get {
                if (pnlViewer.Controls.Count == 1)
                    return pnlViewer.Controls[0] as UserControl;
                else
                    return null;
            }
        }
	
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
            base.Load += new System.EventHandler(this.BaseForm_Load);
        }

        /// <summary>
        /// Navigate panel to the input view
        /// </summary>
        /// <param name="view">The input view (UserControl with IBaseView)</param>
        public void Navigate(IBaseView view)
        {
            Control ctr = view as Control;
            ctr.Dock = DockStyle.Fill;
            ctr.Parent = this;
            pnlViewer.Controls.Clear();
            pnlViewer.Controls.Add(ctr);
        }

        /// <summary>
        /// The Form Load event
        /// </summary>
        public new event EventHandler Load;

        #region protected virtual event handlers
        /// <summary>
        /// The top tool strip item clicked event
        /// </summary>
        /// <param name="sender">the top tool strip item</param>
        /// <param name="e">click event argument</param>
        protected virtual void tlsTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process the command keys
        /// </summary>
        /// <param name="msg">Win32 message</param>
        /// <param name="keyData">The pressed key</param>
        /// <returns>True if processed</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// This event is fired when any controls added into pnlViewer
        /// </summary>
        /// <param name="sender">pnlViewer</param>
        /// <param name="e">control event arguments</param>
        protected virtual void pnlViewer_ControlAdded(object sender, ControlEventArgs e)
        {
            if (!(e.Control is IBaseView))
            {
                throw new InvalidOperationException("You can only add IBaseView here");
            }
            SetKeyEvents(e.Control);
        }

        private void SetKeyEvents(Control control)
        {
            foreach (Control ctr in control.Controls)
            {
                if (ctr.Controls.Count > 0)
                {
                    SetKeyEvents(ctr);
                }
                if (ctr.TabStop)
                {
                    ctr.KeyPress -= ctr_KeyPress;
                    ctr.KeyPress += new KeyPressEventHandler(ctr_KeyPress);
                    if (ctr is Button)
                    {
                        (ctr as Button).Click -= btn_Click;
                        (ctr as Button).Click += new EventHandler(btn_Click);
                    }
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ctr_KeyPress(sender, new KeyPressEventArgs((char)Keys.Return));
        }

        private void ctr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.SelectNextControl(sender as Control, true, true, true, true);
            }
        }

        /// <summary>
        /// This event is fired when any controls removed into pnlViewer
        /// </summary>
        /// <param name="sender">pnlViewer</param>
        /// <param name="e">control event arguments</param>
        protected virtual void pnlViewer_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        /// <summary>
        /// The bottom dripdown menu item clicked event
        /// </summary>
        /// <param name="sender">the menu item</param>
        /// <param name="e">click event argument</param>
        protected virtual void drpbtnSettings_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region private event handlers
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About dlgAbout = new About();
            dlgAbout.ShowDialog(this);
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Events.Dispose();
                return;
            }

            if (!Global.LoginInfo.Instance.GetLoginInfoAndPermissions(functionId, ref permission))
            {
                DialogHelper.ShowMessageBox("The login process is failed!");
                Application.Exit();
                return;
            }

            lblUserID.Text = LoginInfo.Instance.UID;
            lblDept.Text = LoginInfo.Instance.DPT;
            lblGrp.Text = LoginInfo.Instance.Group;
            lblSysDate.Text = "[Sys] " + DateTime.Today.ToShortDateString();
            lblSrvDate.Text = "[Srv] " + DBHelper.GetServerDateTime().ToShortDateString();

            if (Load != null)
                Load(this, new EventArgs());
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (DialogResult.OK == MessageBox.Show("This application is exiting!\nAre you sure?", "Confirmation", MessageBoxButtons.OKCancel))
                {
                    e.Cancel = false;

                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion
    }
}