using System.Collections.Generic;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class SpawnButton : MonoBehaviour
	{
		[SerializeField] private  List<Spawner> _spawners;
        [SerializeField] private Button _button;

        private Spawner _lastActiveSpawner;

        private void OnEnable()
        {

            _button.onClick.AddListener(OnButtonClicked);
            foreach(var spawner in _spawners) 
            {
                spawner.Clicked += OnSpawnerClicked;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            foreach (var spawner in _spawners)
            {
                spawner.Clicked -= OnSpawnerClicked;
            }
        }

        private void OnButtonClicked()
        {
            _lastActiveSpawner.Spawn();
        }

        private void OnSpawnerClicked(Spawner spawner)
        {
            _lastActiveSpawner = spawner;
            _button.image.enabled = true;
        }
    }
}
