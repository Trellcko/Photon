using Fusion;
using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public abstract class AttackHandler : NetworkBehaviour
	{
		[SerializeField] protected AnimatorController Animator;
		[SerializeField] protected OpponentChecker OpponentChecker;
		[SerializeField] private MonsterData _data;
        
		public float Reload => _data.Reload;
        public float Damage => _data.MeleeDamage;

        public float CurrentTime { get; protected set; }

        public bool IsAttacking { get; protected set; }

        private bool _isEnable = false;


		public void Enalbe()
		{
            IsAttacking = false;
			_isEnable = true;
            CurrentTime = 0f;
            OnEnabled();

        }

        public void Disable()
		{
			_isEnable = false;
            CurrentTime = 0f;
            OnDisabled();
        }

        public override void FixedUpdateNetwork()
        {
            if (!_isEnable)
            {
                return;
            }

            if (CurrentTime > Reload && !IsAttacking)
            {
                IsAttacking = true;
                OnReloadFinished();
            }
            CurrentTime += Runner.DeltaTime;

        }

        public abstract void OnReloadFinished();
        protected virtual void OnEnabled() { }
		protected virtual void OnDisabled() { }
    }
}
