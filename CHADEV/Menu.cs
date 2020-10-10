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
using DevExpress.Utils.Svg;
using CHADEV.Properties;
using DevExpress.XtraGrid.Views.Tile;

namespace CHADEV
{
    public partial class Menu : DevExpress.XtraEditors.XtraForm
    {
        StringBuilder sbSQL = new StringBuilder();
        public Menu()
        {
            InitializeComponent();
            sbSQL.Append("SELECT DISTINCT Season FROM Model WHERE (LEN(Season) = 4) ORDER BY Season");
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            new ObjDevEx.setImageComboboxEdit(imageComboBoxEdit1, sbSQL).getDataRange(imageCollection2);
            new ObjDevEx.setImageComboboxEdit(imageComboBoxEdit1).AddItems(imageCollection2, "YYYYYYYYY", 0);
            new ObjDevEx.setImageComboboxEdit(imageComboBoxEdit1).insertItems(imageCollection2, "XXXXXXXXXXX", 0, 2);

            new ObjDevEx.setDateEdit(dateEdit1).setStyle();
            new ObjDevEx.setDateEdit(dateEdit2).setStyle("MMMM yyyy", "MY");
            new ObjDevEx.setDateEdit(dateEdit3).setStyle("yyyy", "Y");

            stepProgressBar1.SelectedItemIndex = 2;

            radioGroup1.SelectedIndex = 0;

            searchControl1.Client = listBoxControl1;

            string[] myColors = {
                        Color.Black.Name,
                        Color.Blue.Name,
                        Color.Brown.Name,
                        Color.Green.Name,
                        Color.Red.Name,
                        Color.Yellow.Name,
                        Color.Orange.Name
                     };
            listBoxControl1.Items.AddRange(myColors);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            popupContainerEdit1.Text = textEdit1.Text;
            //Control button = sender as Control;
            //(button.Parent as PopupContainerControl).OwnerEdit.ClosePopup();
            popupContainerControl1.OwnerEdit.ClosePopup();
        }

        private void popupContainerEdit1_EditValueChanged(object sender, EventArgs e)
        {
            textEdit1.Text = "";
        }

        private void buttonEdit1_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ButtonEdit editor = (ButtonEdit)sender;
            int buttonIndex = editor.Properties.Buttons.IndexOf(e.Button);
            if (buttonIndex == 0) 
            {
                MessageBox.Show(buttonEdit1.Text);
            }
            else if (buttonIndex == 1)
            {
                buttonEdit1.Text = "";
            }
            else if (buttonIndex == 2)
            {
                buttonEdit1.Text = "";
                try
                {
                    xtraOpenFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    xtraOpenFileDialog1.FileName = "";
                    xtraOpenFileDialog1.Title = "Select Excel File";
                    xtraOpenFileDialog1.ShowDialog();
                    buttonEdit1.Text = xtraOpenFileDialog1.FileName;
                }
                catch (Exception)
                { }
                
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateEdit1.Text);
            MessageBox.Show(dateEdit2.Text);
            MessageBox.Show(dateEdit3.Text);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value.ToString());

        }
    }
}