using System;
using System.Collections.Generic;

public abstract class BaseFsm : IState
{
    public Dictionary<string, IState> dict;
    public IState Current { get; protected set; }
    public string Name { get; protected set; }
    public string Debug
    {
        get
        {
            return Name + "/" + Current.Debug;
        }
    }
    public BaseFsm Fsm { get; protected set; }

    public void AddState(IState state)
    {
        dict.Add(state.Name, state);
    }

    public virtual void ChangeState(string name, float additionalDeltaTime = 0f)
    {
        ChangeState(new StateTransitionArgs(Current.Name, name, additionalDeltaTime));
    }

    public virtual void ChangeState(StateTransitionArgs obj)
    {
        Current.Exit(obj);
        Current = dict[obj.NextStateName];
        Current.Enter(obj);
    }

    public BaseFsm(BaseFsm fsm = null)
    {
        Fsm = fsm;
        dict = new Dictionary<string, IState>();
    }

    public virtual void PreUpdate()
    {
        Current.PreUpdate();
    }

    public virtual void FixedUpdate()
    {
        Current.FixedUpdate();
    }

    public virtual void Enter(StateTransitionArgs args)
    {
        Current.Enter(args);
    }

    public virtual void Exit(StateTransitionArgs args)
    {
        Current.Exit(args);
    }
}