using NPOI.SS.UserModel;

public static class ProtoDataExpoter
{
    public static int GetIntFieldValue(string dataStr)
    {
        int ret;
        int.TryParse(dataStr, out ret);
        return ret;
    }

    public static string GetStringFieldValue(string dataStr)
    {
        return dataStr;
    }

    public static uint GetUIntFieldValue(string dataStr)
    {
        uint ret;
        uint.TryParse(dataStr, out ret);
        return ret;
    }

    private static char[] seperator = new char[] { ';' };
    public static  int[] GetArrayFieldValue(string dataStr)
    {
        string[] data = dataStr.Split(seperator);
        int[] ret = new int[data.Length];
        for(int i =0;i<data.Length;i++)
        {
            int.TryParse(data[i], out int num);
            ret[i] = num;
        }
        return ret;
    }
}
