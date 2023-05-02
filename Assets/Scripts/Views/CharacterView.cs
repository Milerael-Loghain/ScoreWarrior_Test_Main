using System;
using System.Collections.Generic;
using Scorewarrior.Test.Views.UI;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
	public class CharacterView : MonoBehaviour
	{
		[Serializable]
		private struct AnimationStringToEnumValue
		{
			public CharacterAnimationVariables VariableEnumKey;
			public string VariableStringKey;
		}

		[Serializable]
		private struct TeamToColor
		{
			public uint TeamId;
			public Color TeamColor;
		}

		public TextBarView HealthBar => _healthBar;

		public TextBarView ArmorBar => _armorBar;

		public WeaponView Weapon;

		[SerializeField]
		private Transform _rightPalm;

		[Header("Animator")]
		[SerializeField]
		private Animator Animator;

		[SerializeField]
		private List<AnimationStringToEnumValue> _animationKeys;

		[Header("Health Bars")]
		[SerializeField]
		private List<TeamToColor> _teamToColors;

		[SerializeField]
		private TextBarView _healthBar;

		[SerializeField]
		private TextBarView _armorBar;

		[SerializeField]
		private Canvas _hudCanvas;

		private Dictionary<CharacterAnimationVariables, int> _animationVariablesHash;
		private uint _team;

		private void Awake()
		{
			_animationVariablesHash = new Dictionary<CharacterAnimationVariables, int>();

			foreach (var animationKey in _animationKeys)
			{
				_animationVariablesHash.Add(animationKey.VariableEnumKey, Animator.StringToHash(animationKey.VariableStringKey));
			}
		}

		public void Update()
		{
			if (_rightPalm != null && Weapon != null)
			{
				Weapon.transform.position = _rightPalm.position;
				Weapon.transform.forward = _rightPalm.up;
			}

			_hudCanvas.transform.rotation = Camera.main.transform.rotation;
		}

		public void SetAnimatorBool(CharacterAnimationVariables variable, bool value)
		{
			Animator.SetBool(_animationVariablesHash[variable], value);
		}

		public void SetAnimatorFloat(CharacterAnimationVariables variable, float value)
		{
			Animator.SetFloat(_animationVariablesHash[variable], value);
		}

		public void SetAnimatorTrigger(CharacterAnimationVariables variable)
		{
			Animator.SetTrigger(_animationVariablesHash[variable]);
		}

		public void SetHudActiveState(bool isActive)
		{
			_hudCanvas.gameObject.SetActive(isActive);
		}

		public void SetTeam(uint team)
		{
			_team = team;

			foreach (var teamToColor in _teamToColors)
			{
				if (teamToColor.TeamId != team) continue;

				_healthBar.SetColor(teamToColor.TeamColor);
				break;
			}
		}
	}
}