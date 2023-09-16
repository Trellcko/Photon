using System.Collections;
using TMPro;
using Trellcko.MonstersVsMonsters.Core;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class WaitingTextDisplayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		[SerializeField] private PlayerInitializer _initalizer;

        private void OnEnable()
        {
            _initalizer.PlayerInititalized += OnPlayerIntitalized;
        }

        private void OnDisable()
        {
            _initalizer.PlayerInititalized -= OnPlayerIntitalized;
        }

        private void OnPlayerIntitalized(int count)
        {
            Debug.Log("Text: " + count);
            if (count < 2)
            {
                _text.SetText("Wait for another Player!");
            }
            else
            {
                _text.SetText("");
            }
        }
    }
}
