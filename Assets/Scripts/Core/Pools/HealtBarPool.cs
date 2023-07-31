using Fusion;
using Trellcko.MonstersVsMonsters.UI;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Pools
{

    public class HealtBarPool : Pool<HealtBar>
    {
        [Rpc(RpcSources.All, RpcTargets.All)]
        public HealtBar GetRpc(out HealtBar prefab, int index, Transform parent)
        {
            return Get(out prefab, index, parent);
        }

        [Rpc]
        public void ReleaseRpc(HealtBar prefab, HealtBar spawned)
        {
            Release(prefab, spawned);
        }
    }
}