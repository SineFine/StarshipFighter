using System;
using Controllers.Contracts;
using States;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResetMenu : MonoBehaviour, IGameStateChanger, IResetMenu
    {
        [SerializeField] private Button _resetButton;
        
        public event Action<GameState> OnStateChange;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(ButtonClick);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(ButtonClick);
        }

        private void ButtonClick()
        {
            OnStateChange?.Invoke(GameState.Restart);
        }
    }
}