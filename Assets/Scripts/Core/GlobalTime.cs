using Fusion;
using UnityEngine;

namespace Trellcko
{
	public class GlobalTime : NetworkBehaviour
	{
		public static GlobalTime Instance 
		{ 
			get 
			{ 
				if(_instance == null)
				{
					_instance = new GameObject("GlobalTime").AddComponent<GlobalTime>();
					DontDestroyOnLoad(_instance.gameObject);
				}
				return _instance;
			} 
		}

		private static GlobalTime _instance;


        private void Awake()
        {
            if(_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			}
        }


    }
}
