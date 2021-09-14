using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.IO;


public class ExcelReader
{
    public static List<ISheet> ReadExcel(string path)
    {
        List<ISheet> sheetList = new List<ISheet>();
        IWorkbook workbook = null;
        using (var stream = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
        {
            stream.Position = 0;
            if(path.IndexOf(".xlsx") > 0) // 07或更高版本
            {
                workbook = new XSSFWorkbook(stream);
            }
            else if(path.IndexOf(".xls") > 0)  // 97-03版本
            {
                workbook = new HSSFWorkbook(stream);
            }

            if(workbook != null)
            {
                int sheetCnt = workbook.NumberOfSheets;
                for(int i  = 0;i<sheetCnt;i++)
                {
                    sheetList.Add(workbook.GetSheetAt(i));
                }
            }
        }
        return sheetList;
    }
}
