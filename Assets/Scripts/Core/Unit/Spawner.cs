using Fusion;
using System;
using Trellcko.MonstersVsMonsters.Core.Resource;
using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Spawner : NetworkBehaviour, IPointerClickHandler
	{
		[SerializeField] private MonsterData _monsterData;
		[SerializeField] private Vector3 _startRotation;
		[SerializeField] private LayerMask _layerMask;

		[SerializeField] private Side _side;

		[SerializeField] private Miner _miner;

        private PlayerRef _ref { get; set; }


        public event Action<Spawner> Clicked;

		private Transform _spawnPoint;
		private Transform _targetPoint;
		
		public void Init(PlayerRef playerRef, Transform spawnPoint, Transform targetPoint)
		{
			_spawnPoint = spawnPoint;
			_targetPoint = targetPoint;
			_ref = playerRef;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if(Runner.LocalPlayer == _ref)
			{
				Clicked?.Invoke(this);
			}
        }

		public bool CheckCanSpawn()
		{
			return !(_monsterData.Gold > _miner.Value);

        }

		 public bool TrySpawn()
		{
			if (!CheckCanSpawn())
			{
				return false;
			}

			_miner.DecreaseValue(_monsterData.Gold);

			var spawned = Runner.Spawn(_monsterData.Prefab, _spawnPoint.position);
			spawned.SetTransform(_spawnPoint.position, _startRotation);
			spawned.gameObject.layer = (int)Mathf.Log(_layerMask, 2);
            spawned.Init(_monsterData, _targetPoint, _side);
			return true;
		}
	}
}
