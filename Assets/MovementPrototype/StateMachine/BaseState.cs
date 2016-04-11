using UnityEngine;

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
    public virtual void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
        elapsed = additionalDeltaTime;
    }
    public virtual void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
    }
}