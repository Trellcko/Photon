using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class AttackingState : BaseState
	{
        private Health _healthOpponet;

        private readonly OpponentChecker _areaChecker;

        private readonly float _damage;

        public AttackingState(OpponentChecker areaChecker, float damage)
        {
            _areaChecker = areaChecker;
            _damage = damage;

            GoToState<MoveToOponentBaseState>(() => !_areaChecker.LastTarget);
        }

        public override void Update()
        {
            Debug.Log("Attack");
            _healthOpponet.TakeDamage(_damage);
        }
    }
}
