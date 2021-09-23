using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System;

public class ExcelReader
{
    private Dictionary<string, ISheet> mSheetList = new Dictionary<string, ISheet>();

    public ISheet GetSheet(string sheetName)
    {
        mSheetList.TryGetValue(sheetName, out ISheet sheet);
        return sheet;
    }

    public static bool IsTempFile(string filename)
    {
        return filename.StartsWith("~");
    }

    public bool ReadExcel(string path)
    {
        IWorkbook workbook = null;

        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            stream.Position = 0;
            if (path.IndexOf(".xlsx") > 0) // 07或更高版本
            {
                workbook = new XSSFWorkbook(stream);
            }
            else if (path.IndexOf(".xls") > 0)  // 97-03版本
            {
                workbook = new HSSFWorkbook(stream);
            }

            if (workbook != null)
            {
                int sheetCnt = workbook.NumberOfSheets;
                for (int i = 0; i < sheetCnt; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    mSheetList.Add(sheet.SheetName,sheet);
                }
            }
            else
            {
                string log = string.Format("Read Excel Failed,path:{0}",path);
                ConsoleLog.Error(log);
            }
        }

        return true;
    }

    private bool ReadDir(DirectoryInfo dirInfo)
    {
        if (dirInfo == null)
        {
            string log = string.Format("<color=red> ReadAllTableExcel Failed,table root dir not find,root dir:{0}</color>", Define.TableRootPath);
            Console.Write(log);
            return false;
        }

        DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
        if(dirInfos.Length > 0)
        {
            for(int i = 0;i<dirInfos.Length;i++)
            {
                ReadDir(dirInfos[i]);
            }
        }

        FileInfo[] fileInfos = dirInfo.GetFiles();
        if (null == fileInfos)
        {
            string log = string.Format("ReadAllTableExcel Failed,GetFiles failed,root dir:{0}", Define.TableRootPath);
            ConsoleLog.Error(log);
            return false;
        }

        for (int i = 0; i < fileInfos.Length; i++)
        {
            FileInfo fi = fileInfos[i];
            if (fi == null)
            {
                string log = string.Format("ReadAllTableExcel Failed,find null file info,root dir:{0}", Define.TableRootPath);
                ConsoleLog.Error(log);
                continue;
            }

            if (!IsTempFile(fi.Name))
            {
                ReadExcel(fi.FullName);
            }
        }

        return true;
    }

    public int ReadAllTableExcel()
    {
        DirectoryInfo rootDirInfo = new DirectoryInfo(Define.TableRootPath);

        ReadDir(rootDirInfo);
    
        return 0;
    }
}

