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
    public partial class GridCtrnTest : DevExpress.XtraEditors.XtraForm
    {
        public GridCtrnTest()
        {
            InitializeComponent();
        }

        private void GridCtrnTest_Load(object sender, EventArgs e)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT MachineName, InstallationID, InstanceName FROM Keys");
            new ObjDevEx.setGridControl(gridControl1, bandedGridView1, sbSQL).getDataCheckBoxTableExistsColumn(false, false, false, false);

            new ObjDevEx.setGridControl(gridControl2, gridView1, sbSQL).getDataCheckBox(false, false, false, false);

            bandedGridView1.OptionsView.ShowFooter = true;
            bandedGridView1.FooterPanelHeight = 70;


            bandedGridView1.OptionsFind.AlwaysVisible = true;
            //bandedGridView1.ApplyFindFilter("Blue");
        }


        private void gridControl1_Load(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.VScrollBar vScrollBar = gridControl1.Controls.OfType<DevExpress.XtraEditors.VScrollBar>().FirstOrDefault();
            //if (vScrollBar != null)
            //{
            //    vScrollBar.LookAndFeel.UseDefaultLookAndFeel = false;
            //    vScrollBar.LookAndFeel.UseWindowsXPTheme = true;
            //    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo = gridView1.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            //    vScrollBar.Location = new Point(vScrollBar.Location.X, vScrollBar.Location.Y - viewInfo.ViewRects.ColumnPanel.Height);
            //    vScrollBar.Height += viewInfo.ViewRects.ColumnPanel.Height;
            //    vScrollBar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            //}
        }

        private void gridControl1_SizeChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.VScrollBar vScrollBar = gridControl1.Controls.OfType<DevExpress.XtraEditors.VScrollBar>().FirstOrDefault();
            //if (vScrollBar != null)
            //{
            //    vScrollBar.Anchor = AnchorStyles.None;
            //    gridView1.LayoutChanged();
            //    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo viewInfo = gridView1.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
            //    vScrollBar.Location = new Point(vScrollBar.Location.X, vScrollBar.Location.Y - viewInfo.ViewRects.ColumnPanel.Height);
            //    vScrollBar.Height += viewInfo.ViewRects.ColumnPanel.Height;
            //    vScrollBar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            //}
        }

       

        Color highPriority = Color.Green;
        Color normalPriority = Color.Orange;
        Color lowPriority = Color.Red;
        int markWidth = 16;


        private void bandedGridView1_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int offset = 5;
            e.DefaultDraw();
            Color color = highPriority;
            Rectangle markRectangle;
            string priorityText = " - High level";
            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                {
                    color = normalPriority;
                    priorityText = " - Normal level";
                }
                else if (i == 2)
                {
                    color = lowPriority;
                    priorityText = " - Low level";
                }
                markRectangle = new Rectangle(e.Bounds.X + offset, e.Bounds.Y + offset + (markWidth + offset) * i, markWidth, markWidth);
                e.Cache.FillEllipse(markRectangle.X, markRectangle.Y, markRectangle.Width, markRectangle.Height, color);
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                e.Appearance.Options.UseTextOptions = true;
                e.Appearance.DrawString(e.Cache, priorityText, new Rectangle(markRectangle.Right + offset, markRectangle.Y, e.Bounds.Width, markRectangle.Height));
            }

        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            e.Appearance.Options.UseTextOptions = true;
            e.DefaultDraw();
            if (e.Column.FieldName == "ID")
            {
                Color color;
                int cellValue = Convert.ToInt32(e.CellValue);
                if (cellValue < 3)
                    color = highPriority;
                else if (cellValue > 2 && cellValue < 5)
                    color = normalPriority;
                else
                    color = lowPriority;
                e.Cache.FillEllipse(e.Bounds.X + 1, e.Bounds.Y + 1, markWidth, markWidth, color);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gridView1 != null)
            {
                gridView1.OptionsPrint.ExpandAllDetails = true;
                gridView1.ExportToPdf("MainViewData.pdf");
                //gridView1.ExportToXlsx("XX.xlsx");
                System.Diagnostics.Process.Start("MainViewData.pdf");
            }
        }
    }


}