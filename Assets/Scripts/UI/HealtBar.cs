using Fusion;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class HealtBar : MonoBehaviour
	{
		[SerializeField] private RectTransform _fill;
		
		[SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Died += OnDied;
            _health.Changed += UpdateValue;
            if (_health.IsSpawned)
            {
                UpdateValue(_health.Value);
            }
        }

        private void OnDied()
        {
            UpdateValue(0);
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
            _health.Changed -= UpdateValue;
        }


        private void UpdateValue(float health)
        {
            print(health);

			float x = Mathf.Clamp01(health / _health.MaxHealth);

			_fill.transform.localScale = new Vector2(x, _fill.transform.localScale.y); 
		}
    }
}
