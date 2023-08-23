using Fusion;
using Trellcko.MonstersVsMonsters.Core.SM;
using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class MonsterBehaviour : NetworkBehaviour
	{
        [field: SerializeField] public Health Health { get; private set; }

        [SerializeField] private AnimatorController _animatorController;
        [SerializeField] private AttackHandler _attackHandler;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NetworkRigidbody _rigibody;
        [SerializeField] private OpponentChecker _opponentAreaChecker;

		private StateMachine _stateMachine { get; set; }

        private void OnEnable()
        {
            Health.Died += DestroyMonsterRpc;
        }

        private void OnDisable()
        {
            Health.Died -= DestroyMonsterRpc;
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void DestroyMonsterRpc()
        {
            _stateMachine.SetState<DieState>();
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

            Debug.Log("Init StateMachine");

            name += side.ToString();
            Health.Init(monsterData.Health, side);

            _opponentAreaChecker.Init(side, monsterData.DetectDistance);

            _navMeshAgent.speed = monsterData.Speed;
            _navMeshAgent.acceleration = monsterData.Speed;
            _navMeshAgent.updatePosition = false;

            DieState dieState = new(Runner, Object, _animatorController);
            PursueState pursueState = new(_opponentAreaChecker, _navMeshAgent, _rigibody, _animatorController, monsterData.AttackDistnace, monsterData.DetectDistance);
            MoveToOponentBaseState moveToOpponentBaseState = new(_navMeshAgent, _rigibody, _opponentAreaChecker, _animatorController, target);
            AttackingState attackingState = new(Runner, _opponentAreaChecker, _animatorController, _attackHandler, monsterData.AttackDistnace);

            _stateMachine = new StateMachine(moveToOpponentBaseState, attackingState, pursueState, dieState);
            _stateMachine.SetState<MoveToOponentBaseState>();

        }

    }
}
