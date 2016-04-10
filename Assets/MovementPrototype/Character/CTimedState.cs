using UnityEngine;

public abstract class CTimedState : CState
{
    protected float elapsed;
    protected float totalTime;
    protected string nextState;

    public CTimedState(CFsm fsm) : base (fsm)
    {
    }

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