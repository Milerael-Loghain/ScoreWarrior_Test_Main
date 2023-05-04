using Scorewarrior.Test.Models;
using Scorewarrior.Test.Views.UI;

namespace Scorewarrior.Test.Controllers
{
    public class UIController
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameMenuView _gameMenuView;

        public UIController(GameStateMachine gameStateMachine, GameMenuView gameMenuView)
        {
            _gameStateMachine = gameStateMachine;
            _gameMenuView = gameMenuView;

            _gameMenuView.OnStartButtonDown += OnStartButtonDown;
            _gameMenuView.OnRestartButtonDown += OnRestartButtonDown;
        }

        public void Dispose()
        {
            _gameMenuView.OnStartButtonDown -= OnStartButtonDown;
            _gameMenuView.OnRestartButtonDown -= OnRestartButtonDown;
        }

        private void OnStartButtonDown()
        {
            _gameStateMachine.SetState(GameStates.RUNTIME_INITIALIZATION);
        }

        private void OnRestartButtonDown()
        {
            _gameStateMachine.SetState(GameStates.START);
        }
    }
}