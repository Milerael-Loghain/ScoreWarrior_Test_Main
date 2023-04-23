using System.Collections.Generic;
using Scorewarrior.Test.Models;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField]
		private CharacterView[] _characters;
		[SerializeField]
		private SpawnPoint[] _spawns;

		private BattlefieldModel _battlefieldModel;

		public void Start()
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
				Destroy(spawn.gameObject);
			}
			_battlefieldModel = new BattlefieldModel(spawnPositionsByTeam);
			_battlefieldModel.Start(_characters);
		}

		public void Update()
		{
			_battlefieldModel.Update(Time.deltaTime);
		}


	}
}