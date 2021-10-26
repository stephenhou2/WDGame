namespace GameEngine
{
    public delegate bool WaitDelegate();

    public class WDWaitUntil : IWait
    {
        private WaitDelegate _waitCheck;

        public WDWaitUntil(WaitDelegate wait)
        {
            _waitCheck = wait;
        }

        public bool CanMoveNext()
        {
            if (_waitCheck == null)
                return true;

            return _waitCheck();
        }

        public void Tick(float deltaTime)
        {
            
        }
    }
}
