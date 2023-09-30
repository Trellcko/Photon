using Fusion;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko
{
	public class GameResultHandler : NetworkBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            NetworkRunnerSpawner.LeaveSession += OnLeaveSession;
        }

        private void OnDisable()
        {
            NetworkRunnerSpawner.LeaveSession -= OnLeaveSession;
        }

        private void OnLeaveSession(PlayerRef obj)
        {
			if(obj != Runner.LocalPlayer)
			{
                _text.SetText($"You win!");
            }
		}

        [Rpc(RpcSources.All, RpcTargets.All)]
		public void SetLooseToRpc(PlayerRef loose)
		{
			print(loose.ToString() + " " + Runner.LocalPlayer);
			if (Runner.LocalPlayer == loose)
			{
				_text.SetText($"You loose!");
			}
			else
			{
                _text.SetText($"You win!");
            }
        }
	}
}

