using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models.Characters
{
    public class ReloadingState : CharacterState
    {
        private readonly float _reloadTime;
        private float _timeLeft;

        public ReloadingState(CharacterModel characterModel) : base(characterModel)
        {
            _reloadTime = characterModel.WeaponModel.Descriptor.Stats[WeaponStats.RELOADTIME];
        }

        public override void Enter()
        {
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.AIMING, true);
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.RELOADING, true);
            CharacterModel.View.SetAnimatorFloat(CharacterAnimationVariables.RELOAD_TIME, _reloadTime / 3.3f);
            _timeLeft = _reloadTime;
        }

        public override void Update(float deltaTime)
        {
            if (_timeLeft <= 0)
            {
                if (CharacterModel.CurrentTarget != null)
                {
                    CharacterModel.SetState(new ShootingState(CharacterModel));
                }
                else
                {
                    CharacterModel.SetState(new IdleState(CharacterModel));
                }

                CharacterModel.Reload();
            }
            else
            {
                _timeLeft -= deltaTime;
            }
        }

        public override void Exit()
        {
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.AIMING, false);
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.RELOADING, false);
        }
    }
}