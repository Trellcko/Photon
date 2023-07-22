using Fusion;
using Trellcko.MonstersVsMonsters.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class PursueState : BaseState
	{
		private readonly OpponentChecker _opponentChecker;
		private readonly NavMeshAgent _navMeshAgent;
		private readonly NetworkRigidbody _rigidbody;

		public PursueState(OpponentChecker opponentChecker, NavMeshAgent navMeshAgent, NetworkRigidbody rigidbody, float attackDistance, float detectDistance)
		{
			_opponentChecker = opponentChecker;
			_navMeshAgent = navMeshAgent;
			_rigidbody = rigidbody;

			GoToState<MoveToOponentBaseState>(() => !_opponentChecker.LastTarget);

			GoToState<MoveToOponentBaseState>(() => Vector3Extensions.SqrVectorDistacneCheck(_opponentChecker.transform.position, _opponentChecker.LastTargetPosition, detectDistance));

			GoToState<AttackingState>(() => !Vector3Extensions.SqrVectorDistacneCheck(_opponentChecker.transform.position, _opponentChecker.LastTargetPosition, attackDistance));
		}

        public override void Enter()
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.destination = _opponentChecker.LastTargetPosition;
        }

        public override void FixedUpdate()
        {
            _rigidbody.Transform.position = _navMeshAgent.nextPosition;
			_navMeshAgent.destination = _opponentChecker.LastTargetPosition;
        }

        public override void Exit()
        {
            _navMeshAgent.isStopped = true;
        }

    }
}