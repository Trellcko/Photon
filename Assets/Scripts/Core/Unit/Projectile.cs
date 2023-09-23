using Fusion;
using Unity.VisualScripting;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Projectile : NetworkBehaviour, IPaused
	{
		[SerializeField] private NetworkTransform _transform;
		[SerializeField] private LayerMask _opponentLayerMask;

		private float _speed;
		private float _damage;

		private bool _isWork;

		private Vector3 _direction;

        public override void Spawned()
        {
			_isWork = !PauseHandler.Instance.IsPaused;
        }

        private void OnEnable()
        {
            PauseHandler.Instance.Register(this);
        }

        private void OnDisable()
        {
            PauseHandler.Instance.UnRegister(this);
        }

        public void Init(float speed, float damage, Vector3 direction)
		{
			_speed = speed;
			_damage = damage;
			_direction = direction;
			
		}

        private void OnTriggerEnter(Collider other)
        {
			Debug.Log(other.name + " layer: " + other.gameObject.layer + " != " + _opponentLayerMask);
			if(other.gameObject.layer == (int)Mathf.Log(_opponentLayerMask, 2))
			{
				if(other.TryGetComponent(out Health health))
				{
					health.TakeDamageRpc(_damage);

                    Runner.Despawn(Object);
				}
			}
        }


		public override void FixedUpdateNetwork()
		{
			if (_isWork)
			{
				_transform.Transform.position += _speed * _direction * Runner.DeltaTime;
			}
		}

        public void Pause()
        {
			_isWork = false;
		}

        public void UnPause()
        {
			_isWork = true;
		}
    }
}
