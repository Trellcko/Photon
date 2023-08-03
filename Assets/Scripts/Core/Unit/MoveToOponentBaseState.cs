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
		private readonly AnimatorController _animator;


		public MoveToOponentBaseState(NavMeshAgent navMeshAgent, NetworkRigidbody rigibody, OpponentChecker opponentAreaChecker, AnimatorController animator, Transform target, float speed) 
		{
			_animator = animator;
			_navMeshAgent = navMeshAgent;
			_oponentBasePoint = target;
			_rigibody = rigibody;
			_navMeshAgent.speed = speed;

			GoToState<PursueState>(() => opponentAreaChecker.LastTarget);
		}

        public override void Enter()
        {
			Debug.Log($"{_rigibody.name} GO To OpponentBase");
			_animator.PlatMovmentFowrad();
			_navMeshAgent.isStopped = false;
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.destination = _oponentBasePoint.position;
        }

        public override void Exit()
        {

            Debug.Log($"{_rigibody.name} not more go to OpponentBase");
            _navMeshAgent.isStopped = true;
        }

        public override void FixedUpdate()
        {
			_rigibody.Transform.position = Vector3.MoveTowards(_rigibody.Transform.position, _navMeshAgent.nextPosition, _rigibody.Runner.DeltaTime);
        }
    }
}
