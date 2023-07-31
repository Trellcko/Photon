using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class HealtBar : MonoBehaviour
	{
		[SerializeField] private Image _fill;
		[SerializeField] private RectTransform _rectTransform;

		private Health _health;

		private Transform _point;

        private void Update()
        {
			_rectTransform.position = new Vector3(_point.position.x, transform.parent.position.y, _point.position.z);
        }

        public void AttachTo(Health health, Transform point)
		{
			_health = health;
			_point = point;
		}
	}
}
