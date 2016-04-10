using System;
using UnityEngine;

public class CFsm : BaseFsm
{
    public CController Character { get; protected set; }
    public CFsm(CController character) : base()
    {
        Character = character;
        StateLoader<CFsm> loader = new StateLoader<CFsm>();
        loader.LoadStates(this, "Assets.MovementPrototype.Character.States");
        Current = dict["IDLE"];
    }

    public override void Enter(StateTransitionArgs args)
    {
        Current = dict["IDLE"];
        UnityEngine.Debug.Log("Enter");
        Current.Enter(args);
    }

    public override void Exit(StateTransitionArgs args)
    {
        UnityEngine.Debug.Log("Exit");
        Current.Exit(args);
    }
}