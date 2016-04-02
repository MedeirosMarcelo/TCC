using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class StateLoader<FsmType, StateType>
    where FsmType : BaseFsm
    where StateType : BaseState
{
    public void LoadStates(FsmType fsm, string @namespace)
    {
        if (!string.IsNullOrEmpty(@namespace))
        {
            Type[] types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == @namespace && type.BaseType == typeof(StateType))
                .ToArray();
            foreach (Type t in types)
            {
                StateType state = (StateType)Activator.CreateInstance(t, fsm);
                fsm.AddState(state);
            }
        }
    }
}

public abstract class BaseFsm
{
    protected string @namespace;
    public Dictionary<string, BaseState> dict;
    public BaseState current { get; protected set; }

    public void AddState(BaseState state)
    {
        dict.Add(state.Name, state);
    }

    public virtual void ChangeState(string name, float additionalDeltaTime = 0f)
    {
        ChangeState(new StateTransitionArgs(current.Name, name, additionalDeltaTime));
    }

    public virtual void ChangeState(StateTransitionArgs obj)
    {
        current.Exit(obj);
        current = dict[obj.NextStateName];
        current.Enter(obj);
    }

    public BaseFsm(string @namespace = null) : base()
    {
        dict = new Dictionary<string, BaseState>();
        this.@namespace = @namespace;
    }

    public abstract void Update();
}