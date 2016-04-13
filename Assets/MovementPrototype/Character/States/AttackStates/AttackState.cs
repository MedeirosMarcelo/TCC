using Assets.MovementPrototype.Character.States;
using UnityEngine;

public class AttackState : CState
{
    public AttackFsm AttackFsm { get; protected set; }
    public AttackState(AttackFsm fsm) : base (fsm, fsm.Character)
    {
        Fsm = fsm;
        AttackFsm = fsm;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look(0.25f, 0.15f);
    }
}
