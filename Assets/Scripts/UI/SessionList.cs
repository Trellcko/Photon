using Fusion;
using System.Collections.Generic;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class SessionList : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _statusText;
		[SerializeField] private TMP_Dropdown _regionDropDown;
		
		[SerializeField] private SessionListItem _sessionListItemPrefab;
		
		[SerializeField] private VerticalLayoutGroup _sesionListItemParent;
		[SerializeField] private NetworkRunnerSpawner _network;

		private List<SessionListItem> _sessionListItems = new List<SessionListItem>();

		private List<SessionInfo> _sessions = new List<SessionInfo>();

		private int _index = 0;

        private void Awake()
        {
			OnLookingSession();
        }

        private void OnEnable()
        {
            NetworkRunnerSpawner.SessionsUpdated += OnSessionsUpdated;
			_regionDropDown.onValueChanged.AddListener(OnRegionChanged);
        }

        private void OnDisable()
        {
            NetworkRunnerSpawner.SessionsUpdated -= OnSessionsUpdated;
            _regionDropDown.onValueChanged.RemoveListener(OnRegionChanged);
        }

        private void OnRegionChanged(int index)
        {
			if(_sessions.Count > 0)
			{
				OnSessionsUpdated(new(_sessions));
			}
        }

        private void OnSessionsUpdated(List<SessionInfo> obj)
        {
			bool hasCorrectSession = false;

            ClearList();
			if(obj.Count <= 0)
			{
				OnNoSesionFound();
				return;
			}
			foreach (SessionInfo info in obj)
			{
				_sessions.Add(info);
				if (info.Region == _regionDropDown.captionText.text)
				{
					hasCorrectSession = true;
					AddSesionItem(info);
				}
			}
			if(!hasCorrectSession)
			{
                OnNoSesionFound();
            }
        }

        public void ClearList()
		{
			foreach(SessionListItem child in _sessionListItems)
			{
				child.gameObject.SetActive(false);
			}
            _sessions.Clear();
            _index = 0;
			_statusText.enabled = false;
		}

		public void AddSesionItem(SessionInfo session)
		{
			SessionListItem spawned;

			if (_index > _sessionListItems.Count - 1)
			{
				spawned = Instantiate(_sessionListItemPrefab, _sesionListItemParent.transform);
				_sessionListItems.Add(spawned);
			}
			else
			{
				spawned = _sessionListItems[_index];
				spawned.gameObject.SetActive(true);
				_index++;
			}
			
			spawned.SetInformation(session);


            spawned.Joined += OnJoined;
		}

        private void OnJoined(SessionInfo obj)
        {
			_network.JoinGame(GameMode.Shared, obj.Name, _regionDropDown.captionText.text);
        }

		private void OnNoSesionFound()
		{
			_statusText.SetText("No session Found");
			_statusText.enabled = true;
		}

		private void OnLookingSession()
		{
			_statusText.SetText("Looking for session");
            _statusText.enabled = true;
		}
    }
}
