using System.Collections;
using System.Collections.Generic;


namespace GameEngine
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        private LinkedList<Coroutine> _coroutines = new LinkedList<Coroutine>();
        private LinkedList<Coroutine> _toStopCoroutines = new LinkedList<Coroutine>();

        public void Update(float deltaTime)
        {
            LinkedListNode<Coroutine> node = _coroutines.First;
            while (node != null)
            {
                Coroutine co = node.Value;
                if (co != null && !_toStopCoroutines.Contains(co))
                {
                    if (!co.MoveNext(deltaTime))
                    {
                        _toStopCoroutines.AddLast(co);
                    }
                }
                node = node.Next;
            }

            LinkedListNode<Coroutine> toStop = _toStopCoroutines.First;
            while(toStop != null)
            {
                Coroutine co = toStop.Value;
                if (co != null)
                {
                    _coroutines.Remove(co);
                }
                toStop = toStop.Next;
            }

            _toStopCoroutines.Clear();
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
            {
                Log.Error(ErrorLevel.Hint, "StartCoroutine Failed,routine is null!");
            }
            Coroutine co = new Coroutine(routine);
            _coroutines.AddLast(co);
            return co;
        }

        public void StopCoroutine(Coroutine co)
        {
            if (co != null)
            {
                _toStopCoroutines.AddLast(co);
            }
        }

        public void StopAllCoroutines()
        {
            _coroutines.Clear();
        }

        public void DisposeCoroutineManager()
        {

        }

    }
}


