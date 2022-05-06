using UnityEngine;
using UnityEditor;

public class PlotEditDataCenter
{
    public static PlotEditDataCenter Ins = new PlotEditDataCenter();

    public string[] AllSprites;
    public string[] AllAudioClips;
    
    public void InitializePlotEditDataCenter()
    {
        FindAllSprites();
        FindAllSoundClips();
        FindAllVideos();
    }

    public void FindAllSprites()
    {
        string[] allGuids = AssetDatabase.FindAssets("t:Sprite");
        AllSprites = new string[allGuids.Length];
        for (int i=0;i< allGuids.Length;i++)
        {
            AllSprites[i] = AssetDatabase.GUIDToAssetPath(allGuids[i]);
        }
    }

    public void FindAllSoundClips()
    {
        string[] allGuids = AssetDatabase.FindAssets("t:Audio");
        AllAudioClips = new string[allGuids.Length];
        for (int i = 0; i < allGuids.Length; i++)
        {
            AllAudioClips[i] = AssetDatabase.GUIDToAssetPath(allGuids[i]);
        }
    }

    public void FindAllVideos()
    {

    }
}