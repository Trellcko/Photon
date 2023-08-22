using Fusion;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class PlayerInitializer : NetworkBehaviour
	{
		[SerializeField] private Base prefab;

		[Networked, HideInInspector] public int InitializeCount { get; set; }

		[SerializeField] private Base _leftBase;
		[SerializeField] private Base _rigthBase;

        public override void Spawned()
        {
            base.Spawned();
			Initialize();
        }

        public void Initialize()
		{
			if(InitializeCount == 0)
			{
				print("Left base was init with ID: " + Runner.LocalPlayer);
                _leftBase.Init(Runner.Simulation.LocalPlayer);
			}
			else
            {
                print("Rigth base was init with ID: " + Runner.LocalPlayer);
                _rigthBase.Init(Runner.Simulation.LocalPlayer);
			}
			InitializeCount++;
		}
	}
}
