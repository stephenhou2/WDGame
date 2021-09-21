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
    public int DataRowCount;
    public int DataColCnt;

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

    private int ParseDataTypeAndKeys(List<ICell> cells,string sheetName)
    {
        for(int i =0;i<cells.Count;i++)
        {
            string data = cells[i].StringCellValue;
            string[] v = data.Split(':');

            DataType type = DataType.Undefine;
            string key = string.Empty;
            if (v.Length == 2)
            {
                type = GetDataType(v[0]);
                key = v[1];
            }
            
            DataTypes.Add(type);
            DataKeys.Add(key);
            DataColCnt++;

            if(v.Length != 2)
            {
                return -1;
            }

            if(type == DataType.Undefine)
            {
                string log = string.Format("ParseDataTypeAndKeys Undefined field type,sheet name={0},col={1}",sheetName,i);
                ConsoleLog.Error(log);
                return -1;
            }

            if(string.IsNullOrEmpty(v[1]))
            {
                string log = string.Format("ParseDataTypeAndKeys undefined field key,sheet name={0},col={1}", sheetName, i);
                ConsoleLog.Error(log);
                return -1;
            }

        }
        return 0;
    }

    public int ReadExcelFields(ISheet sheet)
    {
        if (sheet == null)
        {
            return -1;
        }

        SheetName = sheet.SheetName;

        var rowData = sheet.GetRow(0);
        var cells = rowData.Cells;

        return ParseDataTypeAndKeys(cells,SheetName);
    }

    private int ReadCellDatas(string sheetName,int row,List<ICell> cells)
    {
        List<string> data = new List<string>();
        DataValues.Add(data);
        for (int col = 0;col<DataColCnt;col++)
        {
            DataType type = DataTypes[col];

            if (type == DataType.Comment)
                continue;

            ICell cell = cells[col];
            if(type == DataType.Undefine)
            {
                string log = string.Format("ReadCellDatas failed, undefined dataType ,sheet name:{0},row:{1},col:{2}", sheetName, row, col);
                ConsoleLog.Error(log);
                return -1;
            }

            switch (cell.CellType)
            {
                case CellType.String:
                    data.Add(cell.StringCellValue);
                    break;
                case CellType.Numeric:
                    data.Add(cell.NumericCellValue.ToString());
                    break;
                default:
                    data.Add(string.Empty);
                    break;
            }
        }

        return 0;
    }

    public int ReadExcelData(ISheet sheet)
    {
        if(sheet == null)
        {
            return -1;
        }

        int ret = -2;

        SheetName = sheet.SheetName;

        DataRowCount = sheet.LastRowNum - 1;

        for(int row = 0; row <= sheet.LastRowNum; row ++)
        {
            var rowData = sheet.GetRow(row);
            var cells = rowData.Cells;
            if(row == 0)
            {
                ret++;
                ret += ParseDataTypeAndKeys(cells, SheetName);
            }
            else if(row != 1)
            {
                ret++;
                ret += ReadCellDatas(SheetName,row,cells);
            }
        }

        return ret;
    }


}
