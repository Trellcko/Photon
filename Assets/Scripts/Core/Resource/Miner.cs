using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.Resource
{
	public class Miner : MonoBehaviour, IPaused
	{
        [field: SerializeField] public Resource TypeResource { get; private set; }

        [SerializeField] private PlayerInitializer _playerInitializer;
        [SerializeField] private float _miningTime;
		[SerializeField] private float _count;

        public event Action Updated;


        public float Value { get; private set; }

        private bool _isWork = true;

        private float _currentTime;

        private void OnEnable()
        {
            _playerInitializer.PlayerInititalized += OnPlayerIntitalized;
            if (_playerInitializer.InitalizedPlayerRefs.Count > 1)
            {
                PauseHandler.Instance.Register(this);
            }
        }

        private void OnDisable()
        {
            _playerInitializer.PlayerInititalized -= OnPlayerIntitalized;
            PauseHandler.Instance.UnRegister(this);
        }

        private void OnPlayerIntitalized(int obj)
        {
            Debug.Log("Miner: " + obj);

            if(obj >= 2)
            {
                PauseHandler.Instance.Register(this);
                print("Start mining");
                StartCoroutine(MinerCorun());
            }
        }

        private IEnumerator MinerCorun()
        {
            while (true)
            {
                if (_isWork)
                {

                    if (_currentTime < _miningTime)
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

        public void Pause()
        {
            print("Miner Stop");
            _isWork = false;
        }

        public void UnPause()
        {
            print("Miner Return");
            _isWork = true;
        }
    }

    public enum Resource
    {
        Gold
    }
}
