using Fusion;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class SessionListItem : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _sessionName;
		[SerializeField] private TextMeshProUGUI _sessionCount;
		[SerializeField] private Button _joinButton;

		private SessionInfo _sessionInfo;

        public event Action<SessionInfo> Joined;

        private void OnEnable()
        {
			_joinButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveListener(OnClick);
        }

        public void SetInformation(SessionInfo sessionInfo)
		{
			_sessionInfo = sessionInfo;

			_sessionName.SetText(_sessionInfo.Name);
			_sessionCount.SetText($"{_sessionInfo.PlayerCount} / {_sessionInfo.MaxPlayers}");

			_joinButton.gameObject.SetActive(_sessionInfo.PlayerCount < _sessionInfo.MaxPlayers);
		}

		private void OnClick()
		{
			Joined?.Invoke(_sessionInfo);
		}
    }
}
