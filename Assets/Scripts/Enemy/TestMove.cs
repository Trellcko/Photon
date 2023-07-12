using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

namespace Trellcko
{
	public class TestMove : NetworkBehaviour
	{
		[SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NetworkRigidbody _rigidbody;

        public void Init(Vector3 startPosition, Vector3 target)
        {
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.Warp(startPosition); 
            _navMeshAgent.SetDestination(target);
        }

        public override void FixedUpdateNetwork()
        {
            if (!_navMeshAgent.isStopped)
            {
                if (Vector3.Distance(_navMeshAgent.destination, _rigidbody.Rigidbody.position) < 0.001f)
                {
                    _navMeshAgent.isStopped = true;
                }
                print(_navMeshAgent.nextPosition);
                _rigidbody.TeleportToPosition(_navMeshAgent.nextPosition);
            }
        }
    }
}
