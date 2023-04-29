using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models.Characters
{
    public class AimingState : CharacterState
    {
        private float _aimTime;

        public AimingState(CharacterModel characterModel) : base(characterModel)
        {
        }

        public override void Enter()
        {
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.AIMING, true);
            CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.RELOADING, false);

            _aimTime = CharacterModel.Descriptor.AimTime;
            CharacterModel.AimAt(CharacterModel.CurrentTarget);
        }

        public override void Update(float deltaTime)
        {
            if (CharacterModel.CurrentTarget != null && CharacterModel.CurrentTarget.IsAlive)
            {
                if (_aimTime > 0)
                {
                    _aimTime -= deltaTime;
                }
                else
                {
                    CharacterModel.SetState(new ShootingState(CharacterModel));
                    _aimTime = 0;
                }
            }
            else
            {
                CharacterModel.SetState(new IdleState(CharacterModel));
                _aimTime = 0;
            }
        }

        public override void Exit()
        {
        }
    }
}