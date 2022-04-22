using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RunAndCatch
{
    internal class UIMediator : MonoBehaviour
    {
        [SerializeField]
        private Image _imageDistanceBar;

        [SerializeField]
        private TMP_Text _scoresText;

        internal void ChangeDistanceBar(float value)
        {
            _imageDistanceBar.fillAmount = value;
        }

        internal void ChangeScoresText(int value)
        {
            _scoresText.text = value.ToString();
        }
    }
}
