using System;
using Services;
using Services.Contracts;
using States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour, IGameStateChanger
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _bestScore;

        public event Action<GameState> OnStateChange;

        public void SetBestScore(int score)
        {
            _bestScore.text = $"Best Score: {score}";
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            gameObject.SetActive(false);
            OnStateChange?.Invoke(GameState.Start);
        }
    }
}
