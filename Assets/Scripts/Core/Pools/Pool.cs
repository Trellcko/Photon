using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Pools
{
    public class Pool<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        [SerializeField] private List<T> prefabs;

        private Dictionary<T, ObjectNetworkPool<T>> _storage = new Dictionary<T, ObjectNetworkPool<T>>();

        private void Awake()
        {
            _storage = new Dictionary<T, ObjectNetworkPool<T>>();
            foreach (var prefab in prefabs)
            {
                _storage.Add(prefab, new ObjectNetworkPool<T>(()=> Runner.Spawn(prefab)));
            }
        }


        public T GetRandom(out T prefab, Transform parent)
        {
            prefab = prefabs[Random.Range(0, prefabs.Count)];

            return Get(prefab);
        }

        public T Get(out T prefab, int index)
        {
            prefab = prefabs[index];

            return Get(prefab);
        }

        public T Get(T prefab)
        {
            return _storage[prefab].Get(prefab);
        }

        public void Release(T prefab, T poolObject)
        {
            _storage[prefab].Release(poolObject);
        }

    }
}