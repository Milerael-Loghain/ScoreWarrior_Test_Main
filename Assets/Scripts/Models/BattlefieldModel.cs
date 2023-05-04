using System.Collections.Generic;
using System.Linq;
using Scorewarrior.Test.Views;
using Scorewarrior.Test.Models.Characters;
using UnityEngine;

namespace Scorewarrior.Test.Models
{
	public class BattlefieldModel
	{
		private readonly Dictionary<uint, List<Vector3>> _spawnPositionsByTeam;
		private readonly Dictionary<uint, List<CharacterModel>> _charactersByTeam;

		private bool _paused;

		public BattlefieldModel(Dictionary<uint, List<Vector3>> spawnPositionsByTeam)
		{
			_spawnPositionsByTeam = spawnPositionsByTeam;
			_charactersByTeam = new Dictionary<uint, List<CharacterModel>>();
		}

		public void Dispose()
		{
			foreach (var characterModels in _charactersByTeam.Values)
			{
				foreach (var characterModel in characterModels)
				{
					characterModel?.Dispose();
				}
			}
		}

		public void Start(CharacterView[] prefabs)
		{
			_paused = false;
			_charactersByTeam.Clear();

			List<CharacterView> availablePrefabs = new List<CharacterView>(prefabs);
			foreach (var positionsPair in _spawnPositionsByTeam)
			{
				List<Vector3> positions = positionsPair.Value;
				uint team = positionsPair.Key;
				List<CharacterModel> characters = new List<CharacterModel>();
				_charactersByTeam.Add(team, characters);
				int i = 0;
				while (i < positions.Count && availablePrefabs.Count > 0)
				{
					int index = Random.Range(0, availablePrefabs.Count);
					characters.Add(CreateCharacterAt(team, availablePrefabs[index], this, positions[i]));
					availablePrefabs.RemoveAt(index);
					i++;
				}
			}
		}

		public void Update(float deltaTime)
		{
			if (!_paused)
			{
				foreach (var charactersPair in _charactersByTeam)
				{
					List<CharacterModel> characters = charactersPair.Value;
					foreach (CharacterModel character in characters)
					{
						character.Update(deltaTime);
					}
				}
			}
		}

		public bool IsGameComplete()
		{
			foreach (var team in _charactersByTeam.Keys)
			{
				if (_charactersByTeam[team].All(character => !character.IsAlive)) return true;
			}

			return false;
		}

		public bool TryGetNearestAliveEnemy(CharacterModel characterModel, out CharacterModel target)
		{
			if (TryGetTeam(characterModel, out uint team))
			{
				CharacterModel nearestEnemy = null;
				float nearestDistance = float.MaxValue;
				List<CharacterModel> enemies = team == 1 ? _charactersByTeam[2] : _charactersByTeam[1];
				foreach (CharacterModel enemy in enemies)
				{
					if (enemy.IsAlive)
					{
						float distance = Vector3.Distance(characterModel.Position, enemy.Position);
						if (distance < nearestDistance)
						{
							nearestDistance = distance;
							nearestEnemy = enemy;
						}
					}
				}
				target = nearestEnemy;
				return target != null;
			}
			target = default;
			return false;
		}

		public bool TryGetTeam(CharacterModel target, out uint team)
		{
			foreach (var charactersPair in _charactersByTeam)
			{
				List<CharacterModel> characters = charactersPair.Value;
				foreach (CharacterModel character in characters)
				{
					if (character == target)
					{
						team = charactersPair.Key;
						return true;
					}
				}
			}
			team = default;
			return false;
		}

		private static CharacterModel CreateCharacterAt(uint team, CharacterView view, BattlefieldModel battlefieldModel, Vector3 position)
		{
			CharacterView character = Object.Instantiate(view);
			character.transform.position = position;
			return new CharacterModel(team, character, new WeaponModel(character.Weapon), battlefieldModel);
		}
	}
}