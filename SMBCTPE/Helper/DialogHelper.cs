using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DbEntityHelper.Helper
{
    /// <summary>
    /// A helper class with several static functions for displaying dialogs
    /// </summary>
    public class DialogHelper
    {
        /// <summary>
        /// Show System.Windows.Forms.MessageBox with Message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void ShowMessageBox(string msg)
        {
            MessageBox.Show(msg);
        }

        /// <summary>
        /// Show a dialog with exception message and stack trace.
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void ExceptionDialog(Exception ex)
        {
            MessageBox.Show("Notice! Please printscreen and send it back to ITD for troubleshooting!\n\n"
                            + ex.Message + "\n" + ex.StackTrace, "Exception occurred!");
        }

        /// <summary>
        /// Show a dialog with prefix, exception message, and stack trace.
        /// </summary>
        /// <param name="prefix">Prefix string</param>
        /// <param name="ex">Exception</param>
        public static void ExceptionDialog(String prefix, Exception ex)
        {
            MessageBox.Show("Notice! Please printscreen and send it back to ITD for troubleshooting!\n\n"
                            + prefix + "\n" + ex.Message + "\n" + ex.StackTrace, "Exception occurred!");
        }

        /// <summary>
        /// Show a dialog with exception message and stack trace. Then exit current AP
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void ExceptionDialogAndExitApp(Exception ex)
        {
            MessageBox.Show("Notice! Please printscreen and send it back to ITD for troubleshooting!\n\n"
                            + ex.Message + "\n" + ex.StackTrace, "Exception occurred!");
            Application.Exit();
        }

        /// <summary>
        /// Show a dialog with prefix, exception message, and stack trace. Then exit current AP
        /// </summary>
        /// <param name="prefix">Prefix string</param>
        /// <param name="ex">Exception</param>
        public static void ExceptionDialogAndExitApp(String prefix, Exception ex)
        {
            MessageBox.Show("Notice! Please printscreen and send it back to ITD for troubleshooting!\n\n"
                            + prefix + "\n" + ex.Message + "\n" + ex.StackTrace, "Exception occurred!");
            Application.Exit();
        }

        /// <summary>
        /// Show an Input box for user input text
        /// </summary>
        /// <param name="prompt">Message</param>
        /// <param name="title">Dialog title</param>
        /// <param name="defaultValue">Default value in textbox</param>
        /// <returns>The input string</returns>
        public static string Inputbox(string prompt, string title, string defaultValue)
        {
            Form frmInput = new Form();
            frmInput.MinimizeBox = false;
            frmInput.MaximizeBox = false;
            frmInput.StartPosition = FormStartPosition.CenterScreen;
            frmInput.Width = 280;
            frmInput.Height = 220;
            frmInput.Text = prompt;

            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            lbl.Text = title;
            lbl.Left = 18;
            lbl.Top = 20;
            lbl.Parent = frmInput;
            lbl.AutoSize = true;

            System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
            tb.Top = 80;
            tb.Width = 230;
            tb.Left = 20;
            tb.Parent = frmInput;
            tb.Text = defaultValue;
            tb.SelectAll();

            System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button();
            btnOk.Left = 40;
            btnOk.Top = 140;
            btnOk.Parent = frmInput;
            btnOk.Text = "OK";
            frmInput.AcceptButton = btnOk;
            btnOk.DialogResult = DialogResult.OK;

            System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button();
            btnCancel.Left = 150;
            btnCancel.Top = 140;
            btnCancel.Parent = frmInput;
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;

            try
            {
                if (frmInput.ShowDialog() == DialogResult.OK)
                {
                    return tb.Text;
                }
                else
                {
                    return "";
                }
            }
            finally
            {
                frmInput.Dispose();
            }
        }
    }
}
