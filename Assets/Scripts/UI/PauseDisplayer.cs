using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class PauseDisplayer : MonoBehaviour, IPaused
	{
		[SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            if (PauseHandler.IsInitialized)
            {
                PauseHandler.Instance.Register(this);
            }
            else
            {
                PauseHandler.Initialized += OnInitialized;
            }
        }

        private void OnInitialized()
        {
            PauseHandler.Initialized -= OnInitialized;
            PauseHandler.Instance.Register(this);
        }

        private void OnDisable()
        {
            PauseHandler.Instance.UnRegister(this);
        }

        public void Pause()
        {
            _text.SetText("Pause!");
        }

        public void UnPause()
        {
            _text.SetText("");
        }


    }
}
