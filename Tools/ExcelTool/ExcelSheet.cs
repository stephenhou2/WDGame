using System;
using System.Collections.Generic;

using NPOI.SS.UserModel;

public enum DataType
{
    Comment = 0, // 注释数据，不导出
    UInt = 1,
    Int = 2,
    String = 3,
    Array = 4,
    Undefine = 5
}

public class ExcelSheet
{
    public string SheetName;
    public List<DataType> DataTypes;
    public List<string> DataKeys;
    public List<List<string>> DataValues;

    public ExcelSheet()
    {
        DataTypes = new List<DataType>();
        DataKeys = new List<string>();
        DataValues = new List<List<string>>();
    }

    private DataType GetDataType(string type)
    {
        if(type == "UINT")
        {
            return DataType.UInt;
        }
        else if(type == "INT")
        {
            return DataType.Int;
        }
        else if(type == "STR")
        {
            return DataType.String;
        }
        else if(type == "ARR")
        {
            return DataType.Array;
        }
        else if(type == "$")
        {
            return DataType.Comment;
        }
        else
        {
            return DataType.Undefine;
        }
    }

    private bool ParseDataTypeAndKeys(List<ICell> cells)
    {
        for(int i =0;i<cells.Count;i++)
        {
            string data = cells[i].StringCellValue;
            string[] v = data.Split(':');
            if(v.Length != 2)
            {
                return false;
            }

            DataType type = GetDataType(v[0]);
            if(type == DataType.Undefine)
            {
                return false;
            }

            if(string.IsNullOrEmpty(v[1]))
            {
                return false;
            }

            DataTypes.Add(type);
            DataKeys.Add(v[1]);
        }
        return true;
    }

    private bool ReadCellDatas(string sheetName,int row,List<ICell> cells)
    {
        List<string> data = new List<string>();
        DataValues.Add(data);
        for (int col = 0;col<cells.Count;col++)
        {
            DataType type = DataTypes[col];

            if (type == DataType.Comment)
                continue;

            ICell cell = cells[col];
            if(type == DataType.Undefine)
            {
                string log = string.Format("<color=red>ReadCellDatas failed,sheet name:{0},row:{1},col:{2}</color>", sheetName, row, col);
                Console.WriteLine(log);
                return false;
            }

            data.Add(cell.StringCellValue);
        }

        return true;
    }

    public int ReadExcelData(ISheet sheet)
    {
        if(sheet == null)
        {
            return -1;
        }

        SheetName = sheet.SheetName;

        int firstRow = sheet.FirstRowNum;
        int lastRow = sheet.LastRowNum;
        for(int row = firstRow; row < lastRow;row ++)
        {
            var rowData = sheet.GetRow(row);
            var cells = rowData.Cells;
            if(row == 0)
            {
                if(ParseDataTypeAndKeys(cells))
                {
                    return -2;
                }
            }
            else
            {
                ReadCellDatas(SheetName,row,cells);
            }
            row++; 
        }

        return 0;
    }


}
