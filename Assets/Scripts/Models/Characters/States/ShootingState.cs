using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;
using UnityEngine;

namespace Scorewarrior.Test.Models.Characters
{
    public class ShootingState : CharacterState
    {
        public ShootingState(CharacterModel characterModel) : base(characterModel)
        {
        }

        public override void Enter()
        {
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.AIMING, true);
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.RELOADING, false);
        }

        public override void Update(float deltaTime)
        {
            if (CharacterModel.CurrentTarget != null && CharacterModel.CurrentTarget.IsAlive)
            {
                if (CharacterModel.HasAmmo())
                {
                    if (CharacterModel.IsReady())
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        bool hit = random <= CharacterModel.CurrentStats[CharacterStats.ACCURACY] &&
                                   random <= CharacterModel.WeaponModel.CurrentStats[WeaponStats.ACCURACY] &&
                                   random >= CharacterModel.CurrentTarget.CurrentStats[CharacterStats.DEXTERITY];
                        CharacterModel.FireAt(CharacterModel.CurrentTarget, hit);
                    }
                    else
                    {
                        CharacterModel.WeaponModel.Update(deltaTime);
                    }
                }
                else
                {
                    CharacterModel.SetState(new ReloadingState(CharacterModel));
                }
            }
            else
            {
                CharacterModel.SetState(new IdleState(CharacterModel));
            }
        }

        public override void Exit()
        {
        }
    }
}