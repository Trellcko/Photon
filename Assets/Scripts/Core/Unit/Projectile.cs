using Fusion;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class Projectile : NetworkBehaviour
	{
		[SerializeField] private NetworkTransform _transform;
		
		private float _speed;
		private float _damage;

		private Vector3 _direction;
		
		public void Init(float speed, float damage, Vector3 direction)
		{
			_speed = speed;
			_damage = damage;
			_direction = direction;
		}

        public override void FixedUpdateNetwork()
        {
			_transform.Transform.position += _speed * _direction * Runner.DeltaTime;   
        }
    }
}
