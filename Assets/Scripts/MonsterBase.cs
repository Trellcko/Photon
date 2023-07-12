using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko
{
	public class MonsterBase : MonoBehaviour
	{
		private List<Spawner> _spawners;
		
		public void Init(List<Spawner> spawners, PlayerRef playerRef)
		{
			_spawners = spawners;
		}

		public void SpawnMonster()
		{
			_spawners[0].Spawn();
		}
	}
}
