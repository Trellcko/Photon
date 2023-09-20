using Fusion;
using System;
using System.Collections.Generic;
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

        public List<PlayerRef> InitalizedPlayerRefs { get; private set; } = new();

        public PlayerRef OpponentRef { get; private set; }

        public override void Spawned()
        {
            base.Spawned();
			Initialize();
        }

        [Rpc]
        public void RPCPlayerInitializedInvoke([RpcTarget] PlayerRef playerRef, PlayerRef opponentRef)
        {
            PlayerInititalized?.Invoke(2);
            OpponentRef = opponentRef;
            Debug.Log(OpponentRef);
            SavePlayerRefs();
        }

        private void SavePlayerRefs()
        {
            InitalizedPlayerRefs.Add(_leftBase.Owner);
            InitalizedPlayerRefs.Add(_rigthBase.Owner);
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
                OpponentRef = _leftBase.Owner;
                PlayerInititalized?.Invoke(2); 
                SavePlayerRefs();
                RPCPlayerInitializedInvoke(_leftBase.Owner, Runner.LocalPlayer);
            }
        }
	}
}
