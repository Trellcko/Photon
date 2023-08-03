using Fusion;
using System;
using System.Collections.Generic;

namespace Trellcko.MonstersVsMonsters.Core.Pools
{
    public class ObjectNetworkPool<T> where T : NetworkBehaviour
    {
        private Stack<T> _stack;

        private int _saveToDestroyCount;

        private Func<T> _create;

        public ObjectNetworkPool(Func<T> create)
        {
            _create = create;
            _stack = new Stack<T>();
            _saveToDestroyCount = -1;
        }

        public T Get(T obj)
        {
            if (_stack.Count > 0)
            {
                var result = _stack.Pop();
                RPC_Active(result);

                if (_saveToDestroyCount == -1)
                    _saveToDestroyCount = _stack.Count;
                else if (_stack.Count < _saveToDestroyCount)
                    _saveToDestroyCount = _stack.Count;

                return result;
            }

            return CreateNew(obj);
        }

        public T CreateNew(T obj)
        {
            var instance = _create();
            return instance;
        }

        public void Release(T obj)
        {
            RPC_Disable(obj);
            _stack.Push(obj);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_Active(NetworkBehaviour behaviour)
        {
            behaviour.gameObject.SetActive(true);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_Disable(NetworkBehaviour behaviour)
        {
            behaviour.gameObject.SetActive(false);
        }

    }
}