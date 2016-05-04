using UnityEngine;

public abstract class BaseState : IState
{
    public BaseFsm Fsm { get; protected set; }
    public string Name { get; protected set; }
    public string Debug
    {
        get { return Name; }
    }
    public virtual void PreUpdate()
    {
    }
    public virtual void FixedUpdate()
    {
    }
    public virtual void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
    }
    public virtual void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0f, params object[] args)
    {
    }
    public virtual void OnTriggerEnter(Collider colllider)
    {
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
    }
}