using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public abstract class BaseFsm
{
    public Dictionary<string, IState> dict = new Dictionary<string, IState>();
    public IState Current { get; protected set; }
    public string DebugString
    {
        get
        {
            return "/" + Current.DebugString;
        }
    }
    public BaseFsm Fsm { get; protected set; }

    public void AddStates(params IState[] states)
    {
        foreach (var state in states)
        {
            Assert.IsNotNull(state.Name, "State doesnt have a Name: " + state.GetType());
            dict.Add(state.Name, state);
        }
    }
    public void Start(string startStateName)
    {
        Assert.IsTrue(dict.ContainsKey(startStateName), "Unknown next state: " + startStateName);
        Current = dict[startStateName];
        Current.Enter("", startStateName, 0f);
    }
    public void Stop()
    {
        Current.Exit(Current.Name, "", 0f);
    }

    public virtual void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        Assert.IsTrue(dict.ContainsKey(nextStateName), "Unknown next state: " + nextStateName);
        string lastStateName = Current.Name;
        Current.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        Current = dict[nextStateName];
        Current.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
    }

    public virtual void PreUpdate()
    {
        Current.PreUpdate();
    }

    public virtual void FixedUpdate()
    {
        Current.FixedUpdate();
    }

    public virtual void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        Current.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
    }

    public virtual void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
    {
        Current.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
    }
    public virtual void OnTriggerEnter(Collider collider)
    {
        Current.OnTriggerEnter(collider);
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Current.OnCollisionEnter(collision);
    }
}