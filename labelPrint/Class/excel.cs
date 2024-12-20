using OfficeOpenXml;
using OfficeOpenXml.Table;
using labelPrint.Class;
using labelPrint.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using BoresSoft.Ftp;

namespace labelPrint.Class
{
    public class excel
    {
        private String m_path;
        private DataTable m_book;
        private Utils m_utils;
        public string networkPath = @"\\172.25.0.31\SEMDJPCBLinkage\";
        NetworkCredential credentials = new NetworkCredential(@"simossem", @"simossem1");
        public string myNetworkPath = string.Empty;

        public string Path
        {
            get{ return m_path; }
            set{ m_path = value; }
        }
        public DataTable Book
        {
            get{ return m_book; }
            set{ m_book = value; }
        }

        public excel()
        {
            m_path = "";
            m_book = new DataTable();
            m_utils = new Utils();
        }
        public excel(String path)
        {
            m_path = path;
            m_book = new DataTable();
            m_utils = new Utils();
        }
        public bool write_fileOLE(DataTable dt, String fileName, string path)
        {
            bool result = false;

            string finalFileNameWithPath = string.Empty;
            string finalFileNameWithPathXML = string.Empty;
            String file = "";

            int mes = DateTime.Now.Month;
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            //fileName = path + fileName;
            string strMonthName = mes.ToString();//mfi.GetMonthName(mes).ToString();
            m_utils.translateM(ref strMonthName);
            //fileName = string.Format("{0}{1}", fileName, DateTime.Now.ToString("yyyy"));
            try
            {
                m_utils.createDirectory(path + "\\" + strMonthName);
            }
            catch (Exception ex)
            {

            }

            finalFileNameWithPath = path + "\\" + strMonthName + "\\DJ_" + fileName.Trim();
            //Delete existing file with same file name.
            file = "DJ_" + fileName.Trim();
            if (File.Exists(finalFileNameWithPath))
                File.Delete(finalFileNameWithPath);

            var newFile = new FileInfo(finalFileNameWithPath);

            //Step 1 : Create object of ExcelPackage class and pass file path to constructor.
            using (var package = new ExcelPackage(newFile))
            {
                //Step 2 : Add a new worksheet to ExcelPackage object and give a suitable name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoja 1");

                //Step 3 : Start loading datatable form A1 cell of worksheet.
                worksheet.PrinterSettings.HeaderMargin = 0.5M;
                worksheet.PrinterSettings.LeftMargin = 0.35M;
                worksheet.PrinterSettings.BottomMargin = 0.5M;
                worksheet.PrinterSettings.RightMargin = 0.3M;

                worksheet.PrinterSettings.PaperSize = ePaperSize.Letter;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                //ExcelFont font = new ExcelFont();


                worksheet.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Medium4);
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.AutoFilter = false;


                //Step 4 : (Optional) Set the file properties like title, author and subject
                package.Workbook.Properties.Title = @"Layout DJ Number";
                package.Workbook.Properties.Author = "SIIX EMS MEXICO, II. José Antonio Hernández García.";
                package.Workbook.Properties.Subject = @"Departamento de TI.";
                //package

                //Step 5 : Save all changes to ExcelPackage object which will create Excel 2007 file.
                package.Save();

                //MessageBox.Show(string.Format("Archivo '{0}' generado éxitosamente.", fileName)
                //    , "Archivo generado con éxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                path = finalFileNameWithPath;
                //System.IO.File.Copy(finalFileNameWithPath, @"\\172.25.0.31\SEMDJPCBLinkage", true);
                FileUpload(finalFileNameWithPath,file);
            }
            return result;
        }
        public void exportToCSV(DataTable sourceTable, TextWriter writer, bool includeHeaders)
        {
            if (includeHeaders)
            {
                IEnumerable<String> headerValues = sourceTable.Columns
                    .OfType<DataColumn>()
                    .Select(column => QuoteValue(column.ColumnName));

                writer.WriteLine(String.Join(",", headerValues));
            }

            IEnumerable<String> items = null;

            foreach (DataRow row in sourceTable.Rows)
            {
                //items = row.ItemArray.Select(o => QuoteValue(o.ToString()));
                items = row.ItemArray.Select(o => o.ToString());
                writer.WriteLine(String.Join(",", items));
            }

            writer.Flush();
        }

        public void FileUpload(string filePath, String file)
        {

            ConnectToSharedFolder m_simos = null;
            try
            {
                using (new ConnectToSharedFolder(networkPath, credentials))
                {
                    File.Copy(filePath + file, networkPath);
                }
                //using (FtpClient ftp = new FtpClient())
                //{
                //    ftp.Connect("172.25.0.31");
                //    ftp.CredentialRequired += ftp_CredentialRequired;
                //    ftp.LogOn(false);
                //    ftp.Upload(filePath);
                //}
                //m_simos = new ConnectToSharedFolder(networkPath, credentials);
                //File.Copy(filePath + file, networkPath);
                //m_simos.Dispose();
            }
            catch (Exception ex)
            {
                //m_simos.Dispose();
            }

        }
        void ftp_CredentialRequired(object sender, CredentialRequiredEventArgs e)
        {
            if (e.Password)
                e.Credential = "simossem1";
            else
                e.Credential = "simossem";
        }
        private string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }
        //public void TestConnectivity_Using_Credentials()
        //{
        //    try
        //    {
        //        System.Net.NetworkCredential readCredentials = new NetworkCredential(@"Domain\UserName", "Password");

        //        string filepath = @"\\Servername\DevTest\MyFiles";
        //        using (new NetworkConnection(filepath, readCredentials))
        //        {
        //            File.Copy(@"\\Servername\DevTest\MyFiles\XXX.txt",@"\\Servername\DevTest\MyFiles\XXX-Copy.txt");
        //        }
        //        //Assert.AreEqual<string>("01", "01", "Success");
        //    }
        //    catch
        //    {
        //        //assert.Fail();
        //    }
        //}

        #region excel
        public bool checkSheetOLE(DataTable info, ref string TextFail)
        {

            bool result = true;

            if (info.Columns.Count != 28)////conversa
            //if (info.Columns.Count != 12)///sto
            {
                TextFail = "El número de columnas en la hoja de datos no es el correcto.";
                return false;
            }

            result = true;

            return result;
        }
        public DataTable ReadSheet(string sheetName)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string fileExtension = System.IO.Path.GetExtension(Path);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }
        public void loadBookExcel(/*ref DataTable listSheet*/)
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = Path;
            //listSheet = new DataTable();
            m_book.Columns.Add("TABLE_NAME");
            if (System.IO.Path.GetExtension(Path).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (System.IO.Path.GetExtension(Path).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                m_book.Clear();
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        m_book.Rows.Add(drSheet["TABLE_NAME"].ToString());
                    }
                }
            }
            //return listSheet;
        }
        #endregion

    }
}