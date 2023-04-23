using Scorewarrior.Test.Models.Characters;

public class IdleState : CharacterState
{
    public IdleState(CharacterModel characterModel) : base(characterModel)
    {
    }

    public override void Enter()
    {
        CharacterModel.View.Animator.SetBool("aiming", false);
        CharacterModel.View.Animator.SetBool("reloading", false);

        if (CharacterModel.TryGetNearestAliveEnemy(out CharacterModel target))
        {
            CharacterModel.CurrentTarget = target;
            CharacterModel.SetState(new AimingState(CharacterModel));
        }
    }

    public override void Update(float deltaTime)
    {

    }

    public override void Exit()
    {
    }
}