namespace DbEntityHelper.Global
{
    partial class BaseForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.tlsTop = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.stsBottomLeft = new System.Windows.Forms.StatusStrip();
            this.lblUserID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDept = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblGrp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSysDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSrvDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsBottomRight = new System.Windows.Forms.StatusStrip();
            this.drpbtnSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.stsBottomLeft.SuspendLayout();
            this.stsBottomRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsTop
            // 
            this.tlsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsTop.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tlsTop.Location = new System.Drawing.Point(0, 0);
            this.tlsTop.Name = "tlsTop";
            this.tlsTop.Size = new System.Drawing.Size(984, 25);
            this.tlsTop.TabIndex = 1;
            this.tlsTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsTop_ItemClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.stsBottomLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.stsBottomRight, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.stsBottom, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 562);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 20);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // stsBottomLeft
            // 
            this.stsBottomLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserID,
            this.toolStripStatusLabel1,
            this.lblDept,
            this.toolStripStatusLabel2,
            this.lblGrp,
            this.toolStripStatusLabel3,
            this.lblSysDate,
            this.toolStripStatusLabel4,
            this.lblSrvDate});
            this.stsBottomLeft.Location = new System.Drawing.Point(0, 0);
            this.stsBottomLeft.Name = "stsBottomLeft";
            this.stsBottomLeft.Size = new System.Drawing.Size(500, 20);
            this.stsBottomLeft.TabIndex = 0;
            // 
            // lblUserID
            // 
            this.lblUserID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblUserID.Size = new System.Drawing.Size(69, 15);
            this.lblUserID.Text = "lblUserID";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 15);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // lblDept
            // 
            this.lblDept.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblDept.Name = "lblDept";
            this.lblDept.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblDept.Size = new System.Drawing.Size(60, 15);
            this.lblDept.Text = "lblDept";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 15);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // lblGrp
            // 
            this.lblGrp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblGrp.Name = "lblGrp";
            this.lblGrp.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblGrp.Size = new System.Drawing.Size(53, 15);
            this.lblGrp.Text = "lblGrp";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(13, 15);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // lblSysDate
            // 
            this.lblSysDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblSysDate.Name = "lblSysDate";
            this.lblSysDate.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblSysDate.Size = new System.Drawing.Size(77, 15);
            this.lblSysDate.Text = "lblSysDate";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(13, 15);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // lblSrvDate
            // 
            this.lblSrvDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblSrvDate.Name = "lblSrvDate";
            this.lblSrvDate.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblSrvDate.Size = new System.Drawing.Size(76, 15);
            this.lblSrvDate.Text = "lblSrvDate";
            // 
            // stsBottomRight
            // 
            this.stsBottomRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drpbtnSettings});
            this.stsBottomRight.Location = new System.Drawing.Point(904, 0);
            this.stsBottomRight.Name = "stsBottomRight";
            this.stsBottomRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stsBottomRight.Size = new System.Drawing.Size(80, 20);
            this.stsBottomRight.TabIndex = 1;
            this.stsBottomRight.Text = "statusStrip2";
            // 
            // drpbtnSettings
            // 
            this.drpbtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drpbtnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbout});
            this.drpbtnSettings.Image = global::DbEntityHelper.Properties.Resources.icoSettings;
            this.drpbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drpbtnSettings.Name = "drpbtnSettings";
            this.drpbtnSettings.Size = new System.Drawing.Size(29, 18);
            this.drpbtnSettings.Text = "toolStripDropDownButton1";
            this.drpbtnSettings.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.drpbtnSettings_DropDownItemClicked);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::DbEntityHelper.Properties.Resources.icoInfo;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(111, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // stsBottom
            // 
            this.stsBottom.Location = new System.Drawing.Point(500, 0);
            this.stsBottom.Name = "stsBottom";
            this.stsBottom.Size = new System.Drawing.Size(404, 20);
            this.stsBottom.TabIndex = 2;
            // 
            // pnlViewer
            // 
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 25);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(984, 537);
            this.pnlViewer.TabIndex = 3;
            this.pnlViewer.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.pnlViewer_ControlAdded);
            this.pnlViewer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.pnlViewer_ControlRemoved);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(984, 582);
            this.Controls.Add(this.pnlViewer);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tlsTop);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.stsBottomLeft.ResumeLayout(false);
            this.stsBottomLeft.PerformLayout();
            this.stsBottomRight.ResumeLayout(false);
            this.stsBottomRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip stsBottomLeft;
        private System.Windows.Forms.ToolStripStatusLabel lblSysDate;
        private System.Windows.Forms.StatusStrip stsBottomRight;
        private System.Windows.Forms.ToolStripDropDownButton drpbtnSettings;
        private System.Windows.Forms.ToolStripMenuItem btnAbout;
        private System.Windows.Forms.ToolStripStatusLabel lblUserID;
        private System.Windows.Forms.ToolStripStatusLabel lblDept;
        private System.Windows.Forms.ToolStripStatusLabel lblGrp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        /// <summary>
        /// The top tool strip
        /// </summary>
        public System.Windows.Forms.ToolStrip tlsTop;
        /// <summary>
        /// The bottom status strip
        /// </summary>
        public System.Windows.Forms.StatusStrip stsBottom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblSrvDate;
        private System.Windows.Forms.Panel pnlViewer;


    }
}