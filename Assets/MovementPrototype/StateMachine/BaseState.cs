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

public interface IState
{
    string Name { get; }
    BaseFsm Fsm { get; }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    void PreUpdate();
    void Enter(StateTransitionArgs args);
    void Update();
    void Exit(StateTransitionArgs args);
}

public abstract class BaseState : IState
{
    public BaseFsm Fsm { get; protected set; }
    public string Name { get; protected set; }

    public virtual void Enter(StateTransitionArgs args)
    {

    }
    public virtual void Exit(StateTransitionArgs args)
    {

    }
    public virtual void PreUpdate()
    {

    }
    public virtual void Update()
    {

    }
}