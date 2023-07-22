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

		[field: Header("Damage")]
		[field: SerializeField] public float Damage { get; private set; }
		[field: SerializeField] public float Reload { get; private set; }
		[field: SerializeField] public float AttackDistnace { get; private set; }
		[field: SerializeField] public float DetectDistance { get; private set; }


		[field: Header("Movement")]
		[field: SerializeField] public float Speed { get; private set; }
	}
}
