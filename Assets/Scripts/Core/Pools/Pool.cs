using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Pools
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private List<T> prefabs;

        private Dictionary<T, ObjectPool<T>> _storage = new Dictionary<T, ObjectPool<T>>();

        private void Awake()
        {
            _storage = new Dictionary<T, ObjectPool<T>>();
            foreach (var prefab in prefabs)
            {
                _storage.Add(prefab, new ObjectPool<T>());
            }
        }


        public T GetRandom(out T prefab, Transform parent)
        {
            prefab = prefabs[Random.Range(0, prefabs.Count)];

            return Get(prefab, parent);
        }

        public T Get(out T prefab, int index, Transform parent)
        {
            prefab = prefabs[index];

            return Get(prefab, parent);
        }

        public T Get(T prefab, Transform parent)
        {
            return _storage[prefab].Get(prefab, parent);
        }

        public void Release(T prefab, T poolObject)
        {
            _storage[prefab].Release(poolObject);
        }

    }
}