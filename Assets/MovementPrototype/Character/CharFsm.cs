using System;
using UnityEngine;

public class CharFsm : BaseFsm
{
    public CharController Character { get; protected set; }
    public CharFsm(CharController character) : base()
    {
        Character = character;
        StateLoader<CharFsm> loader = new StateLoader<CharFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
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