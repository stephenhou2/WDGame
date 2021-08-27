using System.IO;
public static class PathHelper
{
    public static string GetMapObsFilePath(int stageId)
    {
        return Path.Combine(PathDefine.MAP_OBS_DIR_PATH, string.Format("{0}.bin", stageId));
    }
}
