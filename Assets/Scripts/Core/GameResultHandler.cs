using Fusion;
using TMPro;
using UnityEngine;

namespace Trellcko
{
	public class GameResultHandler : NetworkBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		[Rpc(RpcSources.StateAuthority, RpcTargets.All)]
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

