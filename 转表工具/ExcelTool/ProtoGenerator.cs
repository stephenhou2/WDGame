using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NPOI.XSSF.UserModel;


public class ProtoGenerator
{
    private StringBuilder mProtoString;

    public ProtoGenerator()
    {
        mProtoString = new StringBuilder();

        mProtoString.Append("syntax =  \"proto3\";\r\npackage TabelProto;\r\n");

    }

    public void AppedProto(ExcelSheet sheet)
    {
        string tableName = sheet.SheetName;

        mProtoString.AppendFormat("\r\n\r\nmessage {0}\r\n", tableName);
        mProtoString.Append("{\r\n");

        for (int i =0;i<sheet.DataTypes.Count;i++)
        {
            DataType type = sheet.DataTypes[i];
            string key = sheet.DataKeys[i];
            int index = i + 1;
            if(type == DataType.UInt)
            {
                mProtoString.AppendFormat("  uint32 {0} = {1};\r\n", key, index);
            }
            if(type == DataType.Int)
            {
                mProtoString.AppendFormat("  int32 {0} = {1};\r\n", key, index);
            }
            else if(type == DataType.String)
            {
                mProtoString.AppendFormat("  string {0} = {1};\r\n", key, index);
            }
            else if(type == DataType.Array)
            {
                mProtoString.AppendFormat("  repeated int32 {0} = {1};\r\n", key, index);
            }
        }

        mProtoString.Append("}");
    }

    public void ExportProto(string path)
    {
        File.WriteAllText(path, mProtoString.ToString(), Encoding.UTF8);
    }

}

