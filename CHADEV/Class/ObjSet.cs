using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Deployment.Application;

namespace ObjSet
{
    //**************** MULTIPLE VALUES ***************
    class setMultiValues
    {
        private string _Conn;
        private StringBuilder _sqlSource;


        public setMultiValues(string Conn, StringBuilder sqlSource)
        {
            this._Conn = Conn;
            this._sqlSource = sqlSource;
        }

        public string[] getData()
        {
            string[] strings = new string[1];
            SqlDataReader drCol = new DBQuery(this._sqlSource, this._Conn).getDataReader();
            int cNum = drCol.FieldCount;
            Array.Resize(ref strings, cNum);
            drCol.Close();

            SqlDataReader drRead = new DBQuery(this._sqlSource, this._Conn).getDataReader();
            if (drRead.HasRows)
            {
                while (drRead.Read())
                {
                    for (int i = 0; i < cNum; i++)
                    {
                        strings[i] = drRead.GetValue(i).ToString();
                    }
                }
            }
            else
            {
                Array.Resize(ref strings, 0);
            }
            drRead.Close();

            return strings;
        }
    }


    //************ FOLDER ***********
    class Folder
    {
        private string _path;

        public Folder(string FolderName)
        {
            this._path = FolderName.Trim();

            if (this._path != "")
            {
                if (this._path.Substring(this._path.Length - 1, 1) != "\\")
                {
                    this._path = this._path + "\\";
                }
            }

            if (!System.IO.Directory.Exists(this._path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(this._path);
                }
                catch (Exception) { }
            }
        }

        public string GetPath()
        {
            return this._path;
        }
    }

    //************ TEXT FILE ***********
    class TextFile
    {
        private string _pathFile;
        private string _pathFolder;

        public TextFile(string PathFileName)
        {
            this._pathFile = PathFileName.Trim();
        }

        public TextFile(string FolderPath, string FileName)
        {
            this._pathFolder = new Folder(FolderPath).GetPath();
            this._pathFile = this._pathFolder + FileName.Trim();

        }

        //public bool CreateFile(bool FileConfig = false, bool ReplaceFile = false)
        //{
        //    try
        //    {
        //        if (!System.IO.File.Exists(this._pathFile))
        //        {
        //            System.IO.File.Create(this._pathFile).Dispose();
        //            if (FileConfig == true)
        //            {
        //                WriteDefault(ReplaceFile);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public bool WriteDefault(bool ReplaceFile = false)
        //{
        //    try
        //    {
        //        string DriverConn = CONFIG.DRIVER_CONNECTION;
        //        string User = CONFIG.USER_CONNECTION;
        //        string Password = CONFIG.PASSWORD_CONNECTION;
        //        string DataBase = CONFIG.DATABASE_CONNECTION;

        //        string strWRITE = DriverConn + Environment.NewLine + User + Environment.NewLine + Password + Environment.NewLine + DataBase;
        //        WriteFile(strWRITE, ReplaceFile);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public string ReadFile(int ReadLine = 0)
        {
            string retrunRead = "";
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(this._pathFile);
            while ((line = file.ReadLine()) != null)
            {
                if (ReadLine == 0)
                {
                    retrunRead += line + Environment.NewLine;
                }
                else
                {
                    if ((ReadLine - 1) == counter)
                    {
                        retrunRead = line;
                        break;
                    }
                    else
                    {
                        retrunRead = "";
                    }
                }
                counter++;
            }

            file.Close();

            return retrunRead;
        }

        public bool WriteFile(string DataWrite, bool ReplaceFile = false)
        {
            try
            {
                if (ReplaceFile == true)
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(this._pathFile))
                    {
                        writer.WriteLine(DataWrite);
                    }
                }
                else
                {
                    if (!System.IO.File.Exists(this._pathFile))
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(this._pathFile))
                        {
                            writer.WriteLine(DataWrite);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int CountLine()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(this._pathFile);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Trim() != "")
                {
                    counter++;
                }
            }

            file.Close();

            return counter;
        }

    }

    //************ EXCEL ***********
    class Excel
    {
        private string _pathFile;
        private string _sheetName;

        public Excel(string pathFile, string sheetName)
        {
            this._pathFile = pathFile;
            this._sheetName = sheetName.Trim();
        }

        public DataTable ReadExcelToDatatble(int HeaderLine = 1, int ColumnStart = 1)
        {
            System.Data.DataTable dataTable = new DataTable();
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range range;
            try
            {
                // Get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Open(this._pathFile);
                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)
                                      excelworkBook.Worksheets.Item[this._sheetName];
                range = excelSheet.UsedRange;
                int cl = range.Columns.Count;
                // loop through each row and add values to our sheet
                int rowcount = range.Rows.Count; ;
                //create the header of table
                for (int j = ColumnStart; j <= cl; j++)
                {
                    dataTable.Columns.Add(Convert.ToString
                                         (range.Cells[HeaderLine, j].Value2), typeof(string));
                }
                //filling the table from  excel file                
                for (int i = HeaderLine + 1; i <= rowcount; i++)
                {
                    DataRow dr = dataTable.NewRow();
                    for (int j = ColumnStart; j <= cl; j++)
                    {

                        dr[j - ColumnStart] = Convert.ToString(range.Cells[i, j].Value2);
                    }

                    dataTable.Rows.InsertAt(dr, dataTable.Rows.Count + 1);
                }

                //now close the workbook and make the function return the data table

                excelworkBook.Close();
                excel.Quit();
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                excelSheet = null;
                range = null;
                excelworkBook = null;
            }
        }
    }

}
