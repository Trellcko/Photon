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
				_leftBase.Init(Runner.LocalPlayer);
			}
			else
            {
                _rigthBase.Init(Runner.LocalPlayer);
			}
			InitializeCount++;
		}
	}
}
