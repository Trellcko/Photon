using Fusion;
using System;
using System.Collections;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Health : NetworkBehaviour
	{
		[Networked] public float MaxHealth { get; set; }
		[Networked] public float Value { get; set; }
		[Networked] public Side Side { get; set; }

        public bool IsDead => Value <= 0;
		

		public event Action Changed;
		public event Action Died;

		public void Init(float maxHealth, Side side)
		{
			Side = side;
			MaxHealth = maxHealth;
			Value = maxHealth;
		}

		public void TakeDamage(float damage)
		{
            if (IsDead) return;

            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
			
			Value -= damage;
			if(Value <= 0)

			{
				StartCoroutine(DieCorun());
				return;
			}
			Changed?.Invoke();

		}

		private IEnumerator DieCorun()
		{
			yield return null;
			Debug.Log("Died");
            Died?.Invoke();
            Died = null;
        }

	}

	public enum Side
	{
		Rigth,
		Left,
		Neutral
	}
}
