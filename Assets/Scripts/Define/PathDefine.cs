using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PathDefine
{
    public static string ROOT_PATH = Application.dataPath.Substring(0,Application.dataPath.Length - 7);

    public static string DATA_DIR_PATH = Path.Combine(ROOT_PATH,"Data");

    public static string MAP_OBS_DIR_PATH = Path.Combine(DATA_DIR_PATH, "ObsData");

    public static string MAP_DATA_DIR_PATH = Path.Combine(DATA_DIR_PATH, "MapData");


}
