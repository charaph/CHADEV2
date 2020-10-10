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
using DevExpress.XtraBars.Docking;
using DevExpress.Utils.Extensions;

namespace CHADEV
{
    public partial class Frame : DevExpress.XtraEditors.XtraForm
    {
        public Frame()
        {
            InitializeComponent();
        }

        private void Frame_Load(object sender, EventArgs e)
        {
            string text = "XXXX";

            xtraUserControl1.Name = text.ToLower() + "UserControl";
            xtraUserControl1.Text = text;

            labelControl1.Parent = xtraUserControl1;
            labelControl1.Appearance.Font = new Font("Tahoma", 25.25F);
            labelControl1.Appearance.ForeColor = Color.Gray;
            labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            labelControl1.AutoSizeMode = LabelAutoSizeMode.None;
            labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            labelControl1.Text = text;
        }
    }
}