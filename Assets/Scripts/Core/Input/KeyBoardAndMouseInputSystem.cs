using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trellcko.MonstersVsMonsters.Core.Input
{
	public class KeyBoardAndMouseInputSystem : MonoBehaviour, IInputSystem
	{
        public static KeyBoardAndMouseInputSystem Instance
		{
			get 
			{ 
				if(s_instance == null)
				{
					s_instance = new GameObject("InputSystem").AddComponent<KeyBoardAndMouseInputSystem>();
					DontDestroyOnLoad(s_instance);
				}
				return s_instance;
			}
		}

		public Vector2 Position => Mouse.current.position.ReadValue();

		public float Scroll => Mouse.current.scroll.ReadValue().normalized.y;

        private static KeyBoardAndMouseInputSystem s_instance;

        private void Awake()
        {
			if (FindObjectsOfType<KeyBoardAndMouseInputSystem>().Length > 1)
			{
				Destroy(gameObject);
			}
		}

    }
}
