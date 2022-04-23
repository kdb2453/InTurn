using InTurn_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Configuration;

//namespace InTurn.Areas
//{
//    public class AdminController : Controller
//    {
//        private InTurnEntities db = new InTurnEntities();
//        GET: Admin
//        public ActionResult ImportStudents()
//        {
//            var filePath = ConfigurationManager.AppSettings["Students"];

//            Excel.Application xlApp = new Excel.Application();
//            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
//            Excel._Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
//            Excel.Range xlRange = xlWorksheet.UsedRange;

//            int rowCount = xlRange.Rows.Count;

//            for (int i = 2; i <= rowCount; i++)
//            {
//                string fName, lName, phone, address, city, state, zip, email;
//                bool current, graduate;

//                fName = Convert.ToString(xlRange.Cells[i, 1].Value2);
//                lName = Convert.ToString(xlRange.Cells[i, 2].Value2);
//                phone = Convert.ToString(xlRange.Cells[i, 3].Value2);
//                address = Convert.ToString(xlRange.Cells[i, 4].Value2);
//                city = Convert.ToString(xlRange.Cells[i, 5].Value2);
//                state = Convert.ToString(xlRange.Cells[i, 6].Value2);
//                zip = Convert.ToString(xlRange.Cells[i, 7].Value2);
//                email = Convert.ToBase64String(xlRange.Cells[i, 8].Value2);
//                current = Convert.ToBoolean(xlRange.Cells[i, 9].Value2);
//                graduate = Convert.ToBoolean(xlRange.Cells[i, 10].Value2);

//                string connectionString = (ConfigurationManager.ConnectionStrings["InTurnEntities"]).ConnectionString;
//                SqlConnection conn = new SqlConnection(connectionString);
//                conn.Open();

//                try
//                {
//                    SqlConnection cmd = new SqlCommand("INSERT INTO	[dbo].[Student] (FirstName, LastName, PhoneNum, [Address], City, [State], ZipCode, Email, [Current], Graduate) values (" + fName + "," + lName + "," + phone + "," + address + "," + city + "," + state + "," + zip + "," + email + "," + current + "," + graduate + ");", conn);
//                    cmd.ExecuteNonQuery();
//                    conn.Close();
//                };

//                catch (Exception ex)
//                {
//                    conn.Close();
//                    throw ex;
//                }
//            }

//        }


//            return View();
//    }
//}
//}