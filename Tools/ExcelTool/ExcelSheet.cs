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

public class ExcelSheet
{
    public string SheetName;
    public Dictionary<int,FieldInfo> FieldInfos;
    public List<List<string>> DataValues;
    public int DataRowCount;
    public int DataColCnt;

    public ExcelSheet()
    {
        FieldInfos = new Dictionary<int, FieldInfo>();
        DataValues = new List<List<string>>();
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

    public Dictionary<int,FieldInfo> ReadTableFieldDefineRow(ISheet sheet)
    {
        if(sheet == null)
        {
            string log = string.Format("ReadTableFieldDefineRow Error, Read null sheet!");
            ConsoleLog.Error(log);
            return null;
        }

        IRow row = sheet.GetRow(0);
        if(row == null)
        {
            string log = string.Format("ReadTableFieldDefineRow Error, Field define not exist,sheet name:{0}", sheet.SheetName);
            ConsoleLog.Error(log);
            return null;
        }

        Dictionary<int, FieldInfo> sheetFieldInfos = new Dictionary<int, FieldInfo>();
        Dictionary<string, FieldInfo> mFieldMap = new Dictionary<string, FieldInfo>();
        for (int i = 0; i < row.Cells.Count; i++)
        {
            DataType dataType = DataType.Undefine;
            FieldType fieldType = FieldType.Undefine;
            string fieldName = undefined;

            ICell cell = row.Cells[i];
            if(cell != null && cell.CellType == CellType.String)
            {
                string s = cell.StringCellValue;
                string[] list = s.Split(':');
                if(list.Length > 0)
                {
                    fieldType = GetFieldType(list[0]); 
                }

                if(list.Length > 1)
                {
                    dataType = GetDataType(list[1]);
                }
                
                if (list.Length > 2)
                {
                    fieldName = list[2];
                }

                if(fieldType == FieldType.Undefine)
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, Undefined field type,sheet name={0},col={1}", sheet.SheetName, i);
                    ConsoleLog.Error(log);
                    return null;
                }

                if (fieldType != FieldType.Comment)
                {
                    if (dataType == DataType.Undefine)
                    {
                        string log = string.Format("ReadTableFieldDefineRow Error, Undefined data type,sheet name={0},col={1}", sheet.SheetName, i);
                        ConsoleLog.Error(log);
                        return null;
                    }
                }

                FieldInfo fi = new FieldInfo();
                fi.FieldType = fieldType;
                fi.DataType = dataType;
                fi.FiledName = fieldName;
                fi.Col = i;

                FieldInfo _fi;
                if (mFieldMap.TryGetValue(fieldName,out _fi))
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, same field name is not allowed,sheet name={0},col_1={1},col_2={2}", sheet.SheetName, i, _fi.Col);
                    ConsoleLog.Error(log);
                    return null;
                }
                mFieldMap.Add(fieldName, fi);

                if (sheetFieldInfos.TryGetValue(i, out _fi))
                {
                    string log = string.Format("ReadTableFieldDefineRow Error, Add Field to same col,sheet name={0},col_1={1},col_2={2}", sheet.SheetName, fieldName, _fi.FiledName);
                    ConsoleLog.Error(log);
                    return null;
                }
                sheetFieldInfos.Add(i, fi);
            }
        }
        return sheetFieldInfos;
    }

    private int ReadCellDatas(ISheet sheet, int row)
    {
        List<string> data = new List<string>();
        DataValues.Add(data);

        IRow rowData = sheet.GetRow(row);
        if(rowData == null)
        {
            string log = string.Format("ReadCellDatas Error, row data is null,sheet name={0},row={1}", sheet.SheetName, row);
            ConsoleLog.Error(log);
            return -1;
        }

        for(int col =0;col<rowData.Cells.Count;col++)
        {
            ICell cell = rowData.Cells[col];
            if(cell == null)
            {
                string log = string.Format("ReadCellDatas Error, row cell is null,sheet name={0},row={1},col={2}", sheet.SheetName, row, col);
                ConsoleLog.Error(log);
                return -2;
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
                    data.Add(undefined);
                    break;
            }
        }
        return 0;
    }

    public int ReadExcelData(ISheet sheet)
    {
        if (sheet == null)
        {
            return -1;
        }

        FieldInfos = ReadTableFieldDefineRow(sheet);

        if(FieldInfos == null)
        {
            return -2;
        }

        for(int row = 2;row <= sheet.LastRowNum;row++)
        {
            int ret = ReadCellDatas(sheet, row);
            if(ret < 0)
            {
                return -3;
            }
        }

        return 0;
    }
}
