using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;

namespace CHADEV
{
    public partial class XtraForm4 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        StringBuilder sbSQL = new StringBuilder();
        public XtraForm4()
        {
            InitializeComponent();

        }
        
        private void XtraForm4_Load(object sender, EventArgs e)
        {
            sbSQL.Append("SELECT TOP (200) MID FROM Model");
            new ObjDevEx.setComboboxEdit(comboBoxEdit1, sbSQL).getDataRange();

            sbSQL.Clear();
            sbSQL.Append("SELECT TOP (200) MID, ModelShow, Size, UColor FROM Model");

            new ObjDevEx.setLookUpEdit(lookUpEdit1, sbSQL, "MID", "MID").getData();
            new ObjDevEx.setSearchLookUpEdit(searchLookUpEdit1, sbSQL, "MID", "MID").getData();
            new ObjDevEx.setGridControl(gridControl1, gridView1, sbSQL).getDataShowOrder(false, false, false, true);

            sbSQL.Clear();
            sbSQL.Append("SELECT DISTINCT TOP (200) ModelShow, Size, UColor ");
            sbSQL.Append("FROM(SELECT        ModelShow, Size, UColor ");
            sbSQL.Append("                          FROM            Model ");
            sbSQL.Append("                          UNION ALL ");
            sbSQL.Append("                          SELECT        ModelShow, '-' AS Size, '-' AS UColor ");
            sbSQL.Append("                          FROM            Model AS Model_1) AS DATA ");
            sbSQL.Append("ORDER BY ModelShow, Size, UColor ");
            new ObjDevEx.setTreeList(treeList1, sbSQL).getDataNodes(false, false, false, true, false);
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show(textEdit1.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}