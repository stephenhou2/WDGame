public class PlotPlayer : IPlotPlayable, IPlotTriggerable
{
    private PlotData mCurPlot;

    public void PlayPlot(string plotId)
    {
        mCurPlot = PlotDataCenter.Ins.GetPlotData(plotId);
    }

    public void MoveNext()
    {
        if(mCurPlot != null)
        {
            mCurPlot.MoveNext();
        }
    }

    public void MoveNextWithOption(int optionIndex)
    {
        if (mCurPlot != null)
        {
            mCurPlot.MoveNextWithOption(optionIndex);
        }
    }

    private bool PlotTriggerCheck(string src, string compare , string target)
    {
        if (compare == PlotTriggerCompare.Equal)
        {
            return PlotTriggerHelper.TriggerEqual(src, target);
        }
        else if (compare == PlotTriggerCompare.NotEqual)
        {
            return PlotTriggerHelper.TriggerNotEqual(src, target);
        }
        else if (compare == PlotTriggerCompare.LowerThan)
        {
            return PlotTriggerHelper.TriggerLowerThan(src, target);
        }
        else if (compare == PlotTriggerCompare.GreaterThan)
        {
            return PlotTriggerHelper.TriggerGreaterThan(src, target);
        }
        else if (compare == PlotTriggerCompare.NotLowerThan)
        {
            return PlotTriggerHelper.TriggerNotLowerThan(src, target);
        }
        else if (compare == PlotTriggerCompare.NotGreaterThan)
        {
            return PlotTriggerHelper.TriggerNotGreaterThan(src, target);
        }

        return false;
    }

    private bool PlotTriggerCheck(string src, string compare, string[] target)
    {
        if (compare == PlotTriggerCompare.In)
        {
            return PlotTriggerHelper.TriggerIn(src, target);
        }
        else if (compare == PlotTriggerCompare.NotIn)
        {
            return PlotTriggerHelper.TriggerNotIn(src, target);
        }

        return false;
    }

    private bool PlotTriggerCheck(string[] src, string compare, string[] target)
    {
        if (compare == PlotTriggerCompare.Cross)
        {
            return PlotTriggerHelper.TriggerCross(src, target);
        }
        else if (compare == PlotTriggerCompare.NotCross)
        {
            return PlotTriggerHelper.TriggerNotCross(src, target);
        }

        return false;
    }


    public bool PlotTriggered(string triggerType, string compare, string target)
    {
        if(triggerType == PlotTriggerType.CurChapter)
        {
            string src = "1";
            return PlotTriggerCheck(src, compare, target);
        }

        return false;
    }

    public bool PlotTriggered(string triggerType, string compare, string[] target)
    {
        if (triggerType == PlotTriggerType.CurChapter)
        {
            string src = "1";
            return PlotTriggerCheck(src, compare, target);
        }

        return false;
    }
}
