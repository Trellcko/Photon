using System;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class RatingGameButton : MonoBehaviour
	{
		[SerializeField] private Button _button;

        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private GameObject _sesionList;
        [SerializeField] private Button _customGameButton;

        [SerializeField] private MatchMaker _matchMaker;

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
            if (!_matchMaker.IsLookingMatch)
            {
                StartLookingMatch();
            }
            else
            {
                StopLookingMatch();
            }
        }

        private void StartLookingMatch()
        {
            _matchMaker.StartLookingGame();
            _sesionList.gameObject.SetActive(false);
            _customGameButton.interactable = false;
            _statusText.SetText("Looking for rating game!");
        }

        private void StopLookingMatch()
        {
            _matchMaker.StopLookingGame();
            _sesionList.gameObject.SetActive(true);
            _customGameButton.interactable = true;
            _statusText.SetText("");
        }
    }
}
