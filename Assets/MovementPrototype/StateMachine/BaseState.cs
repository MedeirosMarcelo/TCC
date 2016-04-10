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
    string Debug { get; }
    BaseFsm Fsm { get; }

    // Evaluate Input and changes the FSM Current State
    // This base method should be used as a generic handler
    void PreUpdate();
    void Enter(StateTransitionArgs args);
    void FixedUpdate();
    void Exit(StateTransitionArgs args);
}

public abstract class BaseState : IState
{
    protected float elapsed;
    protected float totalTime;
    protected string nextState;

    public BaseFsm Fsm { get; protected set; }
    public string Name { get; protected set; }
    public string Debug
    {
        get { return Name; }
    }

    public virtual void PreUpdate()
    {
        if (elapsed >= totalTime)
        {
            Fsm.ChangeState(nextState, totalTime - elapsed);
        }
    }
    public virtual void FixedUpdate()
    {
        elapsed += Time.fixedDeltaTime;
    }
    public virtual void Enter(StateTransitionArgs args)
    {
        elapsed = args.AdditionalDeltaTime;
    }
    public virtual void Exit(StateTransitionArgs args)
    {
    }
}