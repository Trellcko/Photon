using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.UI
{
    public class MainMenuUIHandler : MonoBehaviour
	{
		[SerializeField] private Canvas _playerDetailCanvas;
		[SerializeField] private Canvas _sessionListCanvas;

        private void OnEnable()
        {
            NetworkRunnerSpawner.JoinedToLobby += OnFindGameButtonClicked;
        }

        private void OnDisable()
        {
            NetworkRunnerSpawner.JoinedToLobby -= OnFindGameButtonClicked;
        }

        private void OnFindGameButtonClicked()
        {
            _playerDetailCanvas.gameObject.SetActive(false);
            _sessionListCanvas.gameObject.SetActive(true);
        }
    }
}
