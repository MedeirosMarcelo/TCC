using UnityEngine;

public abstract class TimedState : BaseState
{
    protected float elapsed;
    protected float totalTime;
    protected string nextState;

    public override void PreUpdate()
    {
        if (elapsed >= totalTime)
        {
            Fsm.ChangeState(nextState, totalTime - elapsed);
        }
    }
    public override void FixedUpdate()
    {
        elapsed += Time.fixedDeltaTime;
    }
    public override void Enter(StateTransitionArgs args)
    {
        elapsed = args.AdditionalDeltaTime;
    }
}