using Assets.MovementPrototype.Character.States;
using UnityEngine;

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
