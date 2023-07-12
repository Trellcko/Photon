using Fusion;
using System.Collections.Generic;
using Trellcko.MonstersVsMonsters.Core.Unit;
using Trellcko.MonstersVsMonsters.UI;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class PlayerInitializer : NetworkBehaviour
	{
		[SerializeField] private Base prefab;

		[Networked, HideInInspector] public int InitializeCount { get; set; }

		[SerializeField] private GamePlayUIController _gamePlayUIInitializer;

		[SerializeField] private List<Spawner> _leftSpawners;
		[SerializeField] private List<Spawner> _rigthSpawners;

        public override void Spawned()
        {
            base.Spawned();
			Initialize();
        }

        public void Initialize()
		{
			var monsterBase = Instantiate(prefab);


			if(InitializeCount == 0)
			{
				monsterBase.Init(_leftSpawners, Runner.LocalPlayer);
				_gamePlayUIInitializer.EnableLeftPlayerUI(monsterBase);
			}
			else
            {
                monsterBase.Init(_rigthSpawners, Runner.LocalPlayer);
                _gamePlayUIInitializer.EnableRightPlayerUI(monsterBase);
			}
			InitializeCount++;
		}
	}
}
