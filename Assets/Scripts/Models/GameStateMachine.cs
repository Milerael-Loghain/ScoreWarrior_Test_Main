using System;
using System.Collections.Generic;
using Scorewarrior.Test.Data;
using Scorewarrior.Test.Views;
using Scorewarrior.Test.Views.UI;
using UnityEngine;

namespace Scorewarrior.Test.Models
{
    public class GameStateMachine
    {
        private readonly SpawnPoint[] _spawns;
        private readonly CharacterView[] _characters;
        private readonly ModifiersConfig _modifiersConfig;

        private GameStates _state;
        private BattlefieldModel _battlefieldModel;
        private GameMenuView _gameMenuView;

        public GameStateMachine(GameMenuView gameMenuView, CharacterView[] characters, SpawnPoint[] spawns, ModifiersConfig modifiersConfig)
        {
            _gameMenuView = gameMenuView;
            _characters = characters;
            _spawns = spawns;

            _modifiersConfig = modifiersConfig;

            _state = GameStates.START;
        }

        public void Dispose()
        {
            DisposeBattlefield();
        }

        public void SetState(GameStates gameStates)
        {
            _state = gameStates;
        }

        public void Update(float deltaTime)
        {
            switch (_state)
            {
                case GameStates.START:
                    DisposeBattlefield();
                    _gameMenuView.ShowStartMenu();
                    break;
                case GameStates.RUNTIME_INITIALIZATION:
                    _gameMenuView.DisableUI();
                    InitBattlefield();
                    break;
                case GameStates.RUNTIME:
                    UpdateBattlefield(deltaTime);
                    break;
                case GameStates.END:
                    _gameMenuView.ShowRestartMenu();
                    break;
            }
        }

        private void DisposeBattlefield()
        {
            _battlefieldModel?.Dispose();
        }

        private void InitBattlefield()
        {
            Dictionary<uint, List<Vector3>> spawnPositionsByTeam = new Dictionary<uint, List<Vector3>>();
            foreach (SpawnPoint spawn in _spawns)
            {
                uint team = spawn.Team;
                if (spawnPositionsByTeam.TryGetValue(team, out List<Vector3> spawnPoints))
                {
                    spawnPoints.Add(spawn.transform.position);
                }
                else
                {
                    spawnPositionsByTeam.Add(team, new List<Vector3>{ spawn.transform.position });
                }
            }

            _battlefieldModel = new BattlefieldModel(spawnPositionsByTeam, _modifiersConfig);
            _battlefieldModel.Start(_characters);

            _state = GameStates.RUNTIME;
        }

        private void UpdateBattlefield(float deltaTime)
        {
            if (_battlefieldModel.IsGameComplete())
            {
                _state = GameStates.END;
                return;
            }

            _battlefieldModel.Update(deltaTime);
        }
    }
}