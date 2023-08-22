using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Base : MonoBehaviour
	{
		[SerializeField] private List<Spawner> _spawners;
		[SerializeField] private GameResultHandler _resultHandler;

		[SerializeField] private Health _baseHealth;

		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Transform _targetPoint;

		private PlayerRef _playerRef;

        private void OnEnable()
        {
            _baseHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _baseHealth.Died -= OnDied;
        }

        private void OnDied()
        {
			_resultHandler.SetLooseToRpc(_playerRef);
        }

        public void Init(PlayerRef playerRef)
		{
			_playerRef = playerRef;
			foreach(var spawner in _spawners) 
			{
				spawner.Init(playerRef, _spawnPoint, _targetPoint);
			}
		}
	}
}
