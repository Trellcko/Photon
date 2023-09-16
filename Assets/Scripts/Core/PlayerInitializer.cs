using Fusion;
using System;
using System.Linq;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class PlayerInitializer : NetworkBehaviour
	{
		[SerializeField] private Base prefab;


		[SerializeField] private Base _leftBase;
		[SerializeField] private Base _rigthBase;

        public event Action<int> PlayerInititalized;

        public override void Spawned()
        {
            base.Spawned();
			Initialize();
        }

        [Rpc]
        public void RPCPlayerInitializedInvoke([RpcTarget] PlayerRef playerRef)
        {
            PlayerInititalized?.Invoke(2);
        }

        public void Initialize()
		{
			if(_leftBase.Owner == default)
			{
				print("Left base was init with ID: " + Runner.LocalPlayer);
                _leftBase.Init(Runner.Simulation.LocalPlayer);
                PlayerInititalized?.Invoke(1);

            }
			else
            {
                print("Rigth base was init with ID: " + Runner.LocalPlayer);
                _rigthBase.Init(Runner.Simulation.LocalPlayer);
                PlayerInititalized?.Invoke(2);
                RPCPlayerInitializedInvoke(_leftBase.Owner);
            }
        }
	}
}
