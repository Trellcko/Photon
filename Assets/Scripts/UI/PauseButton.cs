using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class PauseButton : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private PauseHandler _pauseHandler;
		[SerializeField] private PlayerInitializer _playerInitializer;

        private void Awake()
        {
            _button.interactable = false;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _playerInitializer.PlayerInititalized += OnPlayerInititalized;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _playerInitializer.PlayerInititalized -= OnPlayerInititalized;
        }

        private void OnClick()
        {
            _pauseHandler.ChangeState();
        }

        private void OnPlayerInititalized(int obj)
        {
            _button.interactable = obj == 2;
        }
    }
}
