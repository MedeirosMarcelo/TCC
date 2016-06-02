namespace Assets.Scripts.Character
{
    public abstract class AnimatedState : CharacterState
    {
        public string Animation { get; protected set; }
        public AnimatedState(CharacterFsm fsm) : base(fsm)
        {
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play(Animation);
        }
    }
}