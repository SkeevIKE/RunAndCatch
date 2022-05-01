using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace RunAndCatch
{
    internal class UIMediator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _finalScreen;

        [SerializeField]
        private Image _imageDistanceBar;

        [SerializeField]
        private TMP_Text _scoresText;

        [SerializeField]
        private Button _nextLevelButton;

        private const float _alphaThreshold = 0.1f;

        private void Start()
        {
            if (_finalScreen == null) Debug.LogWarning($" final screen in {this}, can't be empty"); 
            if (_imageDistanceBar == null) Debug.LogWarning($"image distance bar in {this}, can't be empty");
            if (_scoresText == null) Debug.LogWarning($" scores text in {this}, can't be empty");
            if (_nextLevelButton == null) Debug.LogWarning($" nextLevel button in {this}, can't be empty");           

            _nextLevelButton.GetComponent<Image>().alphaHitTestMinimumThreshold = _alphaThreshold;
        }

        internal void ChangeDistanceBar(float value)
        {
            _imageDistanceBar.fillAmount = value;
        }

        internal void ChangeScoresText(int value)
        {
            _scoresText.text = value.ToString();
        }

        internal void SubscribeNextLevelBuutonEvent(UnityAction unityAction)
        {            
            _nextLevelButton.onClick.AddListener(unityAction);
        }

        internal void UnsubscribeNextLevelBuutonEvent(UnityAction unityAction)
        {
            _nextLevelButton.onClick.RemoveListener(unityAction);
        }

        internal void ShowFinalScreen()
        {
            _finalScreen.SetActive(true);
        }
    }
}
