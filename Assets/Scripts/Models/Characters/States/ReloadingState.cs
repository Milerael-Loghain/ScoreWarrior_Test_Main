namespace Scorewarrior.Test.Models.Characters
{
    public class ReloadingState : CharacterState
    {
        private readonly float _reloadTime;
        private float _timeLeft;

        public ReloadingState(CharacterModel characterModel) : base(characterModel)
        {
            _reloadTime = characterModel.WeaponModel.Descriptor.ReloadTime;
        }

        public override void Enter()
        {
            CharacterModel.View.Animator.SetBool("aiming", false);
            CharacterModel.View.Animator.SetBool("reloading", true);
            CharacterModel.View.Animator.SetFloat("reload_time", _reloadTime / 3.3f);
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
            CharacterModel.View.Animator.SetBool("aiming", false);
            CharacterModel.View.Animator.SetBool("reloading", false);
        }
    }
}