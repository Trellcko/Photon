using System;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class PlayerDetail : MonoBehaviour
	{
		[SerializeField] private TMP_InputField _playerNameInputField;
		[SerializeField] private Button _findGameButton;
        [SerializeField] private NetworkRunnerSpawner _networkRunnerSpawner;

        public event Action FindGameButtonClicked;

        private void OnEnable()
        {
            _findGameButton.onClick.AddListener(FindGame);
                        
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(Constants.NAME))
            {
                _playerNameInputField.text = PlayerPrefs.GetString(Constants.NAME);
            }   
        }

        private void OnDisable()
        {
            _findGameButton.onClick.RemoveListener(FindGame);
        }
        private void FindGame()
        {
            PlayerPrefs.SetString(Constants.NAME, _playerNameInputField.text);
            _networkRunnerSpawner.JoinLobby();
            FindGameButtonClicked?.Invoke();
        }
    }
}
