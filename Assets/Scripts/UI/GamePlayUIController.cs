using Fusion;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class GamePlayUIController : MonoBehaviour
	{
		[SerializeField] private Button _leftButton;
		[SerializeField] private Button _rightButton;

      
		public void EnableLeftPlayerUI(MonsterBase monsterBase)
		{
			_leftButton.gameObject.SetActive(true);
			_leftButton.onClick.AddListener(monsterBase.SpawnMonster);
		}

        public void EnableRightPlayerUI(MonsterBase monsterBase) 
		{ 
			_rightButton.gameObject.SetActive(true);

            _rightButton.onClick.AddListener(monsterBase.SpawnMonster);
        }
	}
}
