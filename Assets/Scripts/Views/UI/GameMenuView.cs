using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views.UI
{
    public class GameMenuView : MonoBehaviour
    {
        public event Action OnStartButtonDown;
        public event Action OnRestartButtonDown;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Button _restartButton;

        private void Awake()
        {
            ShowStartMenu();

            _startButton.onClick.AddListener(OnStartButton);
            _restartButton.onClick.AddListener(OnRestartButton);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartButton);
            _restartButton.onClick.RemoveListener(OnRestartButton);
        }

        private void OnStartButton()
        {
            OnStartButtonDown?.Invoke();
        }

        private void OnRestartButton()
        {
            OnRestartButtonDown?.Invoke();;
        }

        public void ShowStartMenu()
        {
            SetUIState(true);

            _startButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
        }

        public void ShowRestartMenu()
        {
            SetUIState(true);

            _startButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(true);
        }

        private void SetUIState(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void DisableUI()
        {
            SetUIState(false);
        }
    }
}