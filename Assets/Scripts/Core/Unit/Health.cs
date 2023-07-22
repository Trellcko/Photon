using Fusion;
using System;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Health : MonoBehaviour
	{
		[Networked] public float MaxHealth { get; set; }
		[Networked] public float Value { get; set; }
		[Networked] public Side Side { get; set; }

        public bool IsDead => Value <= 0;
		

		public event Action Changed;
		public event Action Died;

		public void Init(float maxHealth, Side side)
		{
			Side= side;
			MaxHealth = maxHealth;
			Value = maxHealth;
		}

		public void TakeDamage(float damage)
		{
			damage = Mathf.Clamp(damage, 0, Mathf.Infinity);

			if(IsDead) return;

			Value -= damage;
			if(Value < 0)
			{
				Died?.Invoke();
				Died = null;
				return;
			}
			Changed?.Invoke();

		}

	}

	public enum Side
	{
		Rigth,
		Left,
		Neutral
	}
}
