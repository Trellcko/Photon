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
		public event Action RangeAnimationAttackFrameCompleted;

		private const string Range = "IsRangeAttacking";
		private const string Melee = "IsMeleeAttacking";
		private const string Win = "Win";
		private const string Die = "Die";
		private const string Speed = "Speed";

		public void InvokeRangeAnimationAttackFrameCompleted()
		{
			RangeAnimationAttackFrameCompleted?.Invoke();
		}

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

		public void StopRangeAttack()
		{
			_animator.Animator.SetBool(Range, false);
		}

		public void StopMeleeAttack()
		{
			_animator.Animator.SetBool(Melee, false);
		}

		public void PlayMelleAttack()
		{
			_animator.Animator.SetBool(Melee, true);
		}

        public void PlayRangeAttack()
        {
			_animator.Animator.SetBool(Range, true);
        }

		public void PlayIdle()
		{
			_animator.Animator.SetFloat(Speed, 0f);
		}
		
		public void PlayWinAnimation()
		{
			_animator.SetTrigger(Win);
		}

		public void PlayDieAnimation()
		{
			_animator.SetTrigger(Die);
		}

		public void PlatMovmentFowrad()
		{
			_animator.Animator.SetFloat(Speed, 1);
		}

    }
}
