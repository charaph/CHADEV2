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
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm2()
        {
            InitializeComponent();
            
        }
      
        private void XtraForm2_Load(object sender, EventArgs e)
        {
  
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            XtraUserControl2 userControl = new XtraUserControl2();
            userControl.Text = "F2 TEST";
            tabbedView1.AddDocument(userControl);
            tabbedView1.ActivateDocument(userControl);
        }
    }
}