using Assets.MovementPrototype.Character.States;
using UnityEngine;

public class DashTransitionArgs : StateTransitionArgs
{
    public InputEvent.Dash Event { get; private set; }
    public DashTransitionArgs(string lastStateName, string nextStateName, float additionalDeltaTime, InputEvent.Dash evt)
        : base(lastStateName, nextStateName, additionalDeltaTime)
    {
        Event = evt;
    }
}

public class DashState : BaseState
{
    public CController Character { get; protected set; }
    public Transform Transform { get; protected set; }
    public DashFsm DashFsm { get; protected set; }
    public DashState(DashFsm fsm)
    {
        Fsm = fsm;
        DashFsm = fsm;
        Character = fsm.Character;
        Transform = Character.transform;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look();
    }
    public override void Enter(StateTransitionArgs args)
    {
        base.Enter(args);
        Character.ApplyDodgeMaterial();
    }
    public override void Exit(StateTransitionArgs args)
    {
        base.Exit(args);
        Character.ApplyBaseMaterial();
    }
}
