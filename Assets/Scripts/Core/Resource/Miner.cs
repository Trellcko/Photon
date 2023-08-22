using System;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Resource
{
	public class Miner : MonoBehaviour
	{
        [field: SerializeField] public Resource TypeResource { get; private set; }
		[SerializeField] private float _miningTime;
		[SerializeField] private float _count;

        public event Action Updated;

        public float Value { get; private set; }

        private float _currentTime;

        private void Update()
        {
            if(_currentTime < _miningTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                Value += _count;
                Updated?.Invoke();
                _currentTime = 0f;
            }
        }

        public void IncreaseValue(float value)
        {
            Value += Mathf.Clamp(value, 0, float.MaxValue);
            Updated?.Invoke();
        }

        public void DecreaseValue(float value)
        {
            Value -= Mathf.Clamp(value, 0, float.MaxValue);
            Updated?.Invoke();
        }
    }

    public enum Resource
    {
        Gold
    }
}
