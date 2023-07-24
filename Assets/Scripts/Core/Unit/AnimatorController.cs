using Fusion;
using System;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class AnimatorController : MonoBehaviour
	{
		[SerializeField] private NetworkMecanimAnimator _animator;

		public event Action DieAnimationCompleted;
		public event Action MeleeAnimationCompleted;
		public event Action RangeAnimationCompleted;

		private const string Range = "Range";
		private const string Melee = "Melee";
		private const string IsAttacking = "IsAttacking";
		private const string Win = "Win";
		private const string Die = "Die";
		private const string Speed = "Speed";


		public void InvokeMeleeAnimationComplete()
		{
			MeleeAnimationCompleted?.Invoke();
		}

		public void InvokeRangeAnimationCompleted()
		{
			RangeAnimationCompleted?.Invoke();
		}
		public void InvokeDieAnimationCompleted()
		{
			DieAnimationCompleted?.Invoke();
		}

		public void SetMeleeTrigger()
		{
			_animator.SetTrigger(Melee);
		}

        public void SetRangeTrigger()
        {
			_animator.SetTrigger(Range);
        }

		public void EnableAttackState()
		{
			_animator.Animator.SetBool(IsAttacking, true);
		}

		public void DisableAttackState()
		{
			_animator.Animator.SetBool(IsAttacking, false);
		} 
		
		public void PlayWinAnimation()
		{
			_animator.SetTrigger(Win);
		}

		public void PlayDieAnimation()
		{
			_animator.SetTrigger(Die);
		}

		public void EnableMovement()
		{
			_animator.Animator.SetFloat(Speed, 1);
		}
        public void DisableMovement()
        {
			_animator.Animator.SetFloat(Speed, -1);
        }

    }
}
