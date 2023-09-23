using System.Drawing;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Unit
{
    public class OpponentChecker : MonoBehaviour, IPaused
	{
		[SerializeField] private float _radius;
        [SerializeField] private float _delay;
        [SerializeField] private LayerMask checkMask;

        public Vector3 LastTargetPosition => LastTarget.transform.position;

        public Health LastTarget { get; private set; }

        private Side _mySide;

        private bool _isWork = true;

        private float _currentTime = 0f;

        private GameObject _lastTargetGO;

        private Collider[] _hits = new Collider[10];

        private void OnEnable()
        {
            PauseHandler.Instance.Register(this);
        }

        private void OnDisable()
        {
            PauseHandler.Instance.UnRegister(this);
        }

        private void Update()
        {
            if(!_isWork) return;
            _currentTime += Time.deltaTime;

            if (_currentTime > _delay)
            {
                int count = Physics.OverlapSphereNonAlloc(transform.position, _radius,  _hits, checkMask);

                for (int i = 0; i < count; i++)
                {

                    if (_lastTargetGO == _hits[i].transform.gameObject)
                    {

                        return;
                    }
                    if (_hits[i].transform.TryGetComponent(out Health health))
                    {
                        if (health.Runner && health.Side != _mySide)
                        {

                            LastTarget = health;
                            return;
                        }

                    }
                }
                LastTarget = null;
                _lastTargetGO = null;

                _currentTime = 0f;
                return;
            }
        }

        public void Init(Side side)
        {
            _mySide = side;
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
