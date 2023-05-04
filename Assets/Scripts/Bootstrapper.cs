using Scorewarrior.Test.Controllers;
using Scorewarrior.Test.Models;
using Scorewarrior.Test.Views;
using Scorewarrior.Test.Views.UI;
using UnityEngine;

namespace Scorewarrior.Test
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField]
		private CharacterView[] _characters;

		[SerializeField]
		private SpawnPoint[] _spawns;

		[SerializeField]
		private GameMenuView _gameMenuView;

		private UIController _uiController;
		private GameStateMachine _gameStateMachine;

		private void Start()
		{
			_gameStateMachine = new GameStateMachine(_gameMenuView, _characters, _spawns);
			_uiController = new UIController(_gameStateMachine, _gameMenuView);
		}

		private void OnDestroy()
		{
			_gameStateMachine?.Dispose();
			_uiController?.Dispose();
		}

		private void Update()
		{
			_gameStateMachine.Update(Time.deltaTime);
		}
	}
}