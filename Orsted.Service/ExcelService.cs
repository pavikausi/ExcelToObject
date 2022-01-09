using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using Orsted.Model;
using System.Data;

namespace Orsted.Service
{
    public class ExcelService : IExcelService
    {
        public List<Employee> ReadFile(Stream stream, string fileExt)
        {
            List<Employee> employees = new List<Employee>();
            IExcelDataReader reader = null;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (fileExt.EndsWith(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (fileExt.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet dsexcelRecords = new DataSet();
            dsexcelRecords = reader.AsDataSet();
            reader.Close();

            if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
            {
                DataTable dtStudentRecords = dsexcelRecords.Tables[0];
                for (int i = 1; i < dtStudentRecords.Rows.Count; i++)
                {
                    Employee employee = new Employee
                    {
                        Number = Convert.ToInt32(dtStudentRecords.Rows[i][0]),
                        FirstName = Convert.ToString(dtStudentRecords.Rows[i][1]),
                        LastName = Convert.ToString(dtStudentRecords.Rows[i][2]),
                        Status = Convert.ToString(dtStudentRecords.Rows[i][3]),
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}
