using Scorewarrior.Test.Models.Characters;
using Scorewarrior.Test.Views;

public class IdleState : CharacterState
{
    public IdleState(CharacterModel characterModel) : base(characterModel)
    {
    }

    public override void Enter()
    {
    }

    public override void Update(float deltaTime)
    {
        CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.AIMING, false);
        CharacterModel.View.SetAnimatorBool(CharacterAnimationVariables.RELOADING, false);

        if (CharacterModel.TryGetNearestAliveEnemy(out CharacterModel target))
        {
            CharacterModel.CurrentTarget = target;
            CharacterModel.SetState(new AimingState(CharacterModel));
        }
    }

    public override void Exit()
    {
    }
}