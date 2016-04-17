﻿using System;
using UnityEngine;

public class CFsm : BaseFsm
{
    public CController Character { get; protected set; }
    public CFsm(CController character) : base()
    {
        Character = character;
        StateLoader<CFsm> loader = new StateLoader<CFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.BlockStates");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates");
        Current = dict["MOVEMENT"];
    }

    public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        Current = dict["MOVEMENT"];
        base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
    }
}