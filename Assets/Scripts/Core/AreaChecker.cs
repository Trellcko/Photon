using Unity.VisualScripting;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
	public class AreaChecker : MonoBehaviour
	{
		[SerializeField] private float _radius;
        [SerializeField] private float delay;

        public GameObject LastTarget { get; private set; }

        public LayerMask OpponentLayerMask { private get; set; }

        private float _currentTime = 0f;

        private RaycastHit[] _hits = new RaycastHit[1];

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > delay)
            {
                if(Physics.SphereCastNonAlloc(transform.position, _radius, Vector3.zero, _hits, 100f, OpponentLayerMask) > 0)
                {
                    LastTarget = _hits[0].collider.gameObject;
                }
                
                _currentTime = 0f;
                return;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
