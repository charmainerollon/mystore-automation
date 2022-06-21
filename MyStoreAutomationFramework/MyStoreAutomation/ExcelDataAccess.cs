using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreAutomation
{
    class ExcelDataAccess
    {
        public static DataTable ExcelToDataTable(string fileName, string sheetName)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });


                    DataTableCollection table = result.Tables;

                    DataTable resultTable = table[sheetName];

                    return resultTable;
                }
            }
        }
    }
}
