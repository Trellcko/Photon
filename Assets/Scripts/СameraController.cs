using UnityEngine;
using UnityEngine.InputSystem;

namespace Trellcko.MonstersVsMonsters.Core
{
	public class СameraController : MonoBehaviour
	{
		[SerializeField] private Transform _followPoint;
        [SerializeField] private Camera _camera;

        [Header("Config")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _edgeSize = 10f;

        public void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();

            Vector3 cameraDirection = Vector3.zero;

            cameraDirection.z = CalculateDirection(mousePosition.y, Screen.height);
            cameraDirection.x = CalculateDirection(mousePosition.x, Screen.width);

            _followPoint.position += cameraDirection;
        }



        public float CalculateDirection(float currentCoordiatne, float screenCurrentCoordinate)
        {
            if (currentCoordiatne < _edgeSize)
            {
                return -_speed;
            }
            else if (currentCoordiatne > screenCurrentCoordinate - _edgeSize)
            {
                return _speed;
            }
            return 0f;
        }
    }
}
