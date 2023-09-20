using Fusion;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Projectile : NetworkBehaviour
	{
		[SerializeField] private NetworkTransform _transform;
		[SerializeField] private LayerMask _opponentLayerMask;

		private float _speed;
		private float _damage;

		private Vector3 _direction;
		
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
			_transform.Transform.position += _speed * _direction * Runner.DeltaTime * Time.timeScale;   
        }
    }
}
