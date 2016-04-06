using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateTransitionArgs
{
    public string LastStateName { get; private set; }
    public string NextStateName { get; private set; }
    public float AdditionalDeltaTime { get; private set; }
    public StateTransitionArgs(string lastStateName, string nextStateName, float additionalDeltaTime)
    {
        LastStateName = lastStateName;
        NextStateName = nextStateName;
        AdditionalDeltaTime = additionalDeltaTime;
    }
}

public abstract class BaseState
{
    public string Name { get; protected set; }
    public BaseFsm Fsm { get; protected set; }

    public BaseState(BaseFsm fsm)
    {
        Fsm = fsm;
    }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    public abstract void PreUpdate();
    public abstract void Enter(StateTransitionArgs args);
    public abstract void Update();
    public abstract void Exit(StateTransitionArgs args);
}