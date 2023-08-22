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

        private bool _isActive;

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

        private void Update()
        {
            if(_isActive)
            _button.interactable = _lastActiveSpawner.CheckCanSpawn();
        }

        private void OnButtonClicked()
        {
            _lastActiveSpawner.TrySpawn();
        }

        private void OnSpawnerClicked(Spawner spawner)
        {
            _isActive = true;
            _button.image.enabled = true;
            _lastActiveSpawner = spawner;
        }
    }
}
