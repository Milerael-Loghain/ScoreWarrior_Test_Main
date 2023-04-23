namespace Scorewarrior.Test.Models.Characters
{
    public abstract class CharacterState
    {
        protected CharacterModel CharacterModel;

        public CharacterState(CharacterModel characterModel)
        {
            CharacterModel = characterModel;
        }

        public abstract void Enter();
        public abstract void Update(float deltaTime);
        public abstract void Exit();
    }
}