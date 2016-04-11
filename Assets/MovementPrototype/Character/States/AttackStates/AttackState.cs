﻿using Assets.MovementPrototype.Character.States;
using UnityEngine;

public class AttackState : BaseState
{
    public CController Character { get; protected set; }
    public Transform Transform { get; protected set; }
    public AttackFsm AttackFsm { get; protected set; }
    public AttackState(AttackFsm fsm)
    {
        Fsm = fsm;
        AttackFsm = fsm;
        Character = fsm.Character;
        Transform = Character.transform;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Character.Look(0.25f, 0.15f);
    }
}