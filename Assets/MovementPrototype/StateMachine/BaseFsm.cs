using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public abstract class BaseFsm
{
    public Dictionary<string, BaseState> dict;
    public BaseState current { get; protected set; }

    protected void AddState(BaseState state)
    {
        dict.Add(state.Name, state);
    }

    public virtual void ChangeState(string name, float additionalDeltaTime = 0f)
    {
        ChangeState(new StateTransitionEventArgs(current.Name, name, additionalDeltaTime));
    }

    public virtual void ChangeState(StateTransitionEventArgs obj)
    {
        current.Exit();
        current = dict[obj.RequestedStateName];
        current.Enter(obj);
    }

    public BaseFsm(string @namespace = null, Type baseType = null) : base()
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
    }

    public abstract void Update();
}