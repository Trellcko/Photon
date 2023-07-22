using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Utils
{
	public static class Vector3Extensions
	{

		/// <summary>
		/// Return true if distance between vectors lesser than value and false if more
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="distance"></param>
		/// <returns></returns>
		public static bool SqrVectorDistacneCheck(Vector3 a, Vector3 b, float distance)
		{
			float sqrMagnitude = Vector3.SqrMagnitude(a - b);

			return sqrMagnitude < distance*distance;
		}
	}
}
