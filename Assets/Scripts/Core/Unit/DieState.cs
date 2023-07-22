using Fusion;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class DieState : BaseState
    {
        private readonly NetworkRunner _networkRunner;
        private readonly NetworkObject _networkObject;

        public DieState(NetworkRunner networkRunner, NetworkObject networkObject)
        {
            _networkRunner = networkRunner;
            _networkObject = networkObject;
        }

        public override void Enter()
        {
            _networkRunner.Despawn(_networkObject);
        }
    }
}
