using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class RangeOneShootAttackHandler : AttackHandler
    {
        [SerializeField] private ProjectileData _projectilePrefab;
        [SerializeField] private Transform _spawnPoint;
        protected override void OnEnabled()
        {
            Animator.RangeAnimationCompleted += OnRangeAnimationCompleted;
        }


        protected override void OnDisabled()
        {
            Animator.RangeAnimationCompleted += OnRangeAnimationCompleted;
        }

        private void OnRangeAnimationCompleted()
        {
            Vector3 direction = (OpponentChecker.LastTarget.transform.position - transform.position).normalized;
            _projectilePrefab.Create(_spawnPoint.position, direction, Runner);
            IsAttacking = false;
            CurrentTime = 0f;

        }
        public override void OnReloadFinished()
        {
            Animator.PlayRangeAttack();
        }
    }
}
