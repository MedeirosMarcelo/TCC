using UnityEngine;

public abstract class AnimatedState : CharState
{
    public string Animation { get; protected set; }
    public AnimatedState(CharFsm fsm) : base(fsm)
    {
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.animator.SetFloat("Speed", 1f / totalTime);
        Character.animator.Play(Animation);
    }
}