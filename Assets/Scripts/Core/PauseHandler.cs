using Fusion;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core
{
    public class PauseHandler : NetworkBehaviour
	{
		public event Action ChandedState;

        public static PauseHandler Instance => _instance;

        private static PauseHandler _instance;

		public bool IsPaused { get; private set; }

        private List<IPaused> _pauseds = new List<IPaused>();

        public override void Spawned()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(_instance.gameObject);

            }
            if (FindObjectsOfType<PauseHandler>().Length > 1)
            {
                Destroy(gameObject);
            }
        }



        public void Register(IPaused paused)
        {
            _pauseds.Add(paused);
        }

        public void UnRegister(IPaused paused) 
        {
            _pauseds.Remove(paused);
        }

		public void ChangeState(PlayerRef opponent)
		{
			if (!IsPaused)
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
            print(player);
			Pause();
		}

		[Rpc]

        private void RPCUnpause([RpcTarget] PlayerRef player)
		{
			UnPause();
		}
        private void Pause()
        {
            IsPaused = true;
            print("Pause");
            foreach(var paused in _pauseds)
            {
                paused.Pause();
            }
        }

        private void UnPause()
        {
            IsPaused = false; 
            foreach (var paused in _pauseds)
            {
                paused.UnPause();
            }
        }
    }
}
