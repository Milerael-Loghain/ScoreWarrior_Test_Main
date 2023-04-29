using System;
using System.Collections.Generic;
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

		public WeaponView Weapon;

		[SerializeField]
		private Animator Animator;

		[SerializeField]
		private Transform _rightPalm;

		[SerializeField]
		private List<AnimationStringToEnumValue> _animationKeys;

		private Dictionary<CharacterAnimationVariables, int> _animationVariablesHash;

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
	}
}