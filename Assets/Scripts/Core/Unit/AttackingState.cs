using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private readonly OpponentChecker _areaChecker;
        private readonly AnimatorController _animator;

        private readonly float _damage;
        private readonly float _attackReload;
        private readonly bool _isMelee;

        private float _currentTime;

        public AttackingState(OpponentChecker areaChecker, AnimatorController animator, float damage, float attackDistance, float attackReload, bool isMelee)
        {
            _animator = animator;
            _attackReload = attackReload;
            _areaChecker = areaChecker;
            _damage = damage;
            _isMelee = isMelee;

            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
            GoToState<PursueState>(() => areaChecker.LastTarget && !Vector3Extensions.SqrVectorDistacneCheck(_areaChecker.transform.position, _areaChecker.LastTargetPosition, attackDistance));
            _isMelee = isMelee;
        }

        public override void Enter()
        {
            _animator.DisableMovement();
            _animator.EnableAttackState();

            if (_isMelee)
            {
                _animator.MeleeAnimationCompleted += OnMeleeAnimationCompleted;
            }

            Debug.Log($"{_areaChecker.name} start attack {_areaChecker.LastTarget.name}");
            _currentTime = 0f;
            _areaChecker.enabled = false;
        }

        private void OnMeleeAnimationCompleted()
        {
            _areaChecker.LastTarget.TakeDamage(_damage);
        }

        public override void Exit()
        {
            Debug.Log($"{_areaChecker.name} stop attack");
            _animator.MeleeAnimationCompleted -= OnMeleeAnimationCompleted;
            _areaChecker.enabled = true;
        }

        public override void Update()
        {
            if (_currentTime > _attackReload)
            {
                Debug.Log($"{_areaChecker.name} take {_damage} {_areaChecker.LastTarget.name}" +
                    $"he has {_areaChecker.LastTarget.Value}");
                if (_isMelee)
                {
                    _animator.SetMeleeTrigger();
                }
                _currentTime = 0f;
            }
            _currentTime += Time.deltaTime;
        }
    }
}
