using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Deployment.Application;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using DevExpress.Data.TreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

namespace ObjDevEx
{
    //************* DATE EDIT *********************
    class setDateEdit
    {
        private DateEdit _dDes;
        public setDateEdit(DateEdit dDes)
        {
            this._dDes = dDes;
        }

        public void setStyle(string MarkStyle="dd/MM/yyyy", string CalendarSelect = "DMY")
        {
            this._dDes.Properties.Mask.EditMask = MarkStyle;
            this._dDes.Properties.Mask.UseMaskAsDisplayFormat = true;
            string _CalendarSelect = CalendarSelect.ToUpper().Trim();
            if (_CalendarSelect == "DMY")
            {
                this._dDes.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
                this._dDes.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
            }
            else if (_CalendarSelect == "MY")
            {
                this._dDes.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                this._dDes.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            }
            else if (_CalendarSelect == "Y")
            {
                this._dDes.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearsGroupView;
                this._dDes.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearsGroupView;
            }
        }
     }

    //************* COMBOBOXEDIT *********************
    class setComboboxEdit
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private ComboBoxEdit _cbDes;

        public setComboboxEdit(ComboBoxEdit cbDes)
        {
            this._cbDes = cbDes;
        }

        public setComboboxEdit(ComboBoxEdit cbDes, StringBuilder sqlSource)
        {
            this._sqlSource = sqlSource;
            this._cbDes = cbDes;
        }

        public setComboboxEdit(ComboBoxEdit cbDes, StringBuilder sqlSource, string Conn)
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._cbDes = cbDes;
        }

        public void insertItems(string ItemsValue, int Index = 0)
        {
            if (!this._cbDes.Properties.Items.Contains(ItemsValue))
            {
                this._cbDes.Properties.Items.Insert(Index, ItemsValue);
            }
        }

        public void getDataRange(bool Continuous = false)
        {
            if (Continuous == false)
            {
                this._cbDes.Properties.Items.Clear();
            }
            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
            var strings = dt.Rows.Cast<DataRow>().Select(r => r.ItemArray[0].ToString()).ToArray();
            this._cbDes.Properties.Items.AddRange(strings);
        }
    }

    //************* IMAGE COMBOBOX EDIT *********************
    class setImageComboboxEdit
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private ImageComboBoxEdit _icbDes;

        public setImageComboboxEdit(ImageComboBoxEdit icbDes)
        {
            this._icbDes = icbDes;
        }

        public setImageComboboxEdit(ImageComboBoxEdit icbDes, StringBuilder sqlSource)
        {
            this._sqlSource = sqlSource;
            this._icbDes = icbDes;
        }

        public setImageComboboxEdit(ImageComboBoxEdit icbDes, StringBuilder sqlSource, string Conn)
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._icbDes = icbDes;
        }

        //add items
        public void AddItems(ImageCollection imgList, string ItemsValue, int imageIndex = 0)
        {
            if (!this._icbDes.Properties.Items.Contains(ItemsValue))
            {
                this._icbDes.Properties.Items.Add(new ImageComboBoxItem(ItemsValue, ItemsValue, imageIndex));
                this._icbDes.Properties.SmallImages = imgList;
            }
        }

        public void insertItems(ImageCollection imgList, string ItemsValue, int Index = 0, int imageIndex = 0)
        {
            if (!this._icbDes.Properties.Items.Contains(ItemsValue))
            {
                this._icbDes.Properties.Items.Insert(Index, new ImageComboBoxItem(ItemsValue, ItemsValue, imageIndex));
                this._icbDes.Properties.SmallImages = imgList;
            }      
        }

        public void getDataRange(ImageCollection imgList, bool Continuous = false)
        {
            if (Continuous == false)
            {
                this._icbDes.Properties.Items.Clear();
            }
            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
            ImageComboBoxItem[] strings = new ImageComboBoxItem[1];
            int ii = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    System.Array.Resize(ref strings, ii + 1);
                    strings[ii] = new ImageComboBoxItem(row[0].ToString(), row[0].ToString(), ii);
                }
                catch (Exception) { }
                ii++;
            }
            this._icbDes.Properties.Items.AddRange(strings);
            this._icbDes.Properties.SmallImages = imgList;
        }
    }

    //************* LOOK UP EDIT *********************
    class setLookUpEdit
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private LookUpEdit _lueDes;
        private string _DisplayMember;
        private string _ValueMember;

        public setLookUpEdit(LookUpEdit lueDes)
        {
            this._lueDes = lueDes;
        }

        public setLookUpEdit(LookUpEdit lueDes, StringBuilder sqlSource, string DisplayMember = "", string ValueMember = "")
        {
            this._sqlSource = sqlSource;
            this._lueDes = lueDes;
            this._DisplayMember = DisplayMember;
            this._ValueMember = ValueMember;
        }

        public setLookUpEdit(LookUpEdit lueDes, StringBuilder sqlSource, string Conn, string DisplayMember = "", string ValueMember = "")
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._lueDes = lueDes;
            this._DisplayMember = DisplayMember;
            this._ValueMember = ValueMember;
        }

        public void ClearLookUpEdit()
        {
            // clear 
            object ds = this._lueDes.Properties.DataSource;
            this._lueDes.Properties.DataSource = null;
        }

        public void getData(bool BestFit = true)
        {
            this._lueDes.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSuggest;
            ClearLookUpEdit();

            DataTable dtRead = new DataTable();
            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            if (this._lueDes.Properties.Columns.Count > 0)
            { 
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._lueDes.Properties.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._lueDes.Properties.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            this._lueDes.EditValue = "";
            dtRead.Merge(dt);
            this._lueDes.Properties.DataSource = dtRead;
            this._lueDes.Properties.DisplayMember = this._DisplayMember;
            this._lueDes.Properties.ValueMember = this._ValueMember;
            if(BestFit == true)
            {
                this._lueDes.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
        }
    }

    //************* SEARCH LOOK UP EDIT *********************
    class setSearchLookUpEdit
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private SearchLookUpEdit _lueDes;
        private string _DisplayMember;
        private string _ValueMember;

        public setSearchLookUpEdit(SearchLookUpEdit lueDes)
        {
            this._lueDes = lueDes;
        }

        public setSearchLookUpEdit(SearchLookUpEdit lueDes, StringBuilder sqlSource, string DisplayMember = "", string ValueMember = "")
        {
            this._sqlSource = sqlSource;
            this._lueDes = lueDes;
            this._DisplayMember = DisplayMember;
            this._ValueMember = ValueMember;
        }

        public setSearchLookUpEdit(SearchLookUpEdit lueDes, StringBuilder sqlSource, string Conn, string DisplayMember = "", string ValueMember = "")
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._lueDes = lueDes;
            this._DisplayMember = DisplayMember;
            this._ValueMember = ValueMember;
        }

        public void ClearSearchLookUpEdit()
        {
            // clear 
            object ds = this._lueDes.Properties.DataSource;
            this._lueDes.Properties.DataSource = null;
        }

        public void getData(bool BestFit = true)
        {
            ClearSearchLookUpEdit();

            DataTable dtRead = new DataTable();
            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            if (this._lueDes.Properties.View.Columns.Count > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._lueDes.Properties.View.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._lueDes.Properties.View.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            this._lueDes.EditValue = "";
            dtRead.Merge(dt);
            this._lueDes.Properties.DataSource = dtRead;
            this._lueDes.Properties.DisplayMember = this._DisplayMember;
            this._lueDes.Properties.ValueMember = this._ValueMember;
            if (BestFit == true)
            {
                this._lueDes.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            }
        }

    }

    //************* TREE-LIST *******************
    class setTreeList
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private TreeList _tlDes;

        public setTreeList(TreeList tlDes)
        {
            this._tlDes = tlDes;
        }

        public setTreeList(TreeList tlDes, StringBuilder sqlSource)
        {
            this._sqlSource = sqlSource;
            this._tlDes = tlDes;
        }

        public setTreeList(TreeList tlDes, string Conn, StringBuilder sqlSource)
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._tlDes = tlDes;
        }

        public void resizeValueMaxLength(int iMaxLength = 200)
        {
            DataTable dtRead = new DataTable();
            dtRead = ((DataTable)this._tlDes.DataSource);
            foreach (DataColumn dc in dtRead.Columns)
            {
                try
                {
                    if (dtRead.Columns[dc.ColumnName].MaxLength < iMaxLength)
                    {
                        dtRead.Columns[dc.ColumnName].MaxLength = iMaxLength;
                    }
                    dtRead.Columns[dc.ColumnName].AllowDBNull = true;
                }
                catch (Exception)
                { }
            }

        }

        public DataTable resizeValueMaxLengthX(int iMaxLength = 200)
        {
            DataTable dtRead = new DataTable();

            dtRead = ((DataTable)this._tlDes.DataSource);
            foreach (DataColumn dc in dtRead.Columns)
            {
                try
                {
                    if (dtRead.Columns[dc.ColumnName].MaxLength < iMaxLength)
                    {
                        dtRead.Columns[dc.ColumnName].MaxLength = iMaxLength;
                    }
                    dtRead.Columns[dc.ColumnName].AllowDBNull = true;
                }
                catch (Exception)
                { }
            }

            return dtRead;
        }


        public void ClearTreeList()
        {
            this._tlDes.ClearNodes();
            this._tlDes.DataSource = null;
        }

        public void getData(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearTreeList();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._tlDes.ViewInfo.RowsInfo.Rows.Count > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            if (Continuous == true && this._tlDes.ViewInfo.RowsInfo.Rows.Count > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._tlDes.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._tlDes.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            dtRead.Merge(dt);

            this._tlDes.DataSource = dtRead;
            this._tlDes.EndUpdate();
            this._tlDes.ResumeLayout();
            this._tlDes.ClearSelection();
            this._tlDes.OptionsView.AutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._tlDes.BestFitColumns();
            }

            if (this._tlDes.ViewInfo.RowsInfo.Rows.Count > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._tlDes.MoveLast();
            }
            this._tlDes.ClearSelection();
        }

        public void getDataNodes(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false, bool Expand = false)
        {
            if (Continuous == false)
            {
                ClearTreeList();
            }

            DevExpress.XtraTreeList.Nodes.TreeListNode parentForRootNodes = null;
            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            if (this._tlDes.Columns.Count == 0)
            {
                TreeListColumn[] tlcHEAD = new TreeListColumn[1];
                for (int ii = 0; ii < dt.Columns.Count; ii++)
                {
                    try
                    {
                        TreeListColumn col = new TreeListColumn();
                        col.Caption = dt.Columns[ii].ColumnName;
                        col.VisibleIndex = ii;

                        System.Array.Resize(ref tlcHEAD, ii + 1);
                        tlcHEAD[ii] = col;
                    }
                    catch (Exception) { }
                }
                this._tlDes.Columns.AddRange(tlcHEAD);
            }

            string tmpNode = "";
            int zi = 0;
            TreeListNode[] rootNode = new TreeListNode[1];
            foreach (DataRow row in dt.Rows)
            {
                if (tmpNode.Trim() != row[0].ToString().Trim())
                {
                    string[] data = new string[dt.Columns.Count];
                    for (int zz = 0; zz < dt.Columns.Count; zz++)
                    {
                        data[zz] = row[zz].ToString();
                    }

                    System.Array.Resize(ref rootNode, zi + 1);
                    rootNode[zi] = this._tlDes.AppendNode(data, parentForRootNodes);
                    tmpNode = row[0].ToString().Trim();
                }
                else
                {
                    string[] data = new string[dt.Columns.Count];
                    for (int zz = 0; zz < dt.Columns.Count; zz++)
                    {
                        data[zz] = row[zz].ToString();
                    }
                    TreeListNode node1 = this._tlDes.AppendNode(data, rootNode[zi]);
                }
            }

            this._tlDes.EndUpdate();
            this._tlDes.ResumeLayout();
            this._tlDes.ClearSelection();
            this._tlDes.OptionsView.AutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._tlDes.BestFitColumns();
            }

            if (Expand == true)
            {
                this._tlDes.ExpandAll();
            }

            if (toLast == true)
            {
                this._tlDes.MoveLast();
            }
            this._tlDes.ClearSelection();
        }
    }

    //************* GRIDCONTROL *******************
    class setGridControl
    {
        private string _Conn;
        private StringBuilder _sqlSource;
        private GridControl _gcDes;
        private GridView _gvDes;
        private int _atColumn = 0;
        private DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs _evnPopupMenu;
        private RowStyleEventArgs _evnRowStyle;

        public setGridControl(GridControl gcDes, GridView gvDes)
        {
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = 0;
        }

        public setGridControl(GridControl gcDes, GridView gvDes, int atColumn)
        {
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = atColumn;
        }

        public setGridControl(GridControl gcDes, GridView gvDes, StringBuilder sqlSource)
        {
            this._sqlSource = sqlSource;
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = 0;
        }

        public setGridControl(GridControl gcDes, GridView gvDes, string Conn, StringBuilder sqlSource)
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = 0;
        }

        public setGridControl(GridControl gcDes, GridView gvDes, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs evnPopupMenu)
        {
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = 0;
            this._evnPopupMenu = evnPopupMenu;
        }

        public setGridControl(GridControl gcDes, GridView gvDes, RowStyleEventArgs evnRowStyle)
        {
            this._gcDes = gcDes;
            this._gvDes = gvDes;
            this._atColumn = 0;
            this._evnRowStyle = evnRowStyle;
        }

        public void resizeValueMaxLength(int iMaxLength = 200)
        {
            DataTable dtRead = new DataTable();
            dtRead = ((DataView)this._gvDes.DataSource).Table;
            foreach (DataColumn dc in dtRead.Columns)
            {
                try
                {
                    if (dtRead.Columns[dc.ColumnName].MaxLength < iMaxLength)
                    {
                        dtRead.Columns[dc.ColumnName].MaxLength = iMaxLength;
                    }
                    dtRead.Columns[dc.ColumnName].AllowDBNull = true;
                }
                catch (Exception)
                { }
            }
        }

        public DataTable resizeValueMaxLengthX(int iMaxLength = 200)
        {
            DataTable dtRead = new DataTable();
            dtRead = ((DataView)this._gvDes.DataSource).Table;
            foreach (DataColumn dc in dtRead.Columns)
            {
                try
                {
                    if (dtRead.Columns[dc.ColumnName].MaxLength < iMaxLength)
                    {
                        dtRead.Columns[dc.ColumnName].MaxLength = iMaxLength;
                    }
                    dtRead.Columns[dc.ColumnName].AllowDBNull = true;
                }
                catch (Exception)
                { }
            }
            return dtRead;
        }

        public void ClearGrid()
        {
            // clear 
            object ds = this._gcDes.DataSource;
            this._gcDes.DataSource = null;
            GridView view = new GridView(this._gcDes);
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowColumnHeaders = false;
            this._gcDes.MainView = view;

            // restore  
            this._gcDes.MainView = this._gvDes;
            this._gcDes.DataSource = ds;
        }

        public void getData(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._gvDes.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._gvDes.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            dtRead.Merge(dt);

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }

            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }

        public void getDataTableExistsColumn(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            try
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    this._gvDes.Columns[i].FieldName = dt.Columns[i].ColumnName;
                }
            }
            catch (Exception)
            { }

            dtRead.Merge(dt);

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }

            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();

        }

        public void getDataShowOrder(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }


            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
            dt.Columns.Add("NO", typeof(String)).SetOrdinal(0);

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    dt.Rows[x][0] = (x + 1).ToString();
                }
                catch (Exception) { }
                x++;
            }


            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._gvDes.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._gvDes.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
                
            }

            dtRead.Merge(dt);

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                x = 0;
                foreach (DataRow row in dtRead.Rows)
                {
                    try
                    {
                        dtRead.Rows[x][0] = (x + 1).ToString();
                    }
                    catch (Exception) { }
                    x++;
                }
            }

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }

            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();

        }

        public void getDataShowOrderTableExistsColumn(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
  
            if (!dt.Columns.Contains("RowID"))
            {
                dt.Columns.Add("RowID", typeof(String)).SetOrdinal(0);
            }

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    dt.Rows[x][0] = (x + 1).ToString();
                }
                catch (Exception) { }
                x++;
            }
          
            try
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    this._gvDes.Columns[i].FieldName = dt.Columns[i].ColumnName;
                }
            }
            catch (Exception)
            { }

            dtRead.Merge(dt);

            if (Continuous == true)
            {
                x = 0;
                foreach (DataRow row in dtRead.Rows)
                {
                    try
                    {
                        dtRead.Rows[x][0] = (x + 1).ToString();
                    }
                    catch (Exception) { }
                    x++;
                }
            }

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }
            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }

        public void getDataShowCheckBoxAndOrder(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
            dt.Columns.Add("NO", typeof(String)).SetOrdinal(0);
            dt.Columns.Add("Check", typeof(Boolean)).SetOrdinal(0);

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    dt.Rows[x][0] = false;
                    dt.Rows[x][1] = (x + 1).ToString();
                }
                catch (Exception) { }
                x++;
            }

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._gvDes.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._gvDes.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            dtRead.Merge(dt);

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                x = 0;
                foreach (DataRow row in dtRead.Rows)
                {
                    try
                    {
                        dtRead.Rows[x][0] = false;
                        dtRead.Rows[x][1] = (x + 1).ToString();
                    }
                    catch (Exception) { }
                    x++;
                }
            }

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }
            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }

        public void getDataShowCheckBoxAndOrderTableExistsColumn(bool Continuous = false, bool Checked = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {

            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();
            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            dt.Columns.Add("NO", typeof(String)).SetOrdinal(0);
            dt.Columns.Add("Check", typeof(Boolean)).SetOrdinal(0);

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                dt.Rows[x][0] = Checked;
                dt.Rows[x][1] = (x + 1).ToString();
                x++;
            }
            if (x >= 2)
            {
                dt.Columns[2].ReadOnly = false;
            }

            try
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    this._gvDes.Columns[i].FieldName = dt.Columns[i].ColumnName;
                }
            }
            catch (Exception)
            { }

            dtRead.Merge(dt);

            if (Continuous == true)
            {
                x = 0;
                foreach (DataRow row in dtRead.Rows)
                {
                    dtRead.Rows[x][1] = (x + 1).ToString();
                    x++;
                }
            }

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }
            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }

        public void getDataCheckBox(bool Continuous = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {
            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();
            dt.Columns.Add("Check", typeof(Boolean)).SetOrdinal(0);

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                dt.Rows[x][0] = false;
                x++;
            }

            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int ii = 0; ii < this._gvDes.Columns.Count; ii++)
                {
                    try
                    {
                        dt.Columns[ii].ColumnName = this._gvDes.Columns[ii].FieldName;
                        dt.Columns[ii].AllowDBNull = true;
                    }
                    catch (Exception) { }
                    ii++;
                }
            }

            dtRead.Merge(dt);

            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }
            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }

        public void getDataCheckBoxTableExistsColumn(bool Continuous = false, bool Checked = false, bool toLast = true, bool ColumnAutoWidth = false, bool BestFit = false)
        {

            if (Continuous == false)
            {
                ClearGrid();
            }

            DataTable dtRead = new DataTable();
            if (Continuous == true && this._gvDes.DataRowCount > 0)
            {
                dtRead = resizeValueMaxLengthX();
            }

            DataTable dt = new DBQuery(this._sqlSource, this._Conn).getDataTable();

            dt.Columns.Add("Check", typeof(Boolean)).SetOrdinal(0);

            int x = 0;
            foreach (DataRow row in dt.Rows)
            {
                dt.Rows[x][0] = false;
                x++;
            }
            if (x >= 2)
            {
                dt.Columns[1].ReadOnly = false;
            }

            try
            {
                //เปลี่ยนชื่อ Column ใน DataTable ให้ตรงกับ DataGridView
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    this._gvDes.Columns[i].FieldName = dt.Columns[i].ColumnName;
                }
            }
            catch (Exception)
            { }

            dtRead.Merge(dt);
            this._gcDes.DataSource = dtRead;
            this._gcDes.EndUpdate();
            this._gcDes.ResumeLayout();
            this._gvDes.ClearSelection();
            this._gvDes.OptionsView.ColumnAutoWidth = ColumnAutoWidth;
            if (BestFit == true)
            {
                this._gvDes.BestFitColumns();
            }
            //this._dgvDes.Update(); 
            if (this._gvDes.DataRowCount > 0)
            {
                resizeValueMaxLength();
            }

            if (toLast == true)
            {
                this._gvDes.MoveLast();
            }
            this._gvDes.ClearSelection();
        }


        //********** CAL COLUMN ******************
        public void ShowCountColumn(string ColumnName, int Dicimal = 0, string FrontText = "", string BackText = "")
        {
            //Set up a summary on the "yourfieldname" column  
            this._gvDes.Columns[ColumnName].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this._gvDes.Columns[ColumnName].SummaryItem.FieldName = "tt" + ColumnName;
            this._gvDes.Columns[ColumnName].SummaryItem.DisplayFormat = FrontText + " {0:n" + Dicimal + "} " + BackText;
        }

        public void ShowSumColumn(string ColumnName, int Dicimal = 0, string FrontText = "", string BackText = "")
        {
            //Set up a summary on the "yourfieldname" column  
            this._gvDes.Columns[ColumnName].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this._gvDes.Columns[ColumnName].SummaryItem.FieldName = "sum" + ColumnName;
            this._gvDes.Columns[ColumnName].SummaryItem.DisplayFormat = FrontText + " {0:n" + Dicimal + "} " + BackText;
        }

        //********** BACKGROUND ******************
        public void BackGroundWithColumn(string ColumnColorName)
        {
            string BG = this._gvDes.GetRowCellDisplayText(this._evnRowStyle.RowHandle, ColumnColorName);
            this._evnRowStyle.Appearance.BackColor = System.Drawing.Color.FromName(BG);
        }

        //********** RIGHT CLICK ******************
        //COPY CELL BEGIN
        public void RightClickMenu()
        {
            GridView view = this._gvDes as GridView;
            if (this._evnPopupMenu.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                int rowHandle = this._evnPopupMenu.HitInfo.RowHandle;
                // Delete existing menu items, if any.
                this._evnPopupMenu.Menu.Items.Clear();
                // Add the 'Cell Copy' check menu item.
                this._evnPopupMenu.Menu.Items.Add(CreateMenuItemCopy(view, rowHandle));
                // Add the Rows submenu with the 'Delete Row' command
                this._evnPopupMenu.Menu.Items.Add(CreateSubMenuRows(view, rowHandle));
                // Add the 'Cell Merging' check menu item.
                DXMenuItem item = CreateMenuItemCellMerging(view, rowHandle);
                item.BeginGroup = true;
                this._evnPopupMenu.Menu.Items.Add(item);
            }
        }

        DXMenuItem CreateMenuItemCopy(GridView view, int rowHandle)
        {
            DXMenuItem menuItemCopy = new DXMenuItem("Copy", new EventHandler(OnCopy));
            menuItemCopy.Tag = new RowInfo(view, rowHandle);
            menuItemCopy.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);
            return menuItemCopy;
        }

        DXMenuItem CreateSubMenuRows(GridView view, int rowHandle)
        {
            DXSubMenuItem subMenu = new DXSubMenuItem("Rows");

            DXMenuItem menuItemCopyRow = new DXMenuItem("Copy this row", new EventHandler(OnCopyRowClick));
            menuItemCopyRow.Tag = new RowInfo(view, rowHandle);
            menuItemCopyRow.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);
            subMenu.Items.Add(menuItemCopyRow);

            DXMenuItem menuItemCopyTableRow = new DXMenuItem("Copy this table", new EventHandler(OnCopyTableClick));
            menuItemCopyTableRow.Tag = new RowInfo(view, rowHandle);
            menuItemCopyTableRow.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);
            subMenu.Items.Add(menuItemCopyTableRow);

            string deleteRowsCommandCaption;
            if (view.IsGroupRow(rowHandle))
                deleteRowsCommandCaption = "Delete rows in this group";
            else
                deleteRowsCommandCaption = "Delete this row";
            DXMenuItem menuItemDeleteRow = new DXMenuItem(deleteRowsCommandCaption, new EventHandler(OnDeleteRowClick));
            menuItemDeleteRow.Tag = new RowInfo(view, rowHandle);
            menuItemDeleteRow.Enabled = view.IsDataRow(rowHandle) || view.IsGroupRow(rowHandle);
            subMenu.Items.Add(menuItemDeleteRow);
            return subMenu;
        }

        DXMenuCheckItem CreateMenuItemCellMerging(GridView view, int rowHandle)
        {
            DXMenuCheckItem checkItem = new DXMenuCheckItem("Cell Merging", view.OptionsView.AllowCellMerge, null, new EventHandler(OnCellMergingClick));
            checkItem.Tag = new RowInfo(view, rowHandle);
            //checkItem.ImageOptions.Image = imageCollection1.Images[1];
            return checkItem;
        }

        void OnCopy(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;

            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                //ri.View.CopyToClipboard(); //ก๊อปปี้ทั้งแถว
                Clipboard.SetText(this._gvDes.GetRowCellValue(this._gvDes.FocusedRowHandle, this._gvDes.FocusedColumn).ToString()); //ก๊อปปี้เซล์เดียว
            }
        }

        void OnCopyRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;

            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                ri.View.CopyToClipboard(); //ก๊อปปี้ทั้งแถว
            }
        }

        void OnCopyTableClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;

            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                this._gvDes.OptionsSelection.MultiSelect = true;
                this._gvDes.SelectAll();
                this._gvDes.CopyToClipboard();
                this._gvDes.OptionsSelection.MultiSelect = false;
            }
        }

        void OnDeleteRowClick(object sender, EventArgs e)
        {
            DXMenuItem menuItem = sender as DXMenuItem;
            RowInfo ri = menuItem.Tag as RowInfo;
            if (ri != null)
            {
                string message = menuItem.Caption.Replace("&", "");
                if (XtraMessageBox.Show(message + " ?", "Confirm operation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                ri.View.DeleteRow(ri.RowHandle);
            }
        }

        void OnCellMergingClick(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            RowInfo info = item.Tag as RowInfo;
            info.View.OptionsView.AllowCellMerge = item.Checked;
        }

        class RowInfo
        {
            public RowInfo(GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }
            public GridView View;
            public int RowHandle;
        }
        //*****************************************
    }
}
