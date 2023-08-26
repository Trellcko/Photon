using Fusion;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Data
{
	[CreateAssetMenu(fileName = "New Monster Data", menuName = "SO/MonsterData", order = 41)]
	public class MonsterData : ScriptableObject
	{
		[field: SerializeField] public MonsterBehaviour Prefab { get; private set; }

		[field: Header("Stats")]
		[field: Space]

		[field: Header("Health")]
		[field: SerializeField] public float Health { get; private set; }

		[field: Header("Attack")]
		[field: SerializeField] public float MeleeDamage { get; private set; }
		[field: SerializeField] public float Reload { get; private set; }
		[field: SerializeField] public float AttackDistnace { get; private set; }
		[field: SerializeField] public float DetectDistance { get; private set; }


		[field: Header("Movement")]
		[field: SerializeField] public float Speed { get; private set; }

		[field:Header("Cost")]
		[field: SerializeField] public float Gold { get; private set; }


		public MonsterBehaviour Create(Vector3 position, Vector3 rotation, LayerMask layerMask, Side side, Transform targetPoint, NetworkRunner runner)
		{
            var spawned = runner.Spawn(Prefab, position);
            spawned.SetTransform(position, rotation);
            spawned.gameObject.layer = (int)Mathf.Log(layerMask, 2);
            spawned.Init(this, targetPoint, side);

			return spawned;
        }
	}
}
