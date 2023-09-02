using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class SessionList : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _statusText;
		
		[SerializeField] private SessionListItem _sessionListItemPrefab;
		
		[SerializeField] private VerticalLayoutGroup _sesionListItemParent;
	
		public void ClearList()
		{
			foreach(Image child in _sesionListItemParent.transform)
			{
				Destroy(child);
			}
			_statusText.enabled = false;
		}

		public void AddSesion(SessionInfo session)
		{
			SessionListItem spawned = Instantiate(_sessionListItemPrefab, _sesionListItemParent.transform);
			spawned.SetInformation(session);

            spawned.Joined += OnJoined;
		}

        private void OnJoined(SessionInfo obj)
        {

        }

		private void OnNoSesionFound()
		{
			_statusText.SetText("No session Found");
			_statusText.gameObject.SetActive(true);
		}

		private void OnLookingSession()
		{
			_statusText.SetText("Looking for session");
			_statusText.gameObject.SetActive(true);
		}
    }
}
