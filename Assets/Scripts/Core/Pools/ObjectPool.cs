using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Pools
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private Stack<T> _stack;

        private int _saveToDestroyCount;

        public ObjectPool()
        {
            _stack = new Stack<T>();
            _saveToDestroyCount = -1;
        }

        public T Get(T obj, Transform parent)
        {
            if (_stack.Count > 0)
            {
                var result = _stack.Pop();
                result.transform.SetParent(parent);
                result.gameObject.SetActive(true);

                if (_saveToDestroyCount == -1)
                    _saveToDestroyCount = _stack.Count;
                else if (_stack.Count < _saveToDestroyCount)
                    _saveToDestroyCount = _stack.Count;

                return result;
            }

            return CreateNew(obj, parent);
        }

        public T CreateNew(T obj, Transform parent)
        {
            var instance = Instantiate(obj, parent);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _stack.Push(obj);
        }
    }
}