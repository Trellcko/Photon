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

		private bool _isEnable;

		public void Init(PlayerRef playerRef, Transform spawnPoint, Transform targetPoint)
		{
			_spawnPoint = spawnPoint;
			_targetPoint = targetPoint;
			_ref = playerRef;
		}

		public void Enable()
		{
			_isEnable = true;
		}

		public void Disable()
		{
			_isEnable = false;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if(Runner.LocalPlayer == _ref && _isEnable)
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

			_monsterData.Create(_spawnPoint.position, _startRotation, _layerMask, _side, _targetPoint, Runner);
			return true;
		}
	}
}
