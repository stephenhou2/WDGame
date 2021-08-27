using System.IO;

public static class FileHelper
{
    public static  bool FileExist(string fullPath)
    {
        return File.Exists(fullPath);
    }

    public static byte[] ReadAllBytes(string fullPath)
    {
        if(!FileExist(fullPath))
        {
            Log.Error("ReadAllBytes Error,File does not exit,fullPath:{0}", fullPath);
            return null;
        }

        return File.ReadAllBytes(fullPath);
    }

    public static void WriteAllBytes(string fullPath,byte[] data)
    {
        if (FileExist(fullPath))
        {
            File.Delete(fullPath);
        }

        File.WriteAllBytes(fullPath, data);
    }
}
