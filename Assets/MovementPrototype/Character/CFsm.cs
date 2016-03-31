using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CFsm : CState {
    public Dictionary<string, CState> dict;

    public CState last    { get; private set; }
    public CState current { get; private set; }
    public CState next    { get; private set; }

    private void AddState(CState state)
    {
        dict.Add(state.Name, state);
    }

    private void HookEvents()
    {
        foreach (KeyValuePair<string, CState> entry in dict)
        {
            entry.Value.StateTransitionRequested += StateTransitionRequestedListener;
        }
    }

    public CFsm(CController character) : base(character) {
        Name = "FSM";
        Character = character;
        dict = new Dictionary<string, CState>();
        AddState(new CIdle(character));
        AddState(new CWalk(character));
        AddState(new CDash(character));
        current = dict.First().Value;
        HookEvents();
    }

    private void StateTransitionRequestedListener(StateTransitionEventArgs obj)
    {
        Debug.Log("ChangeState: from " + current + " to " + obj.RequestedStateName);

        // Notice there is no check if is a different state, this is by design
        next = dict[obj.RequestedStateName];
        last = current;
        last.Exit();

        current = next;
        next = null;
        current.Enter(obj);

        if (obj.RunNextState)
        {
            current.Update();
        }
    }

    public override void PreUpdate()
    {
    }

    public override void Enter(StateTransitionEventArgs args)
    {
    }

    public override void Update()
    {
        current.PreUpdate();
        current.Update();
    }

    public override void Exit()
    {
    }
}