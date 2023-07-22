using System.Drawing;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class OpponentChecker : MonoBehaviour
	{
		[SerializeField] private float _radius;
        [SerializeField] private float _delay;

        public Health LastTarget { get; private set; }

        public Side MySide { private get; set; }

        private float _currentTime = 0f;

        private GameObject _lastTargetGO;

        private Collider[] _hits = new Collider[10];

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _delay)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius,  _hits);

                Debug.Log("count: " + count);
                for (int i = 0; i < count; i++)
                {
                    if (_lastTargetGO == _hits[i].transform.gameObject)
                    {
                        break;
                    }
                    if (_hits[i].transform.TryGetComponent(out Health health))
                    {
                        if (health.Side != MySide)
                        {
                            LastTarget = health;
                            break;
                        }
                    }
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
