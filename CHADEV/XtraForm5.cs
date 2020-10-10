using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CHADEV
{
    public partial class XtraForm5 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
   
        XtraUserControl2 XXuserControl = new XtraUserControl2();

        public XtraForm5()
        {
            InitializeComponent();
            XXuserControl.Text = "F3";

            //DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2013";
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            XtraUserControl1 userControl = new XtraUserControl1();
            userControl.Name = "F1-" + tabbedView1.Documents.Count.ToString();
            userControl.Text = "F1";
            tabbedView1.AddDocument(userControl);
            tabbedView1.ActivateDocument(userControl);
        }

        private void tabbedView1_DocumentClosed(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
           
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            XtraUserControl2 userControl = new XtraUserControl2();
            userControl.Name = "F2-" + tabbedView1.Documents.Count.ToString();
            userControl.Text = "F2";

            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT DISTINCT TOP(10) ModelShow FROM Model");
            new ObjDevEx.setComboboxEdit(userControl.comboBoxEdit1, sbSQL).getDataRange();

            tabbedView1.AddDocument(userControl);
            tabbedView1.ActivateDocument(userControl);

            
        }

        private void accordionControl1_SelectedElementChanged(object sender, DevExpress.XtraBars.Navigation.SelectedElementChangedEventArgs e)
        {
           // if (e.Element == null) return;
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            
            tabbedView1.AddDocument(XXuserControl);
            tabbedView1.ActivateDocument(XXuserControl);
        }

        private void XtraForm5_Load(object sender, EventArgs e)
        {

        }

        private void tabbedView1_DocumentClosing(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs e)
        {
            if(new Function().msgQuiz("ยกเลิกปิด") == true)
            {
                e.Cancel = true;
                return;
            }
            
            //if (IsChanged)
            //{
            //    if (XtraMessageBox.Show(
            //        "There are unsaved changes, do you wish to save these?",
            //        "Unsaved Changes",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        if (!Save())
            //        {
            //            //Cancel the close if the save fails.  
            //            e.Cancel = true;
            //            return;
            //        }
            //    }
            //}
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabbedView1.ActiveDocument != null)
            {
                //MessageBox.Show("Active Name: " + tabbedView1.ActiveDocument.Control.Name);
                //MessageBox.Show("Active Caption: " + tabbedView1.ActiveDocument.Caption);

                TextBox txtBox = (TextBox)tabbedView1.ActiveDocument.Control.Controls.Find("textBox1", true).FirstOrDefault();
                if (txtBox != null)
                {
                    MessageBox.Show(txtBox.Text);
                }

                DateEdit dEdit = (DateEdit)tabbedView1.ActiveDocument.Control.Controls.Find("dateEdit1", true).FirstOrDefault();
                if (dEdit != null)
                {
                    MessageBox.Show(dEdit.Text);
                }

            }
            else
            {
                MessageBox.Show("Not Select Form");
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(tabbedView1.Documents.Count > 0)
            {
                for (int i = 0; i < tabbedView1.Documents.Count; i++)
                {
                    TextBox txtBox = (TextBox)tabbedView1.Documents[i].Control.Controls.Find("textBox1", true).FirstOrDefault();
                    if (txtBox != null)
                    {
                        MessageBox.Show(txtBox.Text);
                    }
                }

            }
            
        }
    }
}