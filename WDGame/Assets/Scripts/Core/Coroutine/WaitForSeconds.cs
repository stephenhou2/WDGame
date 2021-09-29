namespace GameEngine
{
    public class WaitForSeconds : IWait
    {
        private float _waitTime;
        private float _timer;
        public WaitForSeconds(float seconds)
        {
            _timer = 0;
            _waitTime = seconds;
        }

        public bool CanMoveNext()
        {
            return _timer >= _waitTime;
        }

        public void Tick(float deltaTime)
        {
            _timer += deltaTime;
        }
    }
}
