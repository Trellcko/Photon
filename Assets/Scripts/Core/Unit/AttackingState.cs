using Fusion;
using Trellcko.MonstersVsMonsters.Core.SM;
using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private readonly OpponentChecker _areaChecker;
        private readonly AnimatorController _animator;
        private readonly NetworkRunner _runner;
        private readonly AttackHandler _attackHandler;

        public AttackingState(NetworkRunner runner, OpponentChecker areaChecker, AnimatorController animator, AttackHandler attackHandler, float attackDistance)
        {
            _runner = runner;
            _animator = animator;
            _areaChecker = areaChecker;
            _attackHandler = attackHandler;
            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
            GoToState<PursueState>(() => _areaChecker.LastTarget && !Vector3Extensions.SqrVectorDistacneCheck(_areaChecker.transform.position, _areaChecker.LastTargetPosition, attackDistance));
        }

        public override void Enter()
        {
            _animator.PlayIdle();
            _attackHandler.Enalbe();
            _areaChecker.enabled = false;
        }

        
        public override void Exit()
        {
            _attackHandler.Disable();
            _areaChecker.enabled = true;
        }
    }
}
