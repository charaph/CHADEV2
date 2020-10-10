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
    public partial class TreeList : DevExpress.XtraEditors.XtraForm
    {
        public TreeList()
        {
            InitializeComponent();
        }

        private void TreeList_Load(object sender, EventArgs e)
        {
            //treeList1.DataSource = CreateTLData(100);
        }

        DataTable CreateTLData(int recordCount)
        {
            Random rnd = new Random();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("ParentID", typeof(int));
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Date", typeof(DateTime));
            tbl.Columns.Add("Checked", typeof(bool));
            for (int i = 0; i < recordCount; i++)
                tbl.Rows.Add(new object[] { i, rnd.Next(20), String.Format("Name{0}", i), DateTime.Now.Date.AddDays(rnd.Next(-250, 250)), rnd.Next(2) == 0 });
            return tbl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeList1.ClearNodes();
            treeList1.DataSource = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeList1.DataSource = CreateTLData(10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT 'XX' AS XX, InstallationID, MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT 'YY' AS XX, InstallationID, 'MDS-DEV' AS MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT TOP(1) 'ZZ' AS XX, InstallationID, 'MDS-DEV' AS MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT TOP(1) 'ZZZ' AS XX, InstallationID, 'MDS-DEV' AS MachineName, InstanceName FROM Keys ");

            new ObjDevEx.setTreeList(treeList1, sbSQL).getDataNodes(true, true, false, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT 'XXDDDDDDDDD' AS XX, InstallationID, MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT 'YYDDDDDDDDD' AS XX, InstallationID, 'MDS-DEVRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR' AS MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT TOP(1) 'ZZ' AS XX, InstallationID, 'MDS-DEV' AS MachineName, InstanceName FROM Keys ");
            sbSQL.Append("UNION ALL ");
            sbSQL.Append("SELECT TOP(1) 'ZZ' AS XX, InstallationID, 'MDS-DEV' AS MachineName, InstanceName FROM Keys ");

            new ObjDevEx.setTreeList(treeList1, sbSQL).getDataNodes(false, false, false, true);
        }
    }
}