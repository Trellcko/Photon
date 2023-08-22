using TMPro;
using Trellcko.MonstersVsMonsters.Core.Resource;
using UnityEngine;

namespace Trellcko
{
	public class ResourceDisplayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		[SerializeField] private Miner _miner;

        private void OnEnable()
        {
            _miner.Updated += OnUpdated;
        }

        private void OnUpdated()
        {
        _text.SetText($"{_miner.TypeResource}: {_miner.Value}");
        }
    }
}