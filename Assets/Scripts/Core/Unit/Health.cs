using Fusion;
using System;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Health : MonoBehaviour
	{
		[Networked] public float MaxHealth { get; set; }
		
		[Networked] public float Value { get; set; }

        public bool IsDead => Value <= 0;
		

		public event Action Changed;
		public event Action Died;

		public void Init(float maxHealth)
		{
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
				return;
			}
			Changed?.Invoke();

		}

	}
}