using TMPro;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.UI
{
	public class RatingDisplayer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _ratingText;

        private void Start()
        {
            _ratingText.SetText("Rating: " + PlayerPrefs.GetInt(Constants.Rating, 1500));
        }
    }
}
