using labelPrint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace labelPrint.Class
{
    public class Utils
    {
        private excel m_excel;
        private siixsem_main_dbEntities m_db = new siixsem_main_dbEntities();

        public void translateM(ref String month)
        {
            month = month.Replace("January", "Enero");
            month = month.Replace("February", "Febrero");
            month = month.Replace("March", "Marzo");
            month = month.Replace("April", "Abril");
            month = month.Replace("May", "Mayo");
            month = month.Replace("June", "Junio");
            month = month.Replace("July", "Julio");
            month = month.Replace("August", "Agosto");
            month = month.Replace("September", "Septiembre");
            month = month.Replace("October", "Octubre");
            month = month.Replace("November", "Noviembre");
            month = month.Replace("December", "Diciembre");
            month = month.Replace("01", "Enero");
            month = month.Replace("02", "Febrero");
            month = month.Replace("03", "Marzo");
            month = month.Replace("04", "Abril");
            month = month.Replace("05", "Mayo");
            month = month.Replace("06", "Junio");
            month = month.Replace("07", "Julio");
            month = month.Replace("08", "Agosto");
            month = month.Replace("09", "Septiembre");
            month = month.Replace("10", "Octubre");
            month = month.Replace("11", "Noviembre");
            month = month.Replace("12", "Diciembre");
            month = month.Replace("1", "Enero");
            month = month.Replace("2", "Febrero");
            month = month.Replace("3", "Marzo");
            month = month.Replace("4", "Abril");
            month = month.Replace("5", "Mayo");
            month = month.Replace("6", "Junio");
            month = month.Replace("7", "Julio");
            month = month.Replace("8", "Agosto");
            month = month.Replace("9", "Septiembre");
        }

        public static int GetRandomValue(int LowerBound, int UpperBound)
        {
            Random rnd = new Random();
            return rnd.Next(LowerBound, UpperBound);
        }

        /// <summary>
        /// sends back a date/time +/- 15 days from todays date
        /// </summary>
        public static DateTime GetRandomAppointmentTime(bool GoBackwards, bool Today)
        {
            Random rnd = new Random(Environment.TickCount); // set a new random seed each call
            var baseDate = DateTime.Today;
            if (Today)
                return new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, rnd.Next(9, 18), rnd.Next(1, 6) * 5, 0);
            else
            {
                int rndDays = rnd.Next(1, 16);
                if (GoBackwards)
                    rndDays = rndDays * -1; // make into negative number
                return new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, rnd.Next(9, 18), rnd.Next(1, 6) * 5, 0).AddDays(rndDays);
            }
        }
        #region directories

        public bool createDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return false;
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                //MessageBox.Show("Ocurrio un error al crear el directorio:" + e.Message);
                return false;
            }
            finally { }
            return true;
        }

        #endregion
        public bool isEarlier(DateTime date1, DateTime date2)
        {
            bool result = false;
            int resultC = DateTime.Compare(date1,date2);

            if (resultC < 0)
                result = true;
            return result;
        }
        public bool isEqual(DateTime date1, DateTime date2)
        {
            bool result = false;
            int resultC = DateTime.Compare(date1, date2);

            if (resultC == 0)
                result = true;
            return result;
        }
        public bool isLater(DateTime date1, DateTime date2)
        {
            bool result = false;
            int resultC = DateTime.Compare(date1, date2);

            if (resultC > 0)
                result = true;

            return result;
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //String fieldName = prop.Name.Replace("_", "_");
                int r = 0;
                //try
                //{
                //    if(int.TryParse(fieldName.ElementAt(fieldName.Length-1).ToString(), out r))
                //        fieldName = fieldName.Substring(0, fieldName.Length - 1);
                //}
                //catch (Exception ex) { }
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public bool updatePartNumber(String ruta, ref string info)
        {
            DataTable tmp, dthoja;
            int id_modelo = 0, id_side = 0;
            String partNumber;

            bool result = false;

            m_excel = new excel(ruta);
            m_excel.loadBookExcel();
            tmp = m_excel.Book;
            foreach (DataRow hoja in tmp.Rows)
            {
                dthoja = m_excel.ReadSheet(Convert.ToString(hoja["TABLE_NAME"]));
                if (dthoja.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow fila in dthoja.Rows)
                    {
                        try
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(fila[0])) && !String.IsNullOrEmpty(Convert.ToString(fila[1])) && !String.IsNullOrEmpty(Convert.ToString(fila[2])))
                            {
                                id_modelo = Convert.ToInt32(fila[0]);
                                partNumber = Convert.ToString(fila[2]);
                                id_side = Convert.ToInt32(fila[1]);
                                int response = m_db.updatePartNumber(id_modelo, id_side, partNumber).First().RESULT;

                                if (response == 1)
                                    result = true;
                                else
                                {
                                    if (response == -1)
                                        info = "No existe el lado.";
                                    else info = "No existe el modelo.";
                                    result = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            result = false;
                            info = ex.Message + " - " + ex.InnerException;
                        }
                        i++;
                    }
                    if (i == dthoja.Rows.Count) result = true;
                }
            }
            return result;
        }
        public String getHtmlLbl(String fileName)
        {
            return "<div class='columnprev'>" +
                    "     <div class='lblBox10x10'><center><img class='prevLbl' src='/aprinting/Labels/" + fileName + "' style='width: 100%;'></center></div>" +
                    "</div>";
        }
        public String getRowTbl(String codel, String coder)
        {
            return "<td>" + codel + "</td>" +
                   "<td>" + coder + "</td>";
        }
        
        #region setZPL

        public byte[] getBytesString(generate_lblCode_template2_Result template, String line2, String strZpl)
        {
            if (template.idModel == 92 || template.idModel == 216 || template.idModel == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1);

            }
            if (template.idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", template.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", line2);
            }

            if (template.idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", template.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", line2);
            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", template.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);

            return Encoding.UTF8.GetBytes(strZpl);

        }

        public byte[] getBytesString(generate_lblCode_templateLed2_Result template, String line2, String strZpl)
        {
            if (template.idModel == 117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
            }
            if (template.idModel == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
            }
            if (template.idModel == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
            }
            if (template.idModel == 46)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1U611.Replace("XXXX", line2));
            }
            if (template.idModel == 167 || template.idModel == 168 || template.idModel == 169 || template.idModel == 170)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1U611);

            }
            if (template.idModel == 314)
            {
                strZpl = strZpl.Replace("_XXXXXXXXXXXX_", line2);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", template.line2);
            }
            else
            {
                strZpl = strZpl.Replace("_XXXXXXXXXXXX_", template.line1);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", line2);
            }
            if (template.idModel == 46 || template.idModel == 167 || template.idModel == 168 || template.idModel == 169 
                || template.idModel == 170 || template.idModel == 171 || template.idModel == 172 || template.idModel == 296)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", template.line1U611);
            }

            return Encoding.UTF8.GetBytes(strZpl);

        }

        public String replaceLabel(generate_lblCode_template2_Result templateL, generate_lblCode_template2_Result templateR, String lineL2, String lineR2, String strZpl)
        {
            if (templateL.idModel == 92 || templateL.idModel == 216 || templateL.idModel == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1);
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateR.line1);
            }
            if (templateL.idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (templateL.idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }

            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", templateR.line1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            return strZpl;
        }
        public String replaceLabelOSLed2(generate_lblCode_templateLed2_Result templateL, String lineL2, String strZpl)
        {
            if (templateL.idModel == 46 || templateL.idModel == 167 || templateL.idModel == 168 || templateL.idModel == 169
                || templateL.idModel == 170 || templateL.idModel == 171 || templateL.idModel == 172 || templateL.idModel == 296)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611);
            }
            if (templateL.idModel == 46)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611.Replace("XXXX", lineL2));
            }
            if (templateL.idModel == 167 || templateL.idModel == 168 || templateL.idModel == 169 || templateL.idModel == 170)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611);

            }

            if (templateL.idModel == 92 || templateL.idModel == 216 || templateL.idModel == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1);
            }
            if (templateL.idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (templateL.idModel == 117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
            }
            if (templateL.idModel == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
            }
            if (templateL.idModel == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
            }
            if (templateL.idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }
            if (templateL.idModel == 428 || templateL.idModel == 429 || templateL.idModel == 430 || templateL.idModel == 431 || templateL.idModel == 432 || templateL.idModel == 433)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAA_", templateL.line2.Substring(0, 5));
            }
            if (templateL.idModel == 314) {
                strZpl = strZpl.Replace("_XXXXXXXXXXXX_", lineL2);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", templateL.line2);
            }
            else
            {
                strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
                strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            }

            return strZpl;
        }

        public String replaceLabelOS(generate_lblCode_template2_Result templateL, String lineL2, String strZpl)
        {
            if (templateL.idModel == 92 || templateL.idModel == 216 || templateL.idModel == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1);
            }
            if (templateL.idModel == 128)
            {
                if (templateL.side_desc == "LH")
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                else
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (templateL.idModel == 136)
            {
                if(templateL.side_desc=="LH")
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                else
                    strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "114098A04S");  //115098A04S, 114098A04S
                //strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateL.line1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);

            return strZpl;
        }
        public String replaceLabel(generate_lblCode_template2_Result templateL, generate_lblCode_template2_Result templateR, String lineL2, String lineR2, String line3, String strZpl)
        {
            
            if (templateL.idModel == 313 || templateL.idModel == 316 || templateL.idModel == 317 || templateL.idModel == 318)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1);
                strZpl = strZpl.Replace("_BBBBBBBBBB_", line3);
            }

            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", templateR.line1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            return strZpl;
        }
        public String replaceLabel(generate_lblCode_templateLed2_Result templateL, generate_lblCode_templateLed2_Result templateR, String lineL2, String lineR2, String line3, String strZpl)
        {
            if (templateL.idModel == 117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0600");
            }
            if (templateL.idModel == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0590");
            }
            if (templateL.idModel == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "1144005");
                strZpl = strZpl.Replace("_DDDDDDDDDD_", "0580");
            }

            if (templateL.idModel == 46)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611.Replace("XXXX", lineL2));
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", templateR.line1U611.Replace("XXXX", lineR2));
            }
            if (templateL.idModel == 167 || templateL.idModel == 168 || templateL.idModel == 169 || templateL.idModel == 170 || templateL.idModel == 171 || templateL.idModel == 172)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611);
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", templateR.line1U611);
                strZpl = strZpl.Replace("_SSSSSSSSSSSS_", templateR.line1);
                strZpl = strZpl.Replace("_TTTTTTTTTT_", line3);
            }
            if(templateL.idModel == 296)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", templateL.line1U611);
            }

            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", templateL.line1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", templateR.line1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            return strZpl;
        }

        public String replaceLabelTr(generate_lblCode_templateLed2_Result templateL, generate_lblCode_templateLed2_Result templateR, String lineL2, String lineR2, String line3, String strZpl)
        {
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", lineL2);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", templateL.line2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", lineR2);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", templateR.line2);
            strZpl = strZpl.Replace("_AAAAAAAAAAAA_", line3);
            strZpl = strZpl.Replace("_BBBBBBBBBB_", templateL.line2);

            return strZpl;
        }

        public String replaceLabel(String lineL1, String lineR1, String lineL2, String lineR2, String strZpl, int idModel)
        {
            if (idModel == 92 || idModel == 216 || idModel == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1);
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineR1);
            }
            if (idModel == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "101598D022");  //101598D022, 100598D022
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineL1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (idModel == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "115098A04S");  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineL1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", lineL1);
            strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            strZpl = strZpl.Replace("_YYYYYYYYYYYY_", lineR1);
            strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            return strZpl;

        }

        public String replaceLabelV2(String lineL1, String lineL2, String strZpl, CModelPrint Model, getTemplateLblPrint_Result template)
        {
            if (Model.Id_model == 167 || Model.Id_model == 168 || Model.Id_model == 169
              ||Model.Id_model == 170 || Model.Id_model == 171 || Model.Id_model == 172 || Model.Id_model == 296)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1.Substring(0, 14));
            }
             if (Model.Id_model == 46)
            {
                
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1.Substring(7, 5) + lineL2.Substring(5,4));
            }
            if (Model.Id_model == 92 || Model.Id_model == 216 || Model.Id_model == 203)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1);
                //strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineR1);
            }
            if (Model.Id_model == 128)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1.Substring(0, 10));  //101598D022, 100598D022
                //strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "100598D022");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineL1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);

            }
            if (Model.Id_model == 136)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", lineL1.Substring(0, 10));  //115098A04S, 114098A04S
                strZpl = strZpl.Replace("_CCCCCCCCCCCC_", "114098A04S");
                strZpl = strZpl.Replace("_BBBBBBBBBBBB_", lineL1.Substring(10, 6));
                strZpl = strZpl.Replace("_DDDDDDDDDDDD_", lineL2);
            }
            if (Model.Id_model == 114)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0580");
            }
            if (Model.Id_model == 116)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0590");
            }
            if (Model.Id_model == 117)
            {
                strZpl = strZpl.Replace("_AAAAAAAAAAAA_", "1144005");
                strZpl = strZpl.Replace("_BBBBBBBBBB_", "0600");
            }
            //if (Model.Id_model == 314)
            //{
            //    strZpl = strZpl.Replace("_XXXXXXXXXXXX_", lineL2);
            //    strZpl = strZpl.Replace("_WWWWWWWWWW_", Model.line2);
            //}
            strZpl = strZpl.Replace("_XXXXXXXXXXXX_", lineL1);

            if (Model.Id_model == 46)
                strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2.Substring(5, 4));
            else
                strZpl = strZpl.Replace("_WWWWWWWWWW_", lineL2);
            //strZpl = strZpl.Replace("_YYYYYYYYYYYY_", lineR1);
            //strZpl = strZpl.Replace("_ZZZZZZZZZZ_", lineR2);

            strZpl = strZpl.Replace("_SERTEMPLATE_", template.FULLTEMPLATE);
            strZpl = strZpl.Replace("_CONSTEMPLATE_", template.LINEBTTEMPLATE);
            strZpl = strZpl.Replace("_LBLQTY_", Model.Num_labels.ToString());

            return strZpl;

        }
        #endregion
    }


}