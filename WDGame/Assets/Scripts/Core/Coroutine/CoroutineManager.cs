using System.Collections;
using System.Collections.Generic;


namespace GameEngine
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        private LinkedList<GameCoroutine> _coroutines;
        private LinkedList<GameCoroutine> _toStopCoroutines;

        public void InitializeCoroutineManager()
        {
            _coroutines = new LinkedList<GameCoroutine>();
            _toStopCoroutines = new LinkedList<GameCoroutine>();
        }

        public void Update(float deltaTime)
        {
            if(_coroutines == null || _toStopCoroutines == null)
            {
                return;
            }

            LinkedListNode<GameCoroutine> node = _coroutines.First;
            while (node != null)
            {
                GameCoroutine co = node.Value;
                if (co != null && !_toStopCoroutines.Contains(co))
                {
                    if (!co.MoveNext(deltaTime))
                    {
                        _toStopCoroutines.AddLast(co);
                    }
                }
                node = node.Next;
            }

            LinkedListNode<GameCoroutine> toStop = _toStopCoroutines.First;
            while(toStop != null)
            {
                GameCoroutine co = toStop.Value;
                if (co != null)
                {
                    _coroutines.Remove(co);
                }
                toStop = toStop.Next;
            }

            _toStopCoroutines.Clear();
        }

        public GameCoroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                Log.Error(ErrorLevel.Hint, "StartCoroutine Failed,routine is null!");
                return null;
            }

            if (_coroutines != null)
            {
                Log.Error(ErrorLevel.Hint, "StartCoroutine Failed,_coroutines is null!");
                return null;
            }

            GameCoroutine co = new GameCoroutine(routine);
            _coroutines.AddLast(co);
            return co;
        }

        public void StopCoroutine(GameCoroutine co)
        {
            if (co == null)
            {
                return;
            }

            if(_toStopCoroutines != null)
            {
                _toStopCoroutines.AddLast(co);
            }
        }

        public void StopAllCoroutines()
        {
            if(_coroutines != null)
            {
                _coroutines.Clear();
            }

            if(_toStopCoroutines != null)
            {
                _coroutines.Clear();
            }
        }

        public void DisposeCoroutineManager()
        {
            _coroutines = null;
            _toStopCoroutines = null;
        }

    }
}


