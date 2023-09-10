using Trellcko.MonstersVsMonsters.Core;
using Trellcko.MonstersVsMonsters.UI;
using UnityEngine;

namespace Trellcko
{
	public class MainMenuUIHandler : MonoBehaviour
	{
		[SerializeField] private PlayerDetail _playerDetailPanel;
		[SerializeField] private SessionList _sessionList;
        [SerializeField] private NetworkRunnerSpawner _runnerSpawner;

        private void OnEnable()
        {
            _runnerSpawner.JoinedToLobby += OnFindGameButtonClicked;
        }

        private void OnDisable()
        {
            _runnerSpawner.JoinedToLobby -= OnFindGameButtonClicked;
        }

        private void OnFindGameButtonClicked()
        {
            _playerDetailPanel.gameObject.SetActive(false);
            _sessionList.gameObject.SetActive(true);
        }
    }
}
