using System.Collections.Generic;
namespace GameEngine
{
    public class Emitter
    {
        private BitType mModuleType;

        private Dictionary<string, GameEvent> mEvents = new Dictionary<string, GameEvent>();

        public Emitter(BitType moduleType)
        {
            mModuleType = moduleType;
            mEvents = new Dictionary<string, GameEvent>();
        }

        public void OnFire(string evtName, GameEventArgs args = null)
        {
            GameEvent evt;
            if (mEvents.TryGetValue(evtName, out evt))
            {
                if (evt != null)
                {
                    evt.OnTrigger(args);
                }
            }
        }

        public void AddListener(string eventName, GameEventCallback callback)
        {
            GameEvent evt;
            if (!mEvents.TryGetValue(eventName, out evt))
            {
                evt = new GameEvent();
                mEvents.Add(eventName, evt);
            }

            if (evt == null)
            {
                evt = new GameEvent();
            }

            evt.AddListener(callback);
        }

        public BitType GetModuleType()
        {
            return mModuleType;
        }

        public void Dispose()
        {
            mEvents.Clear();
        }
    }
}