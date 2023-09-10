using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.UI
{
    public class MainMenuUIHandler : MonoBehaviour
	{
		[SerializeField] private Canvas _playerDetailCanvas;
		[SerializeField] private Canvas _sessionListCanvas;
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
            _playerDetailCanvas.gameObject.SetActive(false);
            _sessionListCanvas.gameObject.SetActive(true);
        }
    }
}
