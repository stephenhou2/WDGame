using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDefine
{
    public enum MapBrushType
    {
        Obstacle = 0,
        Max = 1
    }

    public static string[] BrushName =
    {
        "Obstacle",
    };

    public static byte OBSTACLE_TYPE_EMPTY = 0;
    public static byte OBSTACLE_TYPE_OBSTACLE = 1;

    public static int MapDrawer_Ground = 0;
    public static int MapDrawer_Obstacle = 1;



    public static string MAP_GROUD_DRAWER_PATH = "_MAP_GROUD_DRAWER";
    public static string MAP_OBSTACLE_DRAWER_PATH = "_MAP_OBSTACLE_DRAWER";

    public static Color EmptyColor = Color.clear;
    public static Color ObstacleColor = new Color(1,0,0,0.5f);
}
