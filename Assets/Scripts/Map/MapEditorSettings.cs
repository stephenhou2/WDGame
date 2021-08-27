using UnityEngine;

public enum MapCellDirection
{
    Verticle = 0,
    Horizontal = 1,
}

public class MapEditorSettings 
{
    public int BrushWidth { get; private set; }
    public int MapWidth { get; private set; }
    public int MapHeight { get; private set; }
    public float MapCellSize { get; private set; }
    public float MapCellSize_60 { get; private set; }
    public float MapCellSize_30{ get; private set; }

    public MapCellDirection CellDirection { get; private set; }

    private string _KBrushWidth = "MapEditor_BrushWidth";
    private string _KMapWidth = "MapEditor_MapWidth";
    private string _KMapHeight = "MapEditor_MapHeight";
    private string _KCellSize = "MapEditor_CellSize";
    private string _KCellDirection = "MapEditor_CellDirection";

    public MapEditorSettings()
    {
        BrushWidth = PlayerPrefs.GetInt(_KBrushWidth, 1);
        MapWidth = PlayerPrefs.GetInt(_KMapWidth, 10);
        MapHeight = PlayerPrefs.GetInt(_KMapHeight, 10);
        MapCellSize = PlayerPrefs.GetFloat(_KCellSize, 1.0f);
        MapCellSize_60 = MapCellSize * Mathf.Sqrt(3) / 2;
        MapCellSize_30 = MapCellSize / 2; 
        CellDirection = (MapCellDirection)PlayerPrefs.GetInt(_KCellDirection, 1);
        BrushWidth = PlayerPrefs.GetInt(_KBrushWidth, 1);
    }

    public void SetBrushWidth(int width)
    {
        BrushWidth = width;
        PlayerPrefs.SetInt(_KBrushWidth, width);
        PlayerPrefs.Save();
    }  
    
    public void SetMapWidth(int width)
    {
        MapWidth = width;
        PlayerPrefs.SetInt(_KMapWidth, width);
        PlayerPrefs.Save();
    }

    public void SetMapHeight(int height)
    {
        MapHeight = height;
        PlayerPrefs.SetInt(_KMapHeight, height);
        PlayerPrefs.Save();
    }

    public void SetMapCellSize(float size)
    {
        MapCellSize = size;
        MapCellSize_60 = MapCellSize * Mathf.Sqrt(3) / 2;
        MapCellSize_30 = MapCellSize / 2;
        PlayerPrefs.SetFloat(_KCellSize, size);
        PlayerPrefs.Save();
    }

    public void SetMapCellDirection(MapCellDirection direction)
    {
        CellDirection = direction;
        PlayerPrefs.SetInt(_KCellDirection, (int)direction);
        PlayerPrefs.Save();
    }
}
