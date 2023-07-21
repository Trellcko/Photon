using Fusion;
using UnityEngine;
using UnityEngine.AI;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class MoveToOponentBaseState : BaseState
	{
		private readonly NavMeshAgent _navMeshAgent;
		private readonly Transform _oponentBasePoint;
		private readonly NetworkRigidbody _rigibody;


		public MoveToOponentBaseState(NavMeshAgent navMeshAgent, NetworkRigidbody rigibody, OpponentChecker opponentAreaChecker, Transform target, float speed) 
		{
			_navMeshAgent = navMeshAgent;
			_oponentBasePoint = target;
			_rigibody = rigibody;
			_navMeshAgent.speed = speed;

			GoToState<AttackingState>(() => opponentAreaChecker.LastTarget);
		}

        public override void Enter()
        {
			_navMeshAgent.isStopped = false;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.destination = _oponentBasePoint.position;
        }

        public override void Exit()
        {
			_navMeshAgent.isStopped = true;
        }

        public override void FixedUpdate()
        {
			_rigibody.Transform.position = _navMeshAgent.nextPosition;
        }
    }
}
