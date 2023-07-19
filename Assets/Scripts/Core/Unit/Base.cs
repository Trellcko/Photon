using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Base : MonoBehaviour
	{
		[SerializeField] private List<Spawner> _spawners;
		
		public void Init(PlayerRef playerRef)
		{
			foreach(var spawner in _spawners) 
			{
				spawner.Init(playerRef);
			}
		}
	}
}
