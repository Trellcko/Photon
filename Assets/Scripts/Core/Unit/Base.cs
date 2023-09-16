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
        [SerializeField] private PlayerInitializer _initializer;

		[SerializeField] private Health _baseHealth;

		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Transform _targetPoint;

        [Networked] public PlayerRef Owner { get; set; }

        private void OnEnable()
        {
            _initializer.PlayerInititalized += OnPlayerIntitalized;
            _baseHealth.Died += OnDiedRpc;
        }

        private void OnDisable()
        {
            _initializer.PlayerInititalized -= OnPlayerIntitalized;
            _baseHealth.Died -= OnDiedRpc;
        }


        public void Init(PlayerRef playerRef)
        {
            Object.RequestStateAuthority();
            Owner = playerRef;

            foreach (var spawner in _spawners)
            {
                spawner.Init(playerRef, _spawnPoint, _targetPoint);
            }
        }

        private void OnPlayerIntitalized(int obj)
        {
            if(obj < 2)
            {
                foreach(var spawner in _spawners)
                {
                    spawner.Disable();
                }
            }
            else
            {
                foreach (var spawner in _spawners)
                {
                    spawner.Enable();
                }
            }
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void OnDiedRpc()
        {
			_resultHandler.SetLooseToRpc(Owner);
        }

  	}
}
