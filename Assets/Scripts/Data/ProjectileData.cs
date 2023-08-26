using Fusion;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Data
{
	[CreateAssetMenu(fileName = "New Projectile Data", menuName = "SO/ProjectileData", order = 41)]
	public class ProjectileData : ScriptableObject
	{
		[field: SerializeField] public float Speed { get; private set; }
		[field: SerializeField] public float Damage { get; private set; }
		[field: SerializeField] public Projectile Prefab { get; private set; }

		public Projectile Create(Vector3 position, Vector3 direction, NetworkRunner runner)
		{
			var spawned = runner.Spawn(Prefab, position);
			spawned.Init(Speed, Damage, direction);

			return spawned;
		}
	}
}
