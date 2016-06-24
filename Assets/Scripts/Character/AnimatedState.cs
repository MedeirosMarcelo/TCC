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
            UnityEngine.Debug.Log("Playing " + Animation + " for " + totalTime.ToString("N2") + "s");
            Character.Animator.SetFloat("Speed", 1f / totalTime);
            Character.Animator.Play(Animation);
        }
    }
}