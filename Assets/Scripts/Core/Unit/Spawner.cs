using Fusion;
using System;
using Trellcko.MonstersVsMonsters.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Spawner : NetworkBehaviour, IPointerClickHandler
	{
		[SerializeField] private MonsterData _monsterData;
		[SerializeField] private Transform _target;
		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private Vector3 _startRotation;
		[SerializeField] private LayerMask _layerMask;

		[SerializeField] private Side _side;

        [Networked] private PlayerRef _ref { get; set; }


        public event Action<Spawner> Clicked;

		
		public void Init(PlayerRef playerRef)
		{
			_ref = playerRef;
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			if(Runner.LocalPlayer == _ref)
			{
				Clicked?.Invoke(this);
			}
        }

        public void Spawn()
		{
			var spawned = Runner.Spawn(_monsterData.Prefab, _spawnPoint.position);
			spawned.SetTransform(_spawnPoint.position, _startRotation);
			spawned.gameObject.layer = (int)Mathf.Log(_layerMask, 2);
            spawned.Init(_monsterData, _target, _side);
		}
	}
}
