using Fusion;
using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private readonly OpponentChecker _areaChecker;
        private readonly AnimatorController _animator;
        private readonly NetworkRunner _runner;

        private readonly float _damage;
        private readonly float _attackReload;
        private readonly bool _isMelee;

        private bool _isAttacking;

        private float _currentTime;

        public AttackingState(NetworkRunner runner, OpponentChecker areaChecker, AnimatorController animator, float damage, float attackDistance, float attackReload, bool isMelee)
        {
            _runner = runner;
            _animator = animator;
            _attackReload = attackReload;
            _areaChecker = areaChecker;
            _damage = damage;
            _isMelee = isMelee;

            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
            GoToState<PursueState>(() => _areaChecker.LastTarget && !Vector3Extensions.SqrVectorDistacneCheck(_areaChecker.transform.position, _areaChecker.LastTargetPosition, attackDistance));
            _isMelee = isMelee;
        }

        public override void Enter()
        {
            _isAttacking = false;
            _animator.PlayIdle();

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
            _animator.StopMeleeAttack();
            _isAttacking = false;
            _currentTime = 0f;
            Debug.Log($"{_areaChecker.name} take {_damage} {_areaChecker.LastTarget.name}" +
    $"he has {_areaChecker.LastTarget.Value}");
            _areaChecker.LastTarget.TakeDamage(_damage);
        }

        public override void Exit()
        {
            Debug.Log($"{_areaChecker.name} stop attack");
            _animator.MeleeAnimationCompleted -= OnMeleeAnimationCompleted;
            _areaChecker.enabled = true;
        }

        public override void FixedUpdate()
        {
            if (_currentTime > _attackReload && !_isAttacking)
            {
                _isAttacking = true;
                if (_isMelee)
                {
                    _animator.PlayMelleAttack();
                }
            }
            _currentTime += _runner.DeltaTime;
        }
    }
}
