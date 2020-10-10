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
    public partial class FormTest : DevExpress.XtraEditors.XtraForm
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT InstallationID FROM Keys");
            new ObjDevEx.setComboboxEdit(comboBoxEdit1, sbSQL).getDataRange();

            sbSQL.Clear();
            sbSQL.Append("SELECT MachineName, InstallationID, InstanceName FROM Keys");
            new ObjDevEx.setLookUpEdit(lookUpEdit1, sbSQL, "InstallationID", "InstallationID").getData();
            new ObjDevEx.setLookUpEdit(lookUpEdit2, sbSQL, "InstallationID", "InstallationID").getData();

            searchLookUpEdit2.Properties.DataSource = new DBQuery(sbSQL).getDataTable();
            new ObjDevEx.setSearchLookUpEdit(searchLookUpEdit1, sbSQL, "InstallationID", "InstallationID").getData();
            new ObjDevEx.setSearchLookUpEdit(searchLookUpEdit2, sbSQL, "InstallationID", "InstallationID").getData();
        }
    }   
}