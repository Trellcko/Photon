using Fusion;
using System;
using System.Collections;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Health : NetworkBehaviour
	{
		[Networked] public float MaxHealth { get; set; }
		[Networked(OnChanged = nameof(OnHealthChanged))] public float Value { get; set; }
		[Networked] public Side Side { get; set; }

        public bool IsDead => Value <= 0;

        public bool IsSpawned { get; private set; }

        public event Action<float> Changed;
		public event Action Died;

        public override void Spawned()
        {
			base.Spawned();
			IsSpawned = true;
        }

        public void Init(float maxHealth, Side side)
		{
			Side = side;
			MaxHealth = maxHealth;
			Value = maxHealth;
		}

		[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
		public void TakeDamageRpc(float damage)
		{
            if (IsDead) return;

            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
			
			Value -= damage;

		}

		public void StartDiedCoruntine()
		{
			StartCoroutine(DieCorun());
		}

		private IEnumerator DieCorun()
		{
			yield return null;
			Died?.Invoke();
		}

		private static void OnHealthChanged(Changed<Health> changed)
		{
			var health = changed.Behaviour;

			if (health.Value > 0)
			{
				health.Changed?.Invoke(health.Value);
			}
			else
			{
				health.StartDiedCoruntine();
			}
		}


	}

	public enum Side
	{
		Rigth,
		Left,
		Neutral
	}
}
