using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private readonly OpponentChecker _areaChecker;

        private readonly float _damage;
        public AttackingState(OpponentChecker areaChecker, float damage, float attackDistance)
        {
            _areaChecker = areaChecker;
            _damage = damage;

            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
            GoToState<PursueState>(() => Vector3Extensions.SqrVectorDistacneCheck(_areaChecker.transform.position, _areaChecker.LastTargetPosition, attackDistance));
        }

        public override void Enter()
        {
            _areaChecker.enabled = false;
        }

        public override void Exit()
        {
            _areaChecker.enabled = true;
        }

        public override void Update()
        {
            _areaChecker.LastTarget.TakeDamage(_damage);
        }
    }
}
