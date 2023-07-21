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
        [SerializeField] private OpponentChecker _opponentAreaChecker;
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

        public void Init(MonsterData monsterData, Transform target, Side side)
        {
            _health.Init(monsterData.Health, side);

            _opponentAreaChecker.MySide = side;
            MoveToOponentBaseState moveToOpponentBaseState = new(_navMeshAgent, _rigibody, _opponentAreaChecker, target, monsterData.Speed);
            AttackingState attackingState = new(_opponentAreaChecker, monsterData.Damage);

            _stateMachine = new StateMachine(moveToOpponentBaseState, attackingState);
            _stateMachine.SetState<MoveToOponentBaseState>();
        }

    }
}
