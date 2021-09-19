using System.Text;
using System.IO;
using System.Diagnostics;

namespace ExcelTool
{
    public class ProtoGenerator
    {
        public ProtoGenerator()
        {
        }

        public string GetProtoString(ExcelSheet sheet)
        {
            string tableName = sheet.SheetName;
            StringBuilder protoString = new StringBuilder();
            protoString.Append("syntax =  \"proto3\";\r\npackage TableProto;\r\n");

            protoString.AppendFormat("\r\n\r\nmessage {0}\r\n", tableName);
            protoString.Append("{\r\n");

            for (int i = 0; i < sheet.DataTypes.Count; i++)
            {
                DataType type = sheet.DataTypes[i];
                string key = sheet.DataKeys[i];
                int index = i + 1;
                if (type == DataType.UInt)
                {
                    protoString.AppendFormat("  uint32 {0} = {1};\r\n", key, index);
                }
                if (type == DataType.Int)
                {
                    protoString.AppendFormat("  int32 {0} = {1};\r\n", key, index);
                }
                else if (type == DataType.String)
                {
                    protoString.AppendFormat("  string {0} = {1};\r\n", key, index);
                }
                else if (type == DataType.Array)
                {
                    protoString.AppendFormat("  repeated int32 {0} = {1};\r\n", key, index);
                }
            }

            protoString.Append("}");

            protoString.AppendFormat("\r\n\r\nmessage TB_{0}\r\n", tableName);
            protoString.AppendFormat("{{\r\n  repeated {0} data = 1;\r\n}}", tableName);

            return protoString.ToString();
        }

        public void ExportProto(string protoStr,string tableName)
        {
            string protoPath = Path.Combine(Define.ProtoDir,string.Format("{0}.proto",tableName));
            File.WriteAllText(protoPath, protoStr.ToString(), Encoding.UTF8);
            ExporetCSharpProto();
        }

        private void ExporetCSharpProto()
        {
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = Define.ToolRootDir;
            proc.StartInfo.FileName = Define.CSharpProtoBatName;
            proc.Start();
            proc.WaitForExit();
        }

    }
}


