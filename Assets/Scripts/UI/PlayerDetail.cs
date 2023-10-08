using System;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class PlayerDetail : MonoBehaviour
	{
		[SerializeField] private TMP_InputField _playerNameInputField;
		[SerializeField] private Button _findGameButton;
        [SerializeField] private NetworkRunnerSpawner _networkRunnerSpawner;

        private void OnEnable()
        {
            _findGameButton.onClick.AddListener(FindGame);
                        
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(Constants.Name))
            {
                _playerNameInputField.text = PlayerPrefs.GetString(Constants.Name);
            }   
        }

        private void OnDisable()
        {
            _findGameButton.onClick.RemoveListener(FindGame);
        }

        private void FindGame()
        {
            _findGameButton.interactable = false;
            PlayerPrefs.SetString(Constants.Name, _playerNameInputField.text);
            _networkRunnerSpawner.JoinLobby();
        }
    }
}
