using System.Drawing;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class OpponentChecker : MonoBehaviour
	{
		[SerializeField] private float _radius;
        [SerializeField] private float _delay;

        public Vector3 LastTargetPosition => LastTarget.transform.position;

        public Health LastTarget { get; private set; }

        private Side _mySide;

        private float _currentTime = 0f;

        private GameObject _lastTargetGO;

        private Collider[] _hits = new Collider[10];

        private int _layer = 0;

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _delay)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius,  _hits, _layer);

                Debug.Log("count: " + count);
                for (int i = 0; i < count; i++)
                {
                    if (_lastTargetGO == _hits[i].transform.gameObject)
                    {
                        break;
                    }
                    if (_hits[i].transform.TryGetComponent(out Health health))
                    {
                        if (health.Side != _mySide)
                        {
                            LastTarget = health;
                            break;
                        }
                    }
                }
                _lastTargetGO = null;

                _currentTime = 0f;
                return;
            }
        }

        public void Init(Side side)
        {
            _mySide = side;
            _layer = ~(1 << gameObject.layer);
        }

        public void Init(Side side, float radius)
        {
            Init(side);
            _radius = radius;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
