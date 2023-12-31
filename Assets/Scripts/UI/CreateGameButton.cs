using System;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class CreateGameButton : MonoBehaviour
	{
        [SerializeField] private TMP_Dropdown _regionDropDown;
		[SerializeField] private Button _button;

		[SerializeField] private NetworkRunnerSpawner _networkRunner;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            string name = PlayerPrefs.GetString(Constants.Name, "Annonymys");

            _networkRunner.JoinCustomGame(Fusion.GameMode.Shared, $"{name}'s game", _regionDropDown.captionText.text);
        }
    }
}
