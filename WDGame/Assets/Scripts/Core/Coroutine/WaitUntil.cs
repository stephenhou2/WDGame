namespace GameEngine
{
    public delegate bool WaitDelegate();

    public class WaitUntil : IWait
    {
        private WaitDelegate _waitCheck;

        public WaitUntil(WaitDelegate wait)
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
