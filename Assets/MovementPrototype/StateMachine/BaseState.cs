using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateTransitionEventArgs : EventArgs {
    public string CurrentStateName { get; private set; }
    public string RequestedStateName { get; private set; }
    public float AdditionalDeltaTime { get; private set; }
    public StateTransitionEventArgs(string currentStateName, string requestedStateName, float additionalDeltaTime = 0f) : base()
    {
        CurrentStateName = currentStateName;
        RequestedStateName = requestedStateName;
        AdditionalDeltaTime = additionalDeltaTime;
    }
}

public abstract class BaseState {
    public string Name { get; protected set; }
    public BaseFsm Fsm { get; protected set; }

    public BaseState(BaseFsm fsm) {
        Fsm = fsm;
    }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    public abstract void PreUpdate();
    public abstract void Enter(StateTransitionEventArgs args);
    public abstract void Update();
    public abstract void Exit();
}