using Fusion;
using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class MonsterBehaviour : NetworkBehaviour
	{
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NetworkRigidbody _rigibody;
        [SerializeField] private AreaChecker _opponentAreaChecker;
        [SerializeField] private Health _health;

		private StateMachine _stateMachine;

        private void OnEnable()
        {
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void OnDied()
        {
            Runner.Despawn(Object);    
        }

        public override void FixedUpdateNetwork()
        {
            _stateMachine?.FixedUpdate();
        }

        public void SetTransform(Vector3 position, Vector3 rotation)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _navMeshAgent.Warp(transform.position);

        }

        public void Init(MonsterData monsterData, Transform target, LayerMask me, LayerMask opponent)
        {
            _health.Init(monsterData.Health);

            gameObject.layer = me;
            _opponentAreaChecker.OpponentLayerMask = opponent;
            MoveToOponentBaseState moveToOpponentBaseState = new(_navMeshAgent, _rigibody, _opponentAreaChecker, target);

            _stateMachine = new StateMachine(moveToOpponentBaseState);
            _stateMachine.SetState<MoveToOponentBaseState>();
        }

    }
}
