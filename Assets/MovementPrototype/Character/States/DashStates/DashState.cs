using Assets.MovementPrototype.Character.States;
using UnityEngine;

public class DashState : BaseState
{
    public CController Character { get; protected set; }
    public Transform Transform { get; protected set; }
    public DashFsm DashFsm { get; protected set; }
    protected float elapsed;
    protected float totalTime;
    protected string nextState;
    public DashState(DashFsm fsm)
    {
        Fsm = fsm;
        DashFsm = fsm;
        Character = fsm.Character;
        Transform = Character.transform;
    }
    public override void PreUpdate()
    {
        if (elapsed >= totalTime)
        {
            Fsm.ChangeState(nextState, totalTime - elapsed);
        }
    }
    public override void Update()
    {
        elapsed += Time.fixedDeltaTime;
    }
    public override void Enter(StateTransitionArgs args)
    {
        elapsed = args.AdditionalDeltaTime;
    }
    public override void Exit(StateTransitionArgs args)
    {
    }

    public virtual bool IsOver()
    {
        return false;
    }
}
