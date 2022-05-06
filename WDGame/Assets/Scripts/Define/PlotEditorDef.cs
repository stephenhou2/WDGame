using UnityEngine;
internal static class PlotEditorWindowDef
{
    public static string PlotEditorDialogCardPath = "Assets/Prefab/DIALOG_NODE.prefab";
    public static string PlotEditorDialogImageSelectorPath = "Assets/Prefab/_IMG_SELECTOR.prefab";
    public static string PlotEditorDialogImageCardPath = "Assets/Prefab/_IMG_CARD.prefab";
}

public class PlotEditorWindowDelegates
{
    public delegate PlotEditorCard CreateCardDelegate(Vector3 position);
}