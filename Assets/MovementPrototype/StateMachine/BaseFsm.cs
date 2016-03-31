using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class BaseFsm : BaseState
{
    public Dictionary<string, BaseState> dict;

    public BaseState last { get; protected set; }
    public BaseState current { get; protected set; }
    public BaseState next { get; protected set; }

    protected void AddState(BaseState state)
    {
        dict.Add(state.Name, state);
    }

    protected void HookEvents()
    {
        foreach (KeyValuePair<string, BaseState> entry in dict)
        {
            HookEvents(entry.Value);
        }
    }

    protected void HookEvents(BaseState state)
    {
        state.StateTransitionRequested -= StateTransitionRequestedListener; // makes sure we don't hook twice
        state.StateTransitionRequested += StateTransitionRequestedListener;
    }

    protected virtual void StateTransitionRequestedListener(StateTransitionEventArgs obj)
    {

    }

    public BaseFsm(BaseFsm parentFsm, string @namespace = null, Type baseType = null) : base(parentFsm)
    {
        dict = new Dictionary<string, BaseState>();
        baseType = baseType ?? typeof(BaseState);
        if (!string.IsNullOrEmpty(@namespace))
        {
            Type[] types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == @namespace && type.BaseType == baseType)
                .ToArray();
            foreach (Type t in types)
            {
                BaseState state = (BaseState)Activator.CreateInstance(t, this);
                AddState(state);
            }
        }
        HookEvents();
    }

    public override void PreUpdate()
    {
    }

    public override void Enter(StateTransitionEventArgs args)
    {
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}