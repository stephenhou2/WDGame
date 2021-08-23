using System.IO;
using Google.Protobuf;

public static class ProtoDataHandler
{
    public static int DATA_HANDLE_RET_OK = 0;
    public static int DATA_HANDLE_RET_INVALID_PATH = -1;
    public static int DATA_HANDLE_RET_NULL_DATA = -2;

    public static int SaveProtoData<T>(T src,string path)where T:IMessage
    {
        if (string.IsNullOrEmpty(path))
            return DATA_HANDLE_RET_INVALID_PATH;

        byte[] data = src.ToByteArray();

        if (data == null)
            return DATA_HANDLE_RET_NULL_DATA;

        File.WriteAllBytes(path, data);
        return DATA_HANDLE_RET_OK;
    }

    public static T GetProtoData<T>(string path) where T:IMessage,new()
    {
        T target = new T();
        if (File.Exists(path))
        {
            byte[] data = File.ReadAllBytes(path);
            if(data != null)
            {
                target.MergeFrom(data);
            }
        }
        return target;
    }
}
