public class PlotData
{
    public string PlotId;
    private DialogData mFirstDialog;
    private DialogData mCurDialog;

    public DialogData GetCurDialog()
    {
        return mCurDialog;
    }

    public void Reset()
    {
        mCurDialog = mFirstDialog;
    }

    public bool MoveNext()
    {
        if(mCurDialog == null)
        {
            return false;
        }

        // 如果当前有选项，需要使用MoveNextWithOption
        if (mCurDialog.Options != null  && mCurDialog.Options.Length > 0)
        {
            return true;
        }

        // 没有选项的，跳转到下一对话
        if(mCurDialog.FollowedDialog != null)
        {
            mCurDialog = mCurDialog.FollowedDialog;
        }

        return false;
    }

    public bool MoveNextWithOption(int index)
    {
        if (mCurDialog == null || mCurDialog.Options == null  || index >= mCurDialog.Options.Length)
        {
            return false;
        }

        DialogOptionData option = mCurDialog.Options[index];
        if(option ==  null)
        {
            return false;
        }

        mCurDialog = option.FollowedDialog;
        return true;
    }

    public PlotData(string plotId,DialogData firstDialog)
    {
        PlotId = plotId;
        mFirstDialog = firstDialog;
        mCurDialog = firstDialog;
    }
}
