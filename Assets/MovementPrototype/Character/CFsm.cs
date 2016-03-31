using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class CFsm : CState
{
    public Dictionary<string, CState> dict;

    public CState last { get; protected set; }
    public CState current { get; protected set; }
    public CState next { get; protected set; }

    protected void AddState(CState state)
    {
        dict.Add(state.Name, state);
    }

    protected void HookEvents()
    {
        foreach (KeyValuePair<string, CState> entry in dict)
        {
            HookEvents(entry.Value);
        }
    }

    protected void HookEvents(CState state)
    {
        state.StateTransitionRequested -= StateTransitionRequestedListener; // makes sure we don't hook twice
        state.StateTransitionRequested += StateTransitionRequestedListener;
    }

    protected virtual void StateTransitionRequestedListener(StateTransitionEventArgs obj)
    {

    }

    public CFsm(CController character, string @namespace = null) : base(character)
    {
        Character = character;
        dict = new Dictionary<string, CState>();
        if (!string.IsNullOrEmpty(@namespace))
        {
            Type[] types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == @namespace && type.BaseType == typeof(CState))
                .ToArray();
            foreach (Type t in types)
            {
                CState state = (CState)Activator.CreateInstance(t, (object)Character);
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