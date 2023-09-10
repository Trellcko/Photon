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
		
		[SerializeField] private SessionListItem _sessionListItemPrefab;
		
		[SerializeField] private VerticalLayoutGroup _sesionListItemParent;
		[SerializeField] private NetworkRunnerSpawner _network;

		private List<SessionListItem> _sessionListItems = new List<SessionListItem>();

		private int _index = 0;

        private void Awake()
        {
			OnLookingSession();
        }

        private void OnEnable()
        {
            _network.SessionsUpdated += OnSessionsUpdated;
        }

        private void OnDisable()
        {
            _network.SessionsUpdated -= OnSessionsUpdated;
        }

        private void OnSessionsUpdated(System.Collections.Generic.List<SessionInfo> obj)
        {
			ClearList();
			if(obj.Count <= 0)
			{
				OnNoSesionFound();
				return;
			}
			foreach(SessionInfo info in obj)
			{
				AddSesion(info);
			}
        }

        public void ClearList()
		{
			foreach(SessionListItem child in _sessionListItems)
			{
				child.gameObject.SetActive(false);
			}
			_index = 0;
			_statusText.enabled = false;
		}

		public void AddSesion(SessionInfo session)
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
				_index++;
			}
			
			spawned.SetInformation(session);


            spawned.Joined += OnJoined;
		}

        private void OnJoined(SessionInfo obj)
        {
			_network.JoinGame(GameMode.Shared, obj.Name);
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
