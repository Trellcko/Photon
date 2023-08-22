using Cinemachine;
using Trellcko.MonstersVsMonsters.Core.Input;
using UnityEngine;


namespace Trellcko.MonstersVsMonsters.Core.Camera
{
    public class СameraController : MonoBehaviour
    {
        [SerializeField] private Transform _followPoint;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private UnityEngine.Camera _camera;

        [Header("Config")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _zoomSpeed = 0.5f;
        [SerializeField] private float _edgeSize = 10f;


        private void Update()
        {
            Vector3 mousePosition = KeyBoardAndMouseInputSystem.Instance.Position;

            Vector3 cameraDirection = Vector3.zero;

            cameraDirection.z = CalculateDirection(mousePosition.y, Screen.height);
            cameraDirection.x = CalculateDirection(mousePosition.x, Screen.width);

            _followPoint.position += cameraDirection;

            _virtualCamera.m_Lens.OrthographicSize += CalculateZoom(KeyBoardAndMouseInputSystem.Instance.Scroll);
        }


        private float CalculateZoom(float scroll) => _zoomSpeed * scroll;

        private float CalculateDirection(float currentCoordiatne, float screenCurrentCoordinate)
        {
            print(currentCoordiatne);
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
