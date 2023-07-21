using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;

namespace Trellcko
{
	public class Test : MonoBehaviour
	{
		[SerializeField] private Health _health;

        private void Awake()
        {
            _health.Init(10000, Side.Neutral);
        }
    }
}
