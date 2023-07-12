using Fusion;
using UnityEngine;

namespace Trellcko
{
	public class Spawner : NetworkBehaviour
	{
		[SerializeField] private TestMove _testmove;
		[SerializeField] private Transform _target;
		[SerializeField] private Transform _spawnPoint;

	
		public void Spawn()
		{
			var spawned = Runner.Spawn(_testmove, _spawnPoint.position);
			spawned.Init(_spawnPoint.position, _target.position);
		}
	}
}
