using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private readonly OpponentChecker _areaChecker;

        private readonly float _damage;
        private readonly float _attackReload;

        private float _currentTime;

        public AttackingState(OpponentChecker areaChecker, float damage, float attackDistance, float attackReload)
        {
            _attackReload= attackReload;
            _areaChecker = areaChecker;
            _damage = damage;

            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
            GoToState<PursueState>(() => areaChecker.LastTarget && !Vector3Extensions.SqrVectorDistacneCheck(_areaChecker.transform.position, _areaChecker.LastTargetPosition, attackDistance));
        }

        public override void Enter()
        {
            Debug.Log($"{_areaChecker.name} start attack {_areaChecker.LastTarget.name}");
            _currentTime = 0f;
            _areaChecker.enabled = false;
        }

        public override void Exit()
        {
            Debug.Log($"{_areaChecker.name} stop attack");

            _areaChecker.enabled = true;
        }

        public override void Update()
        {
            if (_currentTime > _attackReload)
            {
                Debug.Log($"{_areaChecker.name} take {_damage} {_areaChecker.LastTarget.name}" +
                    $"he has {_areaChecker.LastTarget.Value}");

                _currentTime = 0f;
                _areaChecker.LastTarget.TakeDamage(_damage);
            }
            _currentTime += Time.deltaTime;
        }
    }
}
