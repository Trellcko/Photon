using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko
{
	public class PlayerDetailSkiper : MonoBehaviour
	{
		[SerializeField] private Canvas _playerDetail;
		[SerializeField] private Canvas _lobby;

		[SerializeField] private NetworkRunnerSpawner _networkRunnerSpawner;

        private static bool s_isEnteringTheGame;

        private void Start()
        {
            if (s_isEnteringTheGame)
            {
                _networkRunnerSpawner.JoinLobby();
                _lobby.gameObject.SetActive(true);
                _playerDetail.gameObject.SetActive(false);
            }
            else
            {
                s_isEnteringTheGame = true;
            }
        }
    }
}
