using System.Collections;
using System.Collections.Generic;

public class PlotDataCenter 
{
    public static PlotDataCenter Ins = new PlotDataCenter();

    private Dictionary<string, PlotData> mAllPlotDatas = new Dictionary<string, PlotData>();

    public PlotData GetPlotData(string plotId)
    {
        if(mAllPlotDatas.TryGetValue(plotId,out PlotData data))
        {
            data.Reset();
            return data;
        }

        return PlotDataHelper.LoadPlotData(plotId);
    }

    public void ExportPlotData(string plotId,PlotData data)
    {
        PlotDataHelper.SavePlotData(plotId, data);
    }
}
