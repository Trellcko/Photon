using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Resource
{
	public class Miner : MonoBehaviour
	{
        [field: SerializeField] public Resource TypeResource { get; private set; }

        [SerializeField] private PlayerInitializer _playerInitializer;
        [SerializeField] private float _miningTime;
		[SerializeField] private float _count;

        public event Action Updated;

        public float Value { get; private set; }

        private float _currentTime;

        private void OnEnable()
        {
            _playerInitializer.PlayerInititalized += OnPlayerIntitalized;
        }

        private void OnDisable()
        {
            _playerInitializer.PlayerInititalized -= OnPlayerIntitalized;
        }

        private void OnPlayerIntitalized(int obj)
        {
            Debug.Log("Miner: " + obj);

            if(obj >= 2)
            {
                print("Start mining");
                StartCoroutine(MinerCorun());
            }
        }

        private IEnumerator MinerCorun()
        {
            while (true)
            {
                if (_currentTime < _miningTime)
                {
                    print("Current Timer: " + _currentTime);
                    _currentTime += Time.deltaTime;
                }
                else
                {
                    Value += _count;
                    Updated?.Invoke();
                    _currentTime = 0f;
                }
                yield return null;
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
