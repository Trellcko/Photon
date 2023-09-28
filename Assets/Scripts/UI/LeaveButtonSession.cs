using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class LeaveButtonSession : NetworkBehaviour
	{
		[SerializeField] private Button _button;

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
            Runner.Shutdown(true, ShutdownReason.Ok);
            SceneManager.LoadScene(0);
        }
    }
}
