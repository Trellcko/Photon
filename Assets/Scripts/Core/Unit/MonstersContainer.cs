using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class MonstersContainer : MonoBehaviour
	{
		[Networked] public Dictionary<Side, List<Health>> Spawned { get; set; }

		public void Add(Side side, Health health)
		{
			Spawned[side].Add(health);
			health.Died += () => { Spawned[side].Remove(health); };
		}

		//TODO: can be change to KDTree alghoritm
		public Health GetCloset(Transform to, Side side, float distance)
		{
			Health result = null;

			foreach(Health health in Spawned[side]) 
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
