using System.Collections.Generic;

namespace GameEngine
{
    public class EmitterBus
    {
        private static Dictionary<BitType, Emitter> mAllEmitters = new Dictionary<BitType, Emitter>();

        private static Emitter GetEmitter(BitType moduleType)
        {
            Emitter emitter = null;
            if (mAllEmitters.TryGetValue(moduleType, out emitter))
            {
                return emitter;
            }

            emitter = new Emitter(moduleType);
            mAllEmitters.Add(moduleType, emitter);
            return emitter;
        }

        public static void Fire(BitType moduleType, string evtName, GameEventArgs args)
        {
            if (moduleType == null)
                return;

            moduleType.ForEachSingleType((BitType type) =>
            {
                Emitter emitter = GetEmitter(moduleType);
                emitter.OnFire(evtName, args);
            });
        }

        public static void AddListener(BitType moduleType, string evtName, GameEventCallback callback)
        {
            if (moduleType == null)
                return;

            moduleType.ForEachSingleType((BitType type) =>
            {
                Emitter emitter = GetEmitter(moduleType);
                emitter.AddListener(evtName, callback);
            });
        }
    }
}
