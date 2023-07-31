using Fusion;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class DieState : BaseState
    {
        private readonly AnimatorController _animator;

        private readonly NetworkRunner _networkRunner;
        private readonly NetworkObject _networkObject;

        public DieState(NetworkRunner networkRunner, NetworkObject networkObject, AnimatorController animator)
        {
            _animator = animator;
            _networkRunner = networkRunner;
            _networkObject = networkObject;
        }

        public override void Enter()
        {
            _animator.DieAnimationCompleted += Die;
            _animator.PlayDieAnimation();
        }

        private void Die()
        {
            _animator.DieAnimationCompleted -= Die;
            _networkRunner.Despawn(_networkObject);
        }
    }
}
