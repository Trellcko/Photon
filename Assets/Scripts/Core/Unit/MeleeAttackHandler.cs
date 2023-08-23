namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class MeleeAttackHandler : AttackHandler
	{
        protected override void OnEnabled()
        {
            Animator.MeleeAnimationCompleted += OnMeleeAnimationCompleted;
        }


        protected override void OnDisabled()
        {
            Animator.MeleeAnimationCompleted += OnMeleeAnimationCompleted;
        }

        private void OnMeleeAnimationCompleted()
        {
            Animator.StopMeleeAttack();
            IsAttacking = false;
            CurrentTime = 0f;
            OpponentChecker.LastTarget.TakeDamageRpc(Damage);

        }

        public override void OnReloadFinished()
        {
            Animator.PlayMelleAttack();
        }
    }
}
