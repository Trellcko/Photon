using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class MonstersContainer : MonoBehaviour
	{
		[Networked] private Dictionary<Side, List<Health>> _spawned { get; set; }

		public void Add(Side side, Health health)
		{
			_spawned[side].Add(health);
			health.Died += () => { _spawned[side].Remove(health); };
		}

		//TODO: can be change to KDTree alghoritm
		public Health GetCloset(Transform to, Side side, float distance)
		{
			Health result = null;

			foreach(Health health in _spawned[side]) 
			{
				Transform target = health.transform;

				float squMagnitude = Vector3.SqrMagnitude(to.position - target.position);


                if (squMagnitude*squMagnitude < distance)
				{
					result = health;
					break;
				}
			}
			return result;
		}
	}
}
