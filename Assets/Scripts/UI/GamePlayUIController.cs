using Fusion;
using Trellcko.MonstersVsMonsters.Core.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class GamePlayUIController : MonoBehaviour
	{
		[SerializeField] private Button _leftButton;
		[SerializeField] private Button _rightButton;

      
		public void EnableLeftPlayerUI(Base monsterBase)
		{
			_leftButton.gameObject.SetActive(true);
			_leftButton.onClick.AddListener(monsterBase.SpawnMonster);
		}

        public void EnableRightPlayerUI(Base monsterBase) 
		{ 
			_rightButton.gameObject.SetActive(true);

            _rightButton.onClick.AddListener(monsterBase.SpawnMonster);
        }
	}
}
