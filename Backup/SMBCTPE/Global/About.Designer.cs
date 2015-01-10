namespace SMBCTPE.Global
{
    partial class About
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
            this.lnkIconSetPage = new System.Windows.Forms.LinkLabel();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lnkIconSetPage
            // 
            this.lnkIconSetPage.AutoSize = true;
            this.lnkIconSetPage.LinkArea = new System.Windows.Forms.LinkArea(23, 54);
            this.lnkIconSetPage.Location = new System.Drawing.Point(62, 47);
            this.lnkIconSetPage.Name = "lnkIconSetPage";
            this.lnkIconSetPage.Size = new System.Drawing.Size(292, 34);
            this.lnkIconSetPage.TabIndex = 0;
            this.lnkIconSetPage.TabStop = true;
            this.lnkIconSetPage.Text = "Free Icon Set License\r\nhttp://iconpharm.com/monochrome_windows8_icon_set.html";
            this.lnkIconSetPage.UseCompatibleTextRendering = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(243, 84);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(111, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 118);
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lnkIconSetPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkIconSetPage;
        private System.Windows.Forms.Button btnOk;
    }
}