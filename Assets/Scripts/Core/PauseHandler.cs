using Fusion;
using System;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class PauseHandler : NetworkBehaviour
	{
		public event Action ChandedState;

		public bool IsPaused { get; private set; }

		public void ChangeState(PlayerRef opponent)
		{
			if (IsPaused)
			{

                Pause(); 
				RPCPause(opponent);
            }
			else
            {
                UnPause();
                RPCUnpause(opponent);
            }
		}

		[Rpc]

        private void RPCPause([RpcTarget] PlayerRef player)
		{
			Pause();
		}

		[Rpc]

        private void RPCUnpause([RpcTarget] PlayerRef player)
		{
			UnPause();
		}
        private void Pause()
        {
            Time.timeScale = 0f;
			IsPaused = true;
        }

        private void UnPause()
		{
            Time.timeScale = 1f;
            IsPaused = false;
        }
	}
}
