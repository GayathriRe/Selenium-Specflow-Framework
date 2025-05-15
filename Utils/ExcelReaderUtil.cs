using System.Data;
using System.IO;
using ExcelDataReader;

public static class ExcelReaderUtil
{
    public static DataTable ReadExcel(string filePath, string sheetName)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                return result.Tables[sheetName];
            }
        }
    }
}
