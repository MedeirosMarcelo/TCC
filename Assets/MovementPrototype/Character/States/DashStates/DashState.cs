using Assets.MovementPrototype.Character.States;
using UnityEngine;

public class DashState : CState
{
    public DashFsm DashFsm { get; protected set; }

    public DashState(DashFsm fsm) : base (fsm, fsm.Character)
    {
        Fsm = fsm;
        DashFsm = fsm;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look();
    }
    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.ApplyDodgeMaterial();
    }
    public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        Character.ApplyBaseMaterial();
    }
}
