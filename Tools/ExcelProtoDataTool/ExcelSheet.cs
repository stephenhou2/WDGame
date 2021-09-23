using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;

public enum DataType
{
    UInt = 0,
    Int = 1,
    String = 2,
    Array = 3,
    Undefine = 4
}

public enum FieldType
{
    Comment = 0, // 注释数据，不导出
    Unique = 1,
    Key = 2,
    Field = 3,
    Undefine = 4
}


public struct FieldInfo
{
    public int Col;
    public FieldType FieldType;
    public DataType DataType;
    public string FiledName;
}

public struct ExcelLine
{
    public int Row;
    public List<string> lineData;

    public void InitExcelLine(int row, List<string> line)
    {
        Row = row;
        lineData = line;
    }
}

public class ExcelSheet
{
    public string SheetName;
    public List<FieldInfo> FieldInfos;
    public List<ExcelLine> ExcelLines;

    public ExcelSheet()
    {
        FieldInfos = new List<FieldInfo>();
        ExcelLines = new List<ExcelLine>();
    }

    //"UNI:INT:ID"
    //"KEY:INT:ID"
    //"FIELD:INT:ID"
    //"COMMNET"

    private FieldType GetFieldType(string s)
    {
        if (s == "UNK") // unique key
        {
            return FieldType.Unique;
        }
        else if (s == "KEY") // key
        {
            return FieldType.Key;
        }
        else if (s == "FLD") // field
        {
            return FieldType.Field;
        }
        else if (s == "CMT") // comment
        {
            return FieldType.Comment;
        }

        return FieldType.Undefine;
    }

    private DataType GetDataType(string s)
    {
        if (s == "UINT")
        {
            return DataType.UInt;
        }
        else if (s == "INT")
        {
            return DataType.Int;
        }
        else if (s == "STR")
        {
            return DataType.String;
        }
        else if (s == "ARR")
        {
            return DataType.Array;
        }

        return DataType.Undefine;
    }


    const string undefined = "##";

    public int ReadTableFieldDefineRow(ISheet sheet)
    {
        if (sheet == null)
        {
            string log = string.Format("ReadTableFieldDefineRow Error, Read null sheet!");
            ConsoleLog.Error(log);
            return -1;
        }

        SheetName = sheet.SheetName;

        IRow row = sheet.GetRow(0);
        if (row == null)
        {
            string log = string.Format("ReadTableFieldDefineRow Error, Field define not exist,sheet name:{0}", sheet.SheetName);
            ConsoleLog.Error(log);
            return -2;
        }

        List<FieldInfo> sheetFieldInfos = new List<FieldInfo>();
        Dictionary<string, FieldInfo> mFieldMap = new Dictionary<string, FieldInfo>();
        for (int i = 0; i < row.Cells.Count; i++)
        {
            DataType dataType = DataType.Undefine;
            FieldType fieldType = FieldType.Undefine;
            string fieldName = undefined;

            ICell cell = row.Cells[i];
            if (cell != null && cell.CellType == CellType.String)
            {
                string s = cell.StringCellValue;
                string[] list = s.Split(':');
                if (list.Length > 0)
                {
                    fieldType = GetFieldType(list[0]);
                }

                if (list.Length > 1)
                {
                    dataType = GetDataType(list[1]);
                }

                if (list.Length > 2)
                {
                    fieldName = list[2];
                }

                if (fieldType == FieldType.Undefine)
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, Undefined field type,sheet name={0},col={1}", sheet.SheetName, i);
                    ConsoleLog.Error(log);
                    return -3;
                }

                if (fieldType != FieldType.Comment)
                {
                    if (dataType == DataType.Undefine)
                    {
                        string log = string.Format("ReadTableFieldDefineRow Error, Undefined data type,sheet name={0},col={1}", sheet.SheetName, i);
                        ConsoleLog.Error(log);
                        return -4;
                    }
                }

                if(fieldType == FieldType.Unique && dataType != DataType.Int)
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, unique key must be int type ,sheet name={0}", sheet.SheetName);
                    ConsoleLog.Error(log);
                    return -5;
                }

                FieldInfo fi = new FieldInfo();
                fi.FieldType = fieldType;
                fi.DataType = dataType;
                fi.FiledName = fieldName;
                fi.Col = i;

                FieldInfo _fi;
                if (mFieldMap.TryGetValue(fieldName, out _fi))
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, same field name is not allowed,sheet name={0},col_1={1},col_2={2}", sheet.SheetName, i, _fi.Col);
                    ConsoleLog.Error(log);
                    return -5;
                }
                mFieldMap.Add(fieldName, fi);
                sheetFieldInfos.Add(fi);
            }
        }

        FieldInfos = sheetFieldInfos;
        return 0;
    }

    private int ReadCellDatas(ISheet sheet, int row)
    {
        IRow rowData = sheet.GetRow(row);
        if (rowData == null)
        {
            string log = string.Format("ReadCellDatas Error, row data is null,sheet name={0},row={1}", sheet.SheetName, row);
            ConsoleLog.Error(log);
            return -1;
        }

        List<string> cellDatas = new List<string>();
        for (int col = 0; col < rowData.Cells.Count; col++)
        {
            ICell cell = rowData.Cells[col];
            if (cell == null)
            {
                string log = string.Format("ReadCellDatas Error, row cell is null,sheet name={0},row={1},col={2}", sheet.SheetName, row, col);
                ConsoleLog.Error(log);
                return -2;
            }
            switch (cell.CellType)
            {
                case CellType.String:
                    cellDatas.Add(cell.StringCellValue);
                    break;
                case CellType.Numeric:
                    cellDatas.Add(cell.NumericCellValue.ToString());
                    break;
                default:
                    cellDatas.Add(undefined);
                    break;
            }
        }

        ExcelLine line = new ExcelLine();
        line.InitExcelLine(row, cellDatas);
        ExcelLines.Add(line);
        return 0;
    }

    public int ReadExcelData(ISheet sheet)
    {
        if (sheet == null)
        {
            string log = string.Format("ReadExcelData Error,read null sheet");
            ConsoleLog.Error(log);
            return -1;
        }

        SheetName = sheet.SheetName;
        int ret = ReadTableFieldDefineRow(sheet);

        if (ret < 0)
        {
            string log = string.Format("ReadExcelData Error, read table field define row failed,sheet name={0}", sheet.SheetName);
            ConsoleLog.Error(log);
            return -2;
        }

        for (int row = 2; row <= sheet.LastRowNum; row++)
        {
            ret = ReadCellDatas(sheet, row);
            if (ret < 0)
            {
                string log = string.Format("ReadExcelData Error, row data read failed,sheet name={0},row={1}", sheet.SheetName, row);
                ConsoleLog.Error(log);
                return -3;
            }
        }

        return 0;
    }
}
