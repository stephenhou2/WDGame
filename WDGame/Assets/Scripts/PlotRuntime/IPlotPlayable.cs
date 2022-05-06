public interface IPlotPlayable
{
    void PlayPlot(string plotId);
    void MoveNext();
    void MoveNextWithOption(int optionIndex);
}
