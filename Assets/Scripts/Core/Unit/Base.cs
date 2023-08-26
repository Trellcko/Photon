using Fusion;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Base : NetworkBehaviour
	{
		[SerializeField] private List<Spawner> _spawners;
		[SerializeField] private GameResultHandler _resultHandler;

		[SerializeField] private Health _baseHealth;

		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Transform _targetPoint;

        private PlayerRef _playerRef;


        private void OnEnable()
        {
            _baseHealth.Died += OnDiedRpc;
        }

        private void OnDisable()
        {
            _baseHealth.Died -= OnDiedRpc;
        }


        public void Init(PlayerRef playerRef)
        {
            Object.RequestStateAuthority();
            _playerRef = playerRef;

            print(this._playerRef + " !=" + playerRef);

            foreach (var spawner in _spawners)
            {
                spawner.Init(playerRef, _spawnPoint, _targetPoint);
            }
        }



        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void OnDiedRpc()
        {
			_resultHandler.SetLooseToRpc(_playerRef);
        }

  	}
}
